namespace CreditCheck.Models
{
    using CreditCheck.Models.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Validation;

    [Table("Customers")]
    public class Customer : IEntity, IValidatableObject
    {
        [Column("CustomerId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id{ get; set; }

        [Column("FirstName")]
        [Required]
        [StringLength(15, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Required]
        [StringLength(15, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Column("DateOfBirth")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Column("Salary", TypeName = "money")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Column("IsEligible")]
        public bool IsEligible { get; set; }

        [Column("CreatedOn")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> CreatedOn { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        [Column("UpdatedOn")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> UpdatedOn { get; set; }

        [Column("UpdatedBy")]
        public string UpdatedBy { get; set; }

        // Foreign Key
        [Column("CardId")]
        public Nullable<int> CardId { get; set; }

        // Navigation Property
        public virtual Card CustomerCard { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName.ToUpper() == LastName.ToUpper())
            {
                yield return new ValidationResult(
                    "FirstName and LastName should not be the same",
                    new[] { nameof(FirstName), nameof(LastName) });
            }

            if (DateOfBirth.Date >= DateTime.Now.Date)
            {
                yield return new ValidationResult(
                    $"Date of Birth must have a date no later than {DateTime.Now}.",
                    new[] { nameof(DateOfBirth) });
            }

            if (Salary == 0)
            {
                yield return new ValidationResult(
                    $"Salary must be greater than {Salary}.",
                    new[] { nameof(Salary) });
            }
        }
    }
}
