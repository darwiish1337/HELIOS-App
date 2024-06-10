namespace WebApimyServices.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly JwtOptions _jwtOptions;

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<AuthModelDto> RegisterCustomerAsync(RegistertionCustomerDto registertionCustomerDto)
        {
            if (await _userManager.FindByEmailAsync(registertionCustomerDto.Email) is not null)
                return new AuthModelDto { Message = "Email is already registered !" };

            var exsitPhoneNumber = await _context.Users.AnyAsync(u => u.PhoneNumber == registertionCustomerDto.PhoneNumber);

            if (exsitPhoneNumber == true)
                return new AuthModelDto { Message = "PhoneNumber Is Already Exist !" };

            ApplicationUser user = new ApplicationUser();

            _mapper.Map(registertionCustomerDto, user);

            user.UserName = registertionCustomerDto.Email;

            var result = await _userManager.CreateAsync(user, registertionCustomerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModelDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, RoleConstants.Customer);

            var jwtSecurityToken = await CreateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);

            return new AuthModelDto
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { RoleConstants.Customer },
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }

        public async Task<AuthModelDto> RegisterFactorAsync(RegistertionFactorDto registertionFactorDto)
        {
            if (await _userManager.FindByEmailAsync(registertionFactorDto.Email) is not null)
                return new AuthModelDto { Message = "Email is already registered !" };

            var exsitPhoneNumber = await _context.Users.AnyAsync(u => u.PhoneNumber == registertionFactorDto.PhoneNumber);

            if (exsitPhoneNumber == true)
                return new AuthModelDto { Message = "PhoneNumber Is Already Exist !" };

            ApplicationUser user = new ApplicationUser();

           _mapper.Map(registertionFactorDto,user);

            user.UserName = registertionFactorDto.Email;

            var result = await _userManager.CreateAsync(user, registertionFactorDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModelDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, RoleConstants.Factor);

            var jwtSecurityToken = await CreateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);

            return new AuthModelDto
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { RoleConstants.Factor },
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }

        // Generate Token
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtOptions.LifeTime),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthModelDto> LoginAsync(LoginUserDto loginUserDto)
        {
            var authModel = new AuthModelDto();

            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (user is null)
                return new AuthModelDto { Message = "email or password is incorrect" };

            if (!user.EmailConfirmed)
                return new AuthModelDto { Message = "Email Must Be Confirmed !" };

            if (await _userManager.IsLockedOutAsync(user))
                return new AuthModelDto { Message = "Your Account Is Locked Out 5 Minutes." };

            var found = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if (found is true)
            {
                await _userManager.ResetAccessFailedCountAsync(user);

                var jwtSecurityToken = await CreateJwtToken(user);
                var roleList = await _userManager.GetRolesAsync(user);

                authModel.IsAuthenticated = true;
                authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authModel.Email = user.Email;
                authModel.ExpiresOn = jwtSecurityToken.ValidTo;
                authModel.Roles = roleList.ToList();
            }
            else
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                    return new AuthModelDto { Message = "Your Account Is Locked Out 5 Minutes.." };
            }

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;

                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            return authModel;
        }

        // Generate RefreshToken
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.Now.AddHours(1),
                CreatedOn = DateTime.Now
            };
        }

        public async Task<AuthModelDto> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModelDto();

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user is null)
            {
                authModel.Message = "Invalid Token !";

                return authModel;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authModel.Message = "Inactive Token !";

                return authModel;
            }

            refreshToken.RevokedOn = DateTime.Now;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email = user.Email;
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Roles = roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return authModel;
        }

        //Revoke RefreshToken
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user is null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return true;
        }
    }
}
