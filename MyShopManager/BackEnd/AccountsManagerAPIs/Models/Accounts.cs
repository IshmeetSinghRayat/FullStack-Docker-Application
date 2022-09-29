using System.ComponentModel.DataAnnotations;

namespace AccountsManagerAPIs.Models
{
    public class Accounts
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string? EmailId { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int ContactNumber { get; set; }
        [Required]
        public bool Gender { get; set; }
        public int AccountType { get; set; }
    }
}
