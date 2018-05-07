using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Subjects;

using System.Threading.Tasks;
using AITelemetry.Downloader.Options;

namespace AITelemetry.Downloader
{
    public class QueryDataDownloader
    {
        private Subject<Progress> _progressEvents = new Subject<Progress>();
        private DateTime _clock = DateTime.Now;
        private int _successfullyFiledownloads = 0;
        private int _failedFiledownloads = 0;
        private QueryOptions _options;

      
        public QueryDataDownloader(Subject<Progress> progressEvents, QueryOptions opts)
        {
            _progressEvents = progressEvents;
            this._options = opts;
        }

        internal async Task DownloadData()
        {
            try
            {
                TimeSpan timeSpan;
                DateTime startRange, endDate, startClock;
                Initialise(out timeSpan, out startRange, out endDate, out startClock);
                _progressEvents.OnNext(new Progress());
                while (startRange < endDate)
                {
                    DateTime endRange = startRange.Add(timeSpan);
                    await GetNewJson(startRange, endRange);
                    startRange = endRange.AddMilliseconds(1);
                }
                var t2 = DateTime.Now.Subtract(startClock);
                _progressEvents.OnNext(
                        SendProgressCompletion($"Successfully Downloaded Files:" +
                                $" {_successfullyFiledownloads}, Failed files:{_failedFiledownloads}" +
                                $" in {t2.Hours} hours, {t2.Minutes} minutes and {t2.Seconds} seconds"));
            }
            catch (Exception ex)
            {
                _progressEvents.OnError(ex);
            }
        }

        private void Initialise(out TimeSpan timeSpan, out DateTime startRange, out DateTime endDate, out DateTime startClock)
        {
            if (!Directory.Exists(_options.FolderPath))
            {
                Directory.CreateDirectory(_options.FolderPath);
            }
            timeSpan = TimeSpan.FromHours(_options.chunkSzie).Subtract(TimeSpan.FromMilliseconds(1));
            startRange = _options.GetStartDateHours();
            endDate = startRange.AddDays(_options.NumberofDays).AddMilliseconds(-1);
            startClock = DateTime.Now;
            _clock = DateTime.Now;
        }

        private Progress SendProgressCompletion(string msg)
        {
            var prog = new Progress(msg);
            prog.ProgressType = ProgressType.Complete;
            return prog;
        }
        public async Task<bool> GetNewJson(DateTime startRange, DateTime endRange)
        {
            //const string APP_URL = "http://api.applicationinsights.io/v1/apps";
            string fileName = string.Format($"AI{startRange.ToString("MMdd-HHmm") + endRange.ToString("-HHmm")}.json");
            string fileToWriteTo = Path.Combine(_options.FolderPath, fileName);
            bool ret = false;
            _progressEvents.OnNext(new Progress(startRange, endRange, _clock, _successfullyFiledownloads + 1));
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("x-api-key", _options.AppInsightKey);
                    client.Timeout = new TimeSpan(0, 10, 0);

                    _progressEvents.OnNext(new Progress(string.Format("Downloading Data between {0} and {1}", startRange.ToString("yyyy-MM-ddTHH:mm:ss"), endRange.ToString("yyyy-MM-ddTHH:mm:ss"))));


                    //string newUrl = string.Format("{0}/{1}/query?timespan={2}/{3}&query={4}", APP_URL, _options.AppInsightId, startDateRange, endDateRange, _options.Query);
                    using (HttpResponseMessage response = await client.GetAsync(_options.GetUrL(startRange, endRange)))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK &&
                            response.Content.Headers.ContentType.MediaType == "application/json")
                        {
                            ret = await Write2File(ret, fileToWriteTo, response);
                        }
                        else
                        {
                            ++_failedFiledownloads;
                            fileName = $"Failed: HTTP Response Code:{response.StatusCode},{response.Content.Headers.ContentType}";
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message == "An error occurred while sending the request.")
                {
                    _progressEvents.OnError(new Exception("Could not connect to Application Insights service. Please check the internet connection."));
                }
                else
                    _progressEvents.OnError(ex);
            }
            catch(Exception exception)
            {
                _progressEvents.OnError(exception);

            }
            finally
            {
                _progressEvents.OnNext(new Progress(startRange, endRange, _clock, _successfullyFiledownloads, DateTime.Now, fileName));

            }
            return ret;
        }

        private async Task<bool> Write2File(bool ret, string fileToWriteTo, HttpResponseMessage response)
        {
            using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
            {
                using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                {
                    await streamToReadFrom.CopyToAsync(streamToWriteTo);
                }
                response.Content = null;

                ++_successfullyFiledownloads;
                ret = true;
            }

            return ret;
        }
    }
}
