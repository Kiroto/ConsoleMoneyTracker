using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model.dbModel
{
    [Table("Currency")]
    public class CurrencyDb : Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get => apiIdentifier; set { apiIdentifier = value; } }
        [Column("listItemId")]
        public int listItemId { get; set; }
        [Column("apiidentifier")]
        public string apiIdentifier { get; set; }
        [Column("lastupdated")]
        public DateTime lastUpdated { get; set; }
        [Column("todollar")]
        public  float toDollar { get; set; }
        public ListItemDb item { get; set; }
        public AccountDb accountDb { get; set; }

        public CurrencyDb(Currency currency)
        {
            ID = currency.ID;
            listItemId = currency.item.ID;
            apiIdentifier = currency.apiIdentifier;
            lastUpdated = currency.lastUpdated;
            toDollar = currency.toDollar;
        }

        public CurrencyDb()
        {

        }
    }
}
