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
    [Table("ListItem")]
    public  class ListItemDb : IIndexable<int>
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("shortname")]
        public string shortName { get; set; }
        [Column("description")]
        public string description { get; set; }
        [Column("foregroundcolor")]
        public ConsoleColor foregroundColor { get; set; }
        [Column("backgroundcolor")]
        public ConsoleColor backgroundColor { get; set; }
        [Column("creationdate")]
        public DateTime creationDate { get; set; }
        [Column("removaldate")]
        public DateTime? removalDate { get; set; }
        public CategoryDb categoryDb { get; set; }
        public AccountDb accountDb { get; set; }    
        public CurrencyDb currencyDb { get; set; }

        
    }
}
