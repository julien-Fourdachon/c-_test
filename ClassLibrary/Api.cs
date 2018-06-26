﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace ClassLibrary
{
    public class Api
    {
        public string GetApi(string url)
        {
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);

            // If required by the server, set the credentials.  
           // request.Credentials = CredentialCache.DefaultCredentials;

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

            return (responseFromServer);
        }
    }

   
}
