using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents an individual website user
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Unique identifier for the member
        /// </summary>
        [Key]
        public int MemberId { get; set; }

        /// <summary>
        /// Public facing username for the member.
        /// Alphanumeric characters only
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain alphanumeric characters.")]
        [StringLength(25)]
        public required string Username { get; set; }

        /// <summary>
        /// Email for the member
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Member's password
        /// </summary>
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        public required string Password { get; set; }

        /// <summary>
        /// Date of birth of the member
        /// </summary>
        public DateOnly DateOfBirth { get; set; }
    }

    public class RegistrationViewModel
    {
        /// <summary>
        /// Public facing username for the member.
        /// Alphanumeric characters only
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain alphanumeric characters.")]
        [StringLength(25)]
        public required string Username { get; set; }

        /// <summary>
        /// Email for the member
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        /// <summary>
        /// Member's password
        /// </summary>
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

        /// <summary>
        /// Date of birth of the member
        /// </summary>
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
    }
}
