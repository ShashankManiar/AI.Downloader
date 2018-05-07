using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITelemetry.Downloader.Options
{
    public enum MetricSourceType
    {
        RequestsCount,
        RequestsDuration,
        RequestsFailed,
        PageViewsCount,
        PageViewsDuration,
        BrowserTimingsNetworkDuration
    }
    [Verb("metric", HelpText = "Download metric data.")]
    public class MetricOptions : OptionBase
    {
        [Option("metricSsource", Default = MetricSourceType.RequestsCount, HelpText = "Metric")]
        public MetricSourceType MetricSource { get; set; }

        //[Option("timeSpan", Default = "P1D", HelpText = "Specify time span - StartDate/EndDate in yyyy-mm-dd format or Last 12 Hours => PT12H or Last 1 Day => P1D or Last 7 Days => P7D")]
        //public string TimeSpan { get; set; }


        //[Option("Interval", HelpText = "Specify interval 30 Minutes => PT30M or 1 Hour => PT1H or 3 Hours => PT3H or 1 Day => P1D")]
        //public string Interval { get; set; }

        //[Usage(ApplicationAlias = "AITelemetry.Downloader.exe")]
        //public static IEnumerable<Example> Examples
        //{
        //    get
        //    {
        //        yield return new Example("Normal scenario", new MetricOptions { AppInsightId = "Your application insight id", AppInsightKey = "Your App Insight Key", FolderPath = @"C:\Temp\AIDownload", Metric = MetricType.RequestsCount });
        //        yield return new Example("Specify timespan", new MetricOptions { AppInsightId = "Your application insight id", AppInsightKey = "Your App Insight Key", FolderPath = @"C:\Temp\AIDownload", Metric = MetricType.RequestsCount, TimeSpan = "2018-05-01/2018-05-02" });
        //        yield return new Example("Specify interval", new MetricOptions { AppInsightId = "Your application insight id", AppInsightKey = "Your App Insight Key", FolderPath = @"C:\Temp\AIDownload", Metric = MetricType.RequestsCount, TimeSpan = "2018-05-01/2018-05-02", Interval = "PT30M" });
        //    }
        //}

        public override string GetUrL(DateTime startRange, DateTime endRange)
        {
            return OptionBase.APP_URL;
        }
    }
}
