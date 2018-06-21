using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ApiTest
{


    class ApiTest
    {
      

        static void Main (string[] args)
        {

            // Create a request for the URL.
            WebRequest request = WebRequest.Create("http://data.metromobilite.fr/api/linesNear/json?x=5.709360123&y=45.176494599999984&dist=1200&details=true");

            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.  
            WebResponse response = request.GetResponse();

            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.  
            string responseFromServer = reader.ReadToEnd();


            List<busStop> busStops = JsonConvert.DeserializeObject<List<busStop>>(responseFromServer);
            List<busStop> names = busStops.GroupBy(busstop => busstop.name).Select(x => x.First()).ToList();


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
                WriteLine(name.name);

                foreach(string lines in name.lines)
                {
                    WriteLine(lines);
                }
            }

                ReadKey();


            

        }

        
    }
}