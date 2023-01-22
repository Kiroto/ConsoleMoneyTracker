using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model.dbModel
{
    [Table("Account")]
    public  class AccountDb : IIndexable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("listItemId")]
        public int listItemId { get; set; }
        [Column("currencyId")]
        public string currencyId { get; set; }
        [Column("amount")]
        public float amount { get; set; }
        public ListItemDb item { get; set; }
        public CurrencyDb currency { get; set; }
        public TransactionDb transactionDb { get; set; }
    }
}
