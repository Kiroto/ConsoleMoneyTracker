using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model.httpModel
{
    public class CurrencyRatesView
    {
        // Result of the /latest endpoint
        [JsonPropertyName("data")]
        public Dictionary<string, float> data;
    }
}
