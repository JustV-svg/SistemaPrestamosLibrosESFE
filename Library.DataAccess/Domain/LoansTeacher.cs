using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Library.DataAccess.Domain
{
    [Table("LoansTeacher")]
    public class LoansTeacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LOANSTEACHER_ID { get; set; }

        public long PERSONAL_ID { get; set; }

        public string? PERSONALNAME { get; set; }

        [Display(Name = "Tipo de Préstamo")]
        [Required(ErrorMessage = "El tipo de préstamo es obligatorio")]
        public int ID_TYPE { get; set; }

        [Display(Name = "Estado de Reservación")]
        public int ID_RESERVATION { get; set; }
        public string? ROL { get; set; }

        [Required(ErrorMessage = "El correo institucional es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string? EMAIL { get; set; }

        [Display(Name = "Libro")]
        [Required(ErrorMessage = "Debe seleccionar un libro")]
        public long ID_BOOK { get; set; }

        [Display(Name = "Contacto del Prestamista")]
        [Required(ErrorMessage = "El contacto es obligatorio")]
        [StringLength(50, ErrorMessage = "El contacto no puede exceder los 50 caracteres")]
        public string LENDER_CONTACT { get; set; } = string.Empty;

        public DateTime REGISTRATION_DATE { get; set; }
        public DateTime END_DATE { get; set; }

        public bool STATUS { get; set; }

        // --- Propiedades No Mapeadas (Auxiliares) ---
        [NotMapped] public long ID_LENDER { get; set; }
        [NotMapped] public int COPY { get; set; }
      
        [NotMapped] public int Top_Aux { get; set; }

        // --- Propiedades de Navegación con [ValidateNever] ---
        // Esto evita que el ModelState sea inválido si estos objetos vienen nulos desde la vista

        [ValidateNever]
        [ForeignKey("ID_TYPE")]
        public virtual LoanTypes? LoanTypes { get; set; }

        [ValidateNever]
        [ForeignKey("ID_RESERVATION")]
        public virtual ReservationStatus? ReservationStatus { get; set; }

        [ValidateNever]
        [ForeignKey("ID_BOOK")]
        public virtual Books? Books { get; set; }

       
        public virtual ICollection<LoanDates>? LoanDates { get; set; }
    }
}