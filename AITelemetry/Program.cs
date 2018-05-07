using AITelemetry.Downloader.Options;
using CommandLine;
using CommandLine.Text;
using System;
using System.Reactive.Subjects;

namespace AITelemetry.Downloader
{
    class Program
    {
        private Subject<Progress> _progressEvents = new Subject<Progress>();
        private QueryOptions _options;
        
        static void Main(string[] args)
        {
           new Program().Run(args);
        }

        public void Run(string[] args)
        {
            _progressEvents.Subscribe(OnUpdateProgress, OnEngineException);
           

            //Parser.Default.ParseArguments<MetricOptions, EventOptions, QueryOptions>(args)
            //.WithParsed<MetricOptions>(opts => DownloadMetricData(opts))
            //.WithParsed<EventOptions>(opts => DownloadEventData(opts))
            //.WithParsed<QueryOptions>(opts => DownloadQueryData(opts))
            //.WithNotParsed(errs => Console.WriteLine ("Invalid Arguments"));

            Parser.Default.ParseArguments< QueryOptions>(args)
                .WithParsed<QueryOptions>(opts => DownloadQueryData(opts))
                .WithNotParsed(errs => Console.WriteLine("Please pass valid parameters."));

        }

        private void DownloadQueryData(QueryOptions opts)
        {
            Console.WriteLine("Download Query Data");
            _options = opts;
            var downloader = new QueryDataDownloader(_progressEvents, opts);
            downloader.DownloadData().Wait();
            Console.WriteLine("Press any key to close the window.");
            Console.ReadLine();
        }

        private void DownloadEventData(EventOptions opts)
        {
            Console.WriteLine("Work in progress - Download Event Data");
            Console.WriteLine("Press any key to close the window.");
            Console.ReadLine();
        }

        private void DownloadMetricData(MetricOptions opts)
        {

            Console.WriteLine("Work in progress - Download Metric Data");
            Console.WriteLine("Press any key to close the window.");
            Console.ReadLine();

        }

        private void OnUpdateProgress(Progress progress)
        {
            var top = Console.CursorTop;
            switch (progress.ProgressType)
            {
                case ProgressType.Initialise:
                    InitialiseConsole();
                    break;
                case ProgressType.NewFile:
                    UpdateStats(progress);
                    Console.CursorTop = top;
                    break;

                case ProgressType.LastMessage:

                    Console.CursorTop = 6;
                    Console.WriteLine("| Progress Message :{0,-75} |", progress.LastMessage);
                    Console.CursorTop = top;
                    break;

                case ProgressType.Update:
                    UpdateStats(progress);
                    break;
                case ProgressType.Complete:
                    //Clear Progress Message
                    OnUpdateProgress(new Progress("Download Complete"));
                    Console.CursorTop = top;
                    PrintLine();
                    Console.WriteLine(progress.LastMessage);
                    break;

            }
            Console.CursorLeft = 0;
        }

        private static void PrintLine()
        {
            Console.WriteLine("|{0}|", new String('-', 95));
        }

        private void InitialiseConsole()
        {
            Console.Clear();
            Console.CursorTop = 1;
            Console.WriteLine("|***************            Application Insights Telemetry Downloader            ***************|");
            PrintLine();
            Console.WriteLine("| Parameters | Start Date:{0} | Days:{1}{2,-50} |", _options.GetStartDateHours().ToString("dd-MM-yyyy"), _options.NumberofDays," ");
            Console.WriteLine("|            | Folder Location:{0,-64} |", _options.FolderPath);
            PrintLine();
            Console.WriteLine();
            PrintLine();
          
            Console.WriteLine( "| Start Range      | End Range        | D-S-Time | D-E-Time | #Files    | FileName              |");
            Console.WriteLine( "|------------------+------------------+----------+----------+-----------+-----------------------|");
        }

        private void  UpdateStats(Progress progress)
        {
            var log = $"| {progress.StartRange.ToString("dd-MM-yyyy HH:mm"),15} " +
            $"| {progress.EndRange.ToString("dd-MM-yyyy HH:mm"),15} " +
            $"| {progress.DownloadStartTime.ToString("HH:mm:ss"),8} " +
            $"| {progress.DownloadEndTime.ToString("HH:mm:ss"),8} " +
            $"| {progress.NumberOfFiles,9} " +
            $"| {progress.FileName,21} |";
            Console.WriteLine(log);
            
        }

        private static void OnEngineException(Exception exception)
        {
            Console.WriteLine(exception);
        }

    }
}
