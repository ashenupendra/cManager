using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace cManager.Shared.Accounting
{
    public class AccountTransaction
    {
        public int Id { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [StringLength(100)]
        public string ReferanceNumber { get; set; }

        [Required]
        public int FromAccountId { get; set; }
        public Account FromAccount { get; set; }

        [Required]
        public int ToAccountId { get; set; }
        public Account ToAccount { get; set; }
    }
}
