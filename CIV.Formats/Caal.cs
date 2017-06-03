using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using CIV.Ccs;
using CIV.Hml;
using Newtonsoft.Json.Linq;

namespace CIV.Formats
{
    public class Caal
    {
        public string Extension => "caal";
        public string Name { get; set; }


        public IDictionary<string, CcsProcess> Processes { get; private set; }

        public IDictionary<string, IHmlFormula> Formulae { get; private set; }

        public Caal Load(string path)
        {
            var text = File.ReadAllText(path);
            var json = JObject.Parse(text);
            Name = (string)json["title"];
            Processes = CcsFacade.ParseAll((string)json["ccs"]);

            Formulae = json["properties"]
                .Where(x => (string)x["className"] == "HML" &&
                       (string)x["options"]["definitions"] == "")
                .ToDictionary(
                    x => (string)x["options"]["process"],
                    x => HmlFacade.ParseAll((string)x["options"]["topFormula"])
                );
            return this;
        }
    }
}
