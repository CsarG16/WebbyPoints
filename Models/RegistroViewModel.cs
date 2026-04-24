using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo institucional es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingresa un formato de correo válido (ejemplo@usmp.pe)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debes confirmar tu contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Indica tu universidad")]
        public string Universidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Indica tu carrera")]
        public string Carrera { get; set; } = string.Empty;

        [Range(16, 99, ErrorMessage = "Debes ser mayor de edad")]
        public int Edad { get; set; }

        // Aquí guardaremos las categorías que el usuario elija (ej: Café, Gym, Ocio)
        public List<string> CategoriasFavoritas { get; set; } = new List<string>();
    }
}