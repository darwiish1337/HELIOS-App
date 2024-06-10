﻿namespace WebApimyServices.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtUtils _jwtUtils;

        public AuthController(IAuthService authService,UserManager<ApplicationUser> userManager,IEmailService emailService,SignInManager<ApplicationUser> signInManager,IJwtUtils jwtUtils)
        {
            _authService = authService;
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _jwtUtils = jwtUtils;
        }

        //Create Account (Factor - Registration)
        [HttpPost]
        public async Task<IActionResult> FactorRegisttration(RegistertionFactorDto registertionFactorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var result = await _authService.RegisterFactorAsync(registertionFactorDto);

            if (result.IsAuthenticated)
            {
                var user = await _userManager.FindByEmailAsync(result.Email);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = Url.Action("ConfirmEmail", "Auth",
                    new { email = user.Email, code = code }, protocol: HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(user.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clicking here</a>.");

                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

                return Ok(new 
                { 
                    role = result.Roles,
                    token = result.Token,
                    expiresOn = result.ExpiresOn,
                    message = "Account Registertion SucessFully. " +
                             "Please Check Your Email To Confirm Your Account !" 
                });
            }

            return BadRequest(result.Message);
        }

        //Create Account (Customer - Registration)
        [HttpPost]
        public async Task<IActionResult> CustomerRegisttration(RegistertionCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var result = await _authService.RegisterCustomerAsync(customerDto);

            if (result.IsAuthenticated)
            {
                var user = await _userManager.FindByEmailAsync(result.Email);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = Url.Action("ConfirmEmail", "Auth",
                    new { email = user.Email, code = code }, protocol: HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(user.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

                return Ok(new
                {
                    role = result.Roles,
                    token = result.Token,
                    expiresOn = result.ExpiresOn,
                    messag = "Account Registertion SucessFully. " +
                             "Please Check Your Email To Confirm Your Account !"
                });
            }

            return BadRequest(result.Message);
        }

        //Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var result = await _authService.LoginAsync(loginUserDto);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        //Refresh Token
        [HttpGet]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        //Revoke Token
        [HttpPost]
        public async Task<IActionResult> RevokeToken([FromBody]RevokeTokenDto revokeTokenDto)
        {
            var token = revokeTokenDto.Token ?? Request.Cookies["refreshToken"];
               
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _authService.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid!");

            return Ok();
        }

        // SetTokenInCookie
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,

            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        //Confirm Email
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code))
                    return BadRequest("Email and Code Are Required");

                var user = await _userManager.FindByEmailAsync(email);

                if (user is null)
                    return NotFound("User Not Found");

                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                    return Ok("Email Successfully Confirmed!");

                return BadRequest("There Are Errors While Confirming Your Email.");
        }

        //Send Email & Generate Reset Password Token
        [HttpPost]
        public async Task<IActionResult> SendForgetPasswordEmail(ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return BadRequest("Account May Be Not Found Or Not Confirmed !");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendEmailAsync(mailTo: user.Email, "Reset Your Password",
                $"Reset Your Password By <a href='{HtmlEncoder.Default.Encode($"https://muhameddarwish-001-site1.ftempurl.com/ResetPassword/html/ConfirmResetPassword.html?email={user.Email}&token={token.ToBase64()}")}'>clicking here</a>.");

            return Ok("Please Check Your Email To Aprove Reset Password.");
        }

        //Resend Reset Password Email
        [HttpPost]
        public async Task<IActionResult> ResendForgetPasswordEmail(ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return BadRequest("Account May Be Not Found Or Not Confirmed !");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendEmailAsync(mailTo: user.Email, "Reset Your Password",
                $"Reset Your Password By <a href='{HtmlEncoder.Default.Encode($"https://muhameddarwish-001-site1.ftempurl.com/ResetPassword/html/ConfirmResetPassword.html?email={user.Email}&token={token.ToBase64()}")}'>clicking here</a>.");

            return Ok("Please Check Your Email To Aprove Reset Password.");
        }

        //Reset Password
        [HttpPost]
        public async Task<IActionResult> ConfirmResetPassword(string token, string email, string newPassword, string confirmPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return BadRequest("Account May Be Not Found Or Not Confirmed !");

            if (newPassword.IsNullOrEmpty() && newPassword.IsNullOrEmpty())
                return BadRequest("You must enter New Password & Confirm Password !");

            if (newPassword != confirmPassword)
                return BadRequest("Confirm Password not exist With New Password !");

            // Decode the token from Base64
            var decodedToken = Convert.FromBase64String(token);

            // Convert the decoded token to a string
            var tokenString = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, tokenString, newPassword);

            if (result.Succeeded)
            {
              return Ok("Password Reseted SuccessFully");
            }
              return BadRequest(result.Errors);
        }

        //Logout
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Get the current token from the request headers
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Expire the token
            _jwtUtils.ExpireToken(token);
                        
            // Expire refresh token
            await _authService.RevokeTokenAsync(token);

            // Remove the token from the user's session
            await HttpContext.SignOutAsync();

            // Remove the token from the cookie
            HttpContext.Response.Cookies.Delete("Token");
            // Remove the refresh token from the cookie
            HttpContext.Response.Cookies.Delete("RefreshToken");

            // Return a success response
            return Ok("Logged out successfully");
        }
    }
}