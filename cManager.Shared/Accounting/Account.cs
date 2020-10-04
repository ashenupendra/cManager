using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace cManager.Shared.Accounting
{
    public class Account : Auditable
    {
        public int Id { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public int AccountType { get; set; }

        [StringLength(300)]
        public string AccountDescription { get; set; }

        public string AccountReferance { get; set; }

        public bool IsActive { get; set; }

        public List<AccountTransaction> Payments { get; set; }
        public List<AccountTransaction> Received { get; set; }
    }
}
