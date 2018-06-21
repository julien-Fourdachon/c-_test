using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using ClassLibrary;

namespace ApiTest
{


    class ApiTest
    {


        static void Main (string[] args)
        {
            Line lineInfo = new Line();
            Api api = new Api();
            String getApiBusStop = api.GetApi("http://data.metromobilite.fr/api/linesNear/json?x=5.709360123&y=45.176494599999984&dist=1200&details=true");
            String getApiBusLines = api.GetApi("http://data.metromobilite.fr/api/routers/default/index/routes");
            
            
            List<busStop> busStops = JsonConvert.DeserializeObject<List<busStop>>(getApiBusStop);
            List<busStop> names = busStops.GroupBy(busstop => busstop.name).Select(x => x.First()).ToList();
            List<Line> linedetail = JsonConvert.DeserializeObject<List<Line>>(getApiBusLines);
            


            foreach (busStop name in names)
            {

                foreach (busStop busstop in busStops)
                {
                   if (name.name.Equals(busstop.name))
                    {
                        IEnumerable<string> newLines = name.lines.Union(busstop.lines);
                        name.GetType().GetProperty("lines").SetValue(name, newLines);
                    }
                }

            }
            foreach (busStop name in names)
            {
                Write($"Arrêt: {name.name} \nLignes:");

                foreach (string lines in name.lines)
                {
                    foreach (Line lineColor in linedetail)
                    {
                        if (lineColor.id.Equals(lines))
                        {
                            lineInfo = lineColor;
                        }
                    }
                    Write($"{lines}");
                    
                }
                Write($"\nMode de transport: {lineInfo.mode}\nNom Complet : {lineInfo.longName}");


                WriteLine($"\n");
                
            }

            ReadKey();


            

        }

        
    }
}