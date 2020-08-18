namespace CreditCheck.Models
{
    using CreditCheck.Models.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cards")]
    public class Card : IEntity
    {
        [Column("CardId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("CardType")]
        [Required]
        [StringLength(15)]
        public string CardType { get; set; }

        [Column("BankName")]
        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [Column("AgeLimit")]
        [Required]
        public int AgeLimit { get; set; }

        [Column("SalaryMin")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal SalaryMin { get; set; }

        [Column("APR")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal APR { get; set; }

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
    }
}
