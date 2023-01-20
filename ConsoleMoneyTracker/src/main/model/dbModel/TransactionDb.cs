using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model.dbModel
{
    [Table("transaction")]
    public class TransactionDb : IIndexable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("description")]
        public string description { get; set; }
        [Column("category")]
        public Category category { get; set; }
        [Column("sourceaccount")]
        public Account sourceAccount { get; set; }
        [Column("targetaccount")]
        public Account targetAccount { get; set; }
        [Column("amount")]
        public float amount { get; set; }
        [Column("rate")]
        public float rate { get; set; }
        [Column("date")]
        public DateTime date { get; set; }
    }
}
