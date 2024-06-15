﻿namespace WebApimyServices.Controllers
{
    /// <summary>
    /// Controller for managing authentication and authorization.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for user registration, login, token refresh, token revocation, email confirmation, password reset, and logout.
    /// </remarks>
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

        /// <summary>
        /// Creates a new account for a factor (registration).
        /// </summary>
        /// <param name="registertionFactorDto">The registration data.</param>
        /// <returns>A JSON response with the authentication result.</returns>
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

                await _emailService.SendEmailAsync(
                    user.Email,
                    "Confirm Your Email Address",
                    @$"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Confirm Your Email</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f9f9f9;
                margin: 0;
                padding: 20px;
            }}
            .email-container {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                max-width: 600px;
                margin: auto;
            }}
            .email-header {{
                text-align: center;
                margin-bottom: 20px;
            }}
            .email-body {{
                font-size: 16px;
                line-height: 1.5;
            }}
            .email-footer {{
                margin-top: 20px;
                text-align: center;
                font-size: 14px;
                color: #888888;
            }}
            .button {{
                display: inline-block;
                padding: 10px 20px;
                font-size: 16px;
                color: #ffffff;
                background-color: #4CAF50; /* Button color */
                text-decoration: none;
                border-radius: 5px;
                font-weight: bold; /* Make button text bold */
                border: none;
                cursor: pointer;
                text-align: center;
            }}
            .button:hover {{
                background-color: #45a049; /* Darker shade for hover effect */
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='email-header'>
                <h2>Welcome to Helios!</h2>
            </div>
            <div class='email-body'>
                <p>Dear {user.DisplayName},</p>
                <p>Thank you for registering with Helios. To complete your registration, please confirm your email address by clicking the button below:</p>
                <p><a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='button'>Confirm Your Email</a></p>
                <p>If you did not create an account with us, please ignore this email.</p>
                <p>Thank you,<br>Helios Team</p>
            </div>
            <div class='email-footer'>
                <p>© 2024 Helios. All rights reserved.</p>
            </div>
        </div>
    </body>
    </html>
    ");

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

        /// <summary>
        /// Creates a new account for a customer (registration).
        /// </summary>
        /// <param name="customerDto">The customer registration data.</param>
        /// <returns>A JSON response with the authentication result.</returns>
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

                await _emailService.SendEmailAsync(
                    user.Email,
                    "Confirm Your Email Address",
                    @$"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Confirm Your Email</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f9f9f9;
                margin: 0;
                padding: 20px;
            }}
            .email-container {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                max-width: 600px;
                margin: auto;
            }}
            .email-header {{
                text-align: center;
                margin-bottom: 20px;
            }}
            .email-body {{
                font-size: 16px;
                line-height: 1.5;
            }}
            .email-footer {{
                margin-top: 20px;
                text-align: center;
                font-size: 14px;
                color: #888888;
            }}
            .button {{
                display: inline-block;
                padding: 10px 20px;
                font-size: 16px;
                color: #ffffff;
                background-color: #4CAF50; /* Button color */
                text-decoration: none;
                border-radius: 5px;
                font-weight: bold; /* Make button text bold */
                border: none;
                cursor: pointer;
                text-align: center;
            }}
            .button:hover {{
                background-color: #45a049; /* Darker shade for hover effect */
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='email-header'>
                <h2>Welcome to Helios!</h2>
            </div>
            <div class='email-body'>
                <p>Dear {user.DisplayName},</p>
                <p>Thank you for registering with Helios. To complete your registration, please confirm your email address by clicking the button below:</p>
                <p><a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='button'>Confirm Your Email</a></p>
                <p>If you did not create an account with us, please ignore this email.</p>
                <p>Thank you,<br>Helios Team</p>
            </div>
            <div class='email-footer'>
                <p>© 2024 Helios. All rights reserved.</p>
            </div>
        </div>
    </body>
    </html>
    ");




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

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginUserDto">The login credentials.</param>
        /// <returns>A JSON response with the authentication result.</returns>
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

        /// <summary>
        /// Refreshes a token.
        /// </summary>
        /// <returns>A JSON response with the refreshed token.</returns>
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

        /// <summary>
        /// Revokes a token.
        /// </summary>
        /// <param name="revokeTokenDto">The token to revoke.</param>
        /// <returns>A JSON response indicating the revocation result.</returns>
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

        /// <summary>
        /// Confirms an email address.
        /// </summary>
        /// <param name="email">The email address to confirm.</param>
        /// <param name="code">The confirmation code.</param>
        /// <returns>A JSON response indicating the confirmation result.</returns>
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

        /// <summary>
        /// Sends a password reset email.
        /// </summary>
        /// <param name="ResetPasswordDto">The password reset data.</param>
        /// <returns>A JSON response indicating the email sending result.</returns>
        [HttpPost]
        public async Task<IActionResult> SendForgetPasswordEmail(ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return BadRequest("Account May Be Not Found Or Not Confirmed!");

            var verificationCode = await _userManager.GenerateUserTokenAsync(user, "Email", "ResetPassword");

            await _emailService.SendEmailAsync(mailTo: user.Email, "Reset Your Password",
                $"Dear {user.UserName},<br><br>" +
                $"We have received a request to reset your password. If you did not make this request, please ignore this email.<br><br>" +
                $"To reset your password, please enter the following code: <b>{verificationCode}</b>.<br><br>" +
                $"If you have any questions or concerns, please don't hesitate to contact us.<br><br>" +
                $"Best regards,<br>" +
                $"[Service App Team]");

            return Ok("Please Check Your Email To Get The Verification Code.");
        }

        /// <summary>
        /// Resends a password reset email.
        /// </summary>
        /// <param name="resetPasswordDto">The password reset data.</param>
        /// <returns>A JSON response indicating the email sending result.</returns>
        [HttpPost]
        public async Task<IActionResult> ResendForgetPasswordEmail(ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return BadRequest("Account May Be Not Found Or Not Confirmed!");

            var verificationCode = await _userManager.GenerateUserTokenAsync(user, "Email", "ResetPassword");

            await _emailService.SendEmailAsync(mailTo: user.Email, "Reset Your Password",
                $"Dear {user.UserName},<br><br>" +
                $"We have received a request to reset your password. If you did not make this request, please ignore this email.<br><br>" +
                $"To reset your password, please enter the following code: <b>{verificationCode}</b>.<br><br>" +
                $"If you have any questions or concerns, please don't hesitate to contact us.<br><br>" +
                $"Best regards,<br>" +
                $"[Service App Team]");

            return Ok("Please Check Your Email To Get The Verification Code.");
        }

        /// <summary>
        /// Resets a password.
        /// </summary>
        /// <param name="token">The password reset token.</param>
        /// <param name="email">The email address associated with the password reset.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirmed new password.</param>
        /// <returns>A JSON response indicating the password reset result.</returns>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordWithCodeDto resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.ToList());

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return BadRequest("Account May Be Not Found Or Not Confirmed !");

            if (resetPassword.NewPassword.IsNullOrEmpty() && resetPassword.ConfirmNewPassword.IsNullOrEmpty())
                return BadRequest("You must enter New Password & Confirm Password !");

            if (resetPassword.NewPassword != resetPassword.ConfirmNewPassword)
                return BadRequest("Confirm Password not exist With New Password !");

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Code, resetPassword.NewPassword);

            if (!result.Succeeded)
                return BadRequest("Failed to reset password.");

            return Ok("Password reset successfully.");
        }

        /// <summary>
        /// Logs out a user.
        /// </summary>
        /// <returns>A JSON response indicating the logout result.</returns>
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
