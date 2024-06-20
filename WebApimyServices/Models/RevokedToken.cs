namespace WebApimyServices.Models
{
    public class RevokedToken
    {
        public int Id { get; set; }
        public string TokenId { get; set; } // Store the token identifier (e.g., JWT ID claim)
        public DateTime RevokedAt { get; set; } // Timestamp when the token was revoked
    }
}
