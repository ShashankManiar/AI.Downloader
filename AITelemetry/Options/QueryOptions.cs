using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;

namespace AITelemetry.Downloader.Options
{
    [Verb("query", HelpText = "Download data based on App Insight query")]
    public class QueryOptions : OptionBase
    {
        [Option("aiQuery", Required = true, Default = @"requests", HelpText = "Analytical Query")]
        public string AIQuery { get; set; }

        //[Option("timeSpan", SetName = "TimeRange", Default = "P1D", HelpText = "A timespan, or time range, for a query can be specified as\n" +
        //                                                                        "\t\t\t1. the length of time before now(e.g P2D for the last 2 days, PT12H for the \n\t\t\t last 12 hours, etc.),or \n" +
        //                                                                        "\t\t\t2. as a time range specified by a start and end time where each time has the \n\t\t\t" +
        //                                                                        "format YYYY - MM - DDThh:mm: ss.sssZ and these are combined to form a time range as\n\t\t\t" +
        //                                                                        "2016-03-01T13:00:00Z/20016-03-03T15:30:00Z, or \n\t\t\t" +
        //                                                                        "3. as a start time and a length of time after that, e.g. 2016 - 03 - 01 / P1D which\n\t\t\t" +
        //                                                                        "specifies the time range covering the entire day March 1 UTC.\n\t\t\t" +
        //                                                                        "Specify time span - StartDate/EndDate in yyyy-mm-dd format or Last \n\t\t\t" +
        //                                                                        "12 Hours => PT12H or Last 1 Day => P1D or Last 7 Days => P7D")]
        //public string TimeSpan { get; set; }

        [Option("startDate", Required = true, SetName = "TimeRange", HelpText = "StartDate in yyyy-MM-dd format.")]
        public string StartDate { get; set; }

        public DateTime GetStartDateHours()
        {
            DateTime d = DateTime.Now.AddDays(-1); //default yesterday
            DateTime.TryParse(StartDate, out d);
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
        }

        [Option("days", Default = 1, HelpText = "Number of days from the startDate")]
        public int NumberofDays { get; set; }


        [Option("chunkSize", Default = 8, HelpText = "download chunk size in Hours")]
        public int chunkSzie { get; set; }



        [Usage()]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Normal scenario", new QueryOptions { AppInsightId = "Your application insight id", AppInsightKey = "Your App Insight Key", FolderPath = @"C:\Temp\AIDownload", AIQuery = "Requests" });

            }
        }
        public override string GetUrL(DateTime startRange, DateTime endRange)
        {
            string startDateRange = startRange.ToString("yyyy-MM-ddTHH:mm:ss");
            string endDateRange = endRange.ToString("yyyy-MM-ddTHH:mm:ss");
            return string.Format("{0}/{1}/query?timespan={2}/{3}&query={4}", APP_URL, this.AppInsightId, startDateRange, endDateRange, this.AIQuery);
        }
    }
}
