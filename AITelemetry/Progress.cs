using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITelemetry.Downloader
{
    public enum ProgressType
    {
        Initialise = 0,
        NewFile,
        Update,
        LastMessage,
        Complete
    }


    public class Progress
    {
        
        public ProgressType ProgressType { set; get; }


        public Progress()
        {
            
        }
        public Progress(string msg)
        {
            ProgressType = ProgressType.LastMessage;
            this.LastMessage = msg;
        }

        public Progress(DateTime startRange, DateTime endRange, 
            DateTime downloadStartTime, int fileCount)
        {
            
            StartRange = startRange;
            EndRange = endRange;
            DownloadStartTime = downloadStartTime;
            NumberOfFiles = fileCount ;
            ProgressType = ProgressType.NewFile;


        }

        public Progress(DateTime startRange, DateTime endRange, DateTime downloadStartTime,  
            int fileCount,  DateTime downloadEndTime,
            string fileName)
        {
            StartRange = startRange;
            EndRange = endRange;
            DownloadStartTime = downloadStartTime;
            DownloadEndTime = downloadEndTime ;
            NumberOfFiles = fileCount;
            FileName = fileName;
            ProgressType = ProgressType.Update;
        }
    
        public string LastMessage { get; }
        public string FileDownloaded { get; }

        public DateTime StartRange { get; }
        public DateTime EndRange { get; }
        public DateTime DownloadStartTime{ get; }
        public DateTime DownloadEndTime { get; }

        public int NumberOfFiles { get; }
        public string FileName { get; internal set; }
    }
}
