# AI.Downloader
Application Insights Telemetry Downloader

This utility downloads the telemetry data from Microsoft Application Insight repository to your local folder. It uses Application Insights Rest API https://dev.applicationinsights.io/documentation/overview

The utility needs set of command line parameters. To get the help on screen please use

AI.Downloader.exe query help 

Typical parameter for query is as follows.

AI.Downloader.exe query --appInsightId [your application Insight instrumentation key]' --appInsightKey [your application insight keys]  --startDate 2018-05-06 --days 2 --folderPath C:\tfl --aiQuery "requests"

e.g.

AI.Downloader.exe query --appInsightId ffff7474-a3ac-41f6-9dc6-abee95fedfff --appInsightKey jlceq2hpmf7qo6mfo5k4lzpy0diq7dryuwpkjfff --startDate 2018-05-06 --days 2 --folderPath C:\tfl --aiQuery &quot;requests&quot;
