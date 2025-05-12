namespace Sakan_project.DTO
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }  // The token generated from Forgot Password
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
