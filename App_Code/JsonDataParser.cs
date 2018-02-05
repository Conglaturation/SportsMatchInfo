using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace Objects
{
    /// <summary>
    /// Summary description for JsonDataParser
    /// </summary>
    public class JsonDataParser
    {
        /// <summary>
        /// Parses and deserializes a JSON data file into objects
        /// </summary>
        /// <param name="jsonFileLoc">JSON data file location as a string</param>
        public static void ParseJson(string jsonFileLoc)
        {
            using (StreamReader r = new StreamReader(jsonFileLoc))
            {
                string json = r.ReadToEnd();
                Matches.matches = JsonConvert.DeserializeObject<IList<Match>>(json);
            }
        }
    }
}