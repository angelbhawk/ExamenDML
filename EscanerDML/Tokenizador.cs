using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EscanerDML
{
    class Tokenizador
    {
        public Tokenizador() { }

        public static MatchCollection getTokens(string text)
        {
            //string pattern = @"(\n)|(('|‘|’)[^'‘’]*('|’|‘))|([=<>]{2})|(\W)|([@a-zA-Z0-9#]*)";
            string pattern = @"(\n)|(('|‘|’)[^'‘’]*('|’|‘))|([*,)(])|([=<>]{1,2})|([@a-zA-Z0-9#]*)";

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = r.Matches(text);
            return matches;
        }
    }
}