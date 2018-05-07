using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITelemetry.Downloader.Options
{
    public enum EventSourceType
    {
        Requests,
        CustomEvents,
        PageViews,
        BrowserTimings,
        Dependencies,
        Traces,
        Exceptions,
        AvailabilityResults
    }


    [Verb("event", HelpText = "Download event data.")]
    public class EventOptions : OptionBase
    {

        [Option("eventSource", Required = true, Default = EventSourceType.Requests, HelpText = "Analytical Query - Requests ")]
        public EventSourceType EventSource { get; set; }

        //[Option("timeSpan", Default = "P1D", HelpText = "Specify time span - StartDate/EndDate in yyyy-mm-dd format or Last 12 Hours => PT12H or Last 1 Day => P1D or Last 7 Days => P7D")]
        //public string TimeSpan { get; set; }

        //[Usage(ApplicationAlias = "AITelemetry.Downloader.exe")]
        //public static IEnumerable<Example> Examples
        //{
        //    get
        //    {
        //        yield return new Example("Normal scenario", new EventOptions { AppInsightId = "Your application insight id", AppInsightKey = "Your App Insight Key", FolderPath = @"C:\Temp\AIDownload", Event = EventType.Requests });
        //        yield return new Example("Specify timespan", new EventOptions { AppInsightId = "Your application insight id", AppInsightKey = "Your App Insight Key", FolderPath = @"C:\Temp\AIDownload", Event = EventType.Requests, TimeSpan = "2018-05-01/2018-05-02" });

        //    }
        //}

        public override string GetUrL(DateTime startRange, DateTime endRange)
        {
            return OptionBase.APP_URL;
            //string.Format("{0}/{1}/query?timespan={2}/{3}&query={4}", APP_URL, this.AppInsightId, startDateRange, endDateRange, this.Event)
        }
    }
}
