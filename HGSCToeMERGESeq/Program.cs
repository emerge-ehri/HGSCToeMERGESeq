using System;
using System.IO;
using HGSCToeMERGESeq.Models;
using Newtonsoft.Json;

namespace HGSCToeMERGESeq
{
    class Program
    {
        static void Main(string[] args)
        {
            var translator = new HGSCToeMERGESeq(args[1]);

            var result = JsonConvert.DeserializeObject<HGSCResult>(File.ReadAllText(args[0]));
            Console.WriteLine(result.ToString());

            Console.WriteLine(translator.Transform(result));
        }
    }
}
