#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegLog.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "El identificador del usuario es requerido!")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es requerido!")]
        [MinLength(2, ErrorMessage = "El nombre del usuario debe tener al menos 2 caracteres!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido del usuario es requerido!")]
        [MinLength(2, ErrorMessage = "El nombre del usuario debe tener al menos 2 caracteres!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress]
        [UniqueEmail]
        [MinLength(8, ErrorMessage = "El correo electrónico debe ser único con 8 caracteres como mínimo!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida!")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de contraseña es requerida!")]
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La confirmación de la contraseña debe ser igual a la contraseña requerida")]
        public string PasswordConfirm { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
