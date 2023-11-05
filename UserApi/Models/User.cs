using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public required string Name { get; set; }

        [Column("phone")]
        [Required]
        [Phone]
        public required string Phone { get; set; }

        [Column("email")]
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }

    public class UserDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        [Phone]
        public required string Phone { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}

