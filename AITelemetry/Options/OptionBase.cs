using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITelemetry.Downloader
{
    public class OptionBase
    {
        public const string APP_URL = "http://api.applicationinsights.io/v1/apps";

        [Option("appInsightId", Required = true, HelpText = "Application Insight Instrumentation Id.")]
        public string AppInsightId { get; set; }

        [Option("appInsightKey", Required = true, HelpText = "Application Insight Access Key.")]
        public string AppInsightKey { get; set; }

        [Option("folderPath", Required = true, HelpText = "Folder Path to persist the telemetry json files.")]
        public string FolderPath { get; set; }

        public virtual string GetUrL(DateTime startRange, DateTime endRange)
        {
            return APP_URL;
        }
    }

}
