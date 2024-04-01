using common.tag;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace R2R.historyvalues
{
    public class historyValues
    {
        [Measurement("mem_history")]//Measurement
        private class HisPoint
        {
            [Column("tag_name", IsTag = true)] public string tag_name { get; set; }//tags
            [Column("value")] public double Value { get; set; }//fields
            [Column(IsTimestamp = true)] public DateTime Time { get; set; }//time
        }
        public static async void historyValues_to_influx(object obj)
        {
            await influxWrite();
            Mwin.timer_historyValues.Change(3000, Timeout.Infinite);
        }

        public static async Task influxWrite()
        {
            await BasicEventHandler();
        }

        private static Task BasicEventHandler()
        {
            var options = new WriteOptions
            {
                BatchSize = 1,
                FlushInterval = 1000,
                RetryInterval = 2000,
                MaxRetries = 3
            };

            //
            // Write Data
            //
            using (var writeApi = App.influxDBClient.GetWriteApi(options))
            {
                #region   Handle the Events 
                //
                //
                //writeApi.EventHandler += (sender, eventArgs) =>
                //{
                //
                // Set output to console for different events types
                //
                //switch (eventArgs)
                //{
                //// success response from server
                //case WriteSuccessEvent successEvent:
                //    //var dataList = successEvent.LineProtocol.Split(Environment.NewLine.ToCharArray());
                //    Oprations.addLog("WriteSuccessEvent: point was successfully written to InfluxDB");
                //    break;

                //// unhandled exception from server
                //case WriteErrorEvent errorEvent:
                //    Oprations.addLog($"WriteErrorEvent: {errorEvent.Exception.Message}");
                //    break;

                //// retrievable error from server
                //case WriteRetriableErrorEvent error:
                //    Oprations.addLog($"WriteErrorEvent: {error.Exception.Message}");
                //    break;

                //// runtime exception in background batch processing
                //case WriteRuntimeExceptionEvent error:
                //    Oprations.addLog($"WriteRuntimeExceptionEvent: {error.Exception.Message}");
                //    break;
                //}
                //};
                #endregion


                //
                // Write 
                //
                var pointsToWrite = new List<HisPoint>();
                foreach (var item in communicationTag.Current.Dic_ranTags_history)
                {


                        //写入influxDB,需要蒋数据打包后一次性写入，减少通讯消耗
                        #region  point 方式
                        //var points = new List<PointData>
                        //{
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(1L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(2L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(3L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(4L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(5L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(6L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(7L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(8L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(9L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(10L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(11L, WritePrecision.S),
                        //   PointData.Measurement("h2o").Tag("location", "coyote_creek").Field("water_level", 10.0D)
                        //       .Timestamp(12L, WritePrecision.S)
                        //};
                        #endregion


                        //POCO方式
                        try
                        {
                            //pointsToWrite.Add(new HisPoint
                            //{ tag_name = (string)item.Value["name"], Value = Convert.ToDouble(item.Value["value"]), Time = DateTime.UtcNow });
                            pointsToWrite.Add(new HisPoint
                            { tag_name = (string)item.Value.name, Value = Convert.ToDouble(item.Value.value), Time = DateTime.UtcNow });
                        }
                        catch (Exception)
                        {
                        }
                }
                if (pointsToWrite.Count>0)
                {
                    writeApi.WriteMeasurements(pointsToWrite, WritePrecision.Ns, App.bucket, App.org);
                }                
            }
            return Task.CompletedTask;
        }
    }
}
