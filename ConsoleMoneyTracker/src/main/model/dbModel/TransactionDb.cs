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
        [Column("categoryId")]
        public int categoryId { get; set; }
        [Column("sourceAccountId")]
        public int sourceAccountId{ get; set; }
        [Column("targetAccountId")]
        public int targetAccountId { get; set; }
        [Column("listItemId")]
        public int listItemId { get; set; }
        [Column("amount")]
        public float amount { get; set; }
        [Column("rate")]
        public float rate { get; set; }
        [Column("date")]
        public DateTime date { get; set; }
        public ListItemDb item { get; set; }
        public AccountDb account { get; set; }
        public CategoryDb category { get; set; }
    }
}
