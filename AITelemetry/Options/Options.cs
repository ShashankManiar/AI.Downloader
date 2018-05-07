using CommandLine;
using System;


namespace AITelemetry.Downloader.Options
{
   

  
    //public class PrgOptions
    //{
    //    [Option("appInsightId", Required = true, HelpText = "Application Insight Instrumentation Id.")]
    //    public string AppInsightId { get; set; }

    //    [Option("appInsightKey", Required = true, HelpText = "Application Insight Access Key.")]
    //    public string AppInsightKey { get; set; }

    //    [Option("folderPath", Required = true, HelpText = "Folder Path to persist the telemetry json files.")]
    //    public string FolderPath { get; set; }
        

    //    //[Option("metrics",  Default= MetricType.RequestsCount, HelpText = "Analytical Query - Requests ")]
    //    //public MetricType Metrics { get; set; }


    //    //[Option("event", Required = true, Default= EventType.Requests, HelpText = "Analytical Query - Requests ")]
    //    //public EventType Event { get; set; }

    //    [Option("query", Required = true, Default = @"requests", HelpText = "Analytical Query - Requests ")]
    //    public string Query { get; set; }

        
    //    [Option("startDate", Default = @"2018-03-01", HelpText = "Start Date - yyyy-MM-dd format.")]
    //    public string StartDate { get; set; }

    //    public DateTime StartDate2 { get {
    //            DateTime d = Convert.ToDateTime(StartDate);
    //            return new DateTime(d.Year, d.Month, d.Day,0,0,0);
    //        } }
       
        
    //    [Option("days", Default = 1, HelpText = "Number of days from the start date")]
    //    public int NumberofDays { get; set; }


    //    [Option("jsonInterval", Default = 8, HelpText = "Json Time Interval in Hours")]
    //    public int JsonInterval { get; set; }

    //    internal string GetURL(DateTime startRange, DateTime endRange)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class Options
    {
        public MetricOptions MetrictVerb { get; set; }
        public EventOptions EventVerb { get; set; }
        public QueryOptions QueryVerb { get; set; }
    }


    

}
