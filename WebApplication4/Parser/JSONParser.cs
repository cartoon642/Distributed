using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication4.Parser
{
    public class JSONParser<T>
    {
        public T parseJSON(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
