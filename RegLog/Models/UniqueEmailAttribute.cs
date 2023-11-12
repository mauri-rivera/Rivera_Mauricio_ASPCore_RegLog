using System.ComponentModel.DataAnnotations;

namespace RegLog.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("El Email es requerido!");
            }

            MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

            if (_context.Users.Any(e => e.Email == value.ToString()))
            {
                return new ValidationResult("El Email debe ser único!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
