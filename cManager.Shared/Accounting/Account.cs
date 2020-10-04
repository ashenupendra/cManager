using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static cManager.Shared.Constants;

namespace cManager.Shared.Accounting
{
    public class Account : Auditable
    {
        public int Id { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public int AccountType { get; set; }

        [NotMapped]
        public string AccountTypeDescription
        {
            get
            {
                return ((AccountTypes)AccountType).ToString();
            }
        }

        [StringLength(300)]
        public string AccountDescription { get; set; }

        public string AccountReferance { get; set; }

        public bool IsActive { get; set; }

        public List<AccountTransaction> Payments { get; set; }
        public List<AccountTransaction> Received { get; set; }
    }
}
