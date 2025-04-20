using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocumentationGenerator.Serializing
{
    public static class NavBarUtil
    {
        /// <summary>
        /// Convert JSON into a NavBar object.
        /// </summary>
        /// <param name="json"></param>
        /// <returns>Deserialized object</returns>
        public static NavBar Deserialize(string json)
        {
            return JsonSerializer.Deserialize<NavBar>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
