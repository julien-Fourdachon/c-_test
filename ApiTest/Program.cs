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
            Line lineInfo = new Line();

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

            reader.Close();
            response.Close();

            // Create a request for the URL.   
            WebRequest requests = WebRequest.Create(
              "http://data.metromobilite.fr/api/routers/default/index/routes");
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse responses = requests.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)responses).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStreams = responses.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader readers = new StreamReader(dataStreams);
            // Read the content.  
            string responseFromServers = readers.ReadToEnd();
            // Display the content.  
            // Clean up the streams and the response.  
            readers.Close();
            response.Close();


            List<busStop> busStops = JsonConvert.DeserializeObject<List<busStop>>(responseFromServer);
            List<busStop> names = busStops.GroupBy(busstop => busstop.name).Select(x => x.First()).ToList();
            List<Line> linedetail = JsonConvert.DeserializeObject<List<Line>>(responseFromServers);
            


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
                WriteLine($"Arrêt: {name.name}");

                foreach (string lines in name.lines)
                {
                    foreach (Line lineColor in linedetail)
                    {
                        if (lineColor.id.Equals(lines))
                        {
                            lineInfo = lineColor;
                        }
                    }
                    Write($"{lines} : {lineInfo.type}");
                    
                }
                WriteLine($"\n");
                
            }

            ReadKey();


            

        }

        
    }
}