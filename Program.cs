using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace twilio_downloader
{
    
    class Program
    {
        private static string accountSid = ""; 
        private static string authToken = ""; 
        static void Main(string[] args)
        {
            
            var dateToRun = args.Length >= 1 ?  
                    args[0] : 
                    DateTime.Now.AddDays(-1).ToShortDateString();

            var fileName = $"twilio-logs.csv";
            Console.WriteLine($"Fetching calls for {dateToRun}");
            var path = $"./{fileName}"; 
            
            if (!System.IO.File.Exists(path))
            {
               using (System.IO.FileStream fs = System.IO.File.Create(path)){
                Console.WriteLine("File Written");
                }
            }
            
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {    
                TwilioClient.Init(accountSid, authToken);
                var opts = new ReadMessageOptions(){
                        PageSize = 100000, 
                        DateSentAfter =DateTime.Parse($"{dateToRun} 00:00:00")
                };
                var messages = MessageResource.Read(opts);
                var exportRecords = messages.Select(x => new TwilioExportRecord{
                    datecreated = x.DateCreated.ToString(), 
                    datesent = x.DateSent.ToString(), 
                    lastupdated = x.DateUpdated.ToString(), 
                    sid = x.Sid, 
                    status = x.Status.ToString(), 
                    url = x.Uri.ToString(), 
                    to = x.To.ToString()
                });
                csv.WriteRecords(exportRecords);
            }
        }
       
    }

}
