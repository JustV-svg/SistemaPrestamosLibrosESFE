using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Domain
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Asegura que el ID sea autoincremental
        [Display(Name = "ID Docente")]
        public long PERSONAL_ID { get; set; } // Propiedad solicitada para la clave primaria

        [Required(ErrorMessage = "El nombre es Obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 Caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es Obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 Caracteres")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es Obligatorio")]
        [StringLength(15, ErrorMessage = "Máximo 15 Caracteres")]
        [Display(Name = "Teléfono")]
        public string CellPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es Obligatorio")]
        [StringLength(200, ErrorMessage = "Máximo 200 Caracteres")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}