using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model.httpModel
{
    public class CurrencyRatesView
    {
        // Result of the /latest endpoint
        [JsonProperty("rates")]
        public Dictionary<string, float> data;
    }
}
