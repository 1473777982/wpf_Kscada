using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace R2R.helper
{
    /// <summary>
    /// SerializeToFile  断电保存
    /// </summary>
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
    }

    public class Program
    {
        public static void Main1()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot"
            };
            string fileName = "WeatherForecast.json";
            string jsonString = JsonSerializer.Serialize(weatherForecast);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine(File.ReadAllText(fileName));
        }
    }
}
// output:
//{"Date":"2019-08-01T00:00:00-07:00","TemperatureCelsius":25,"Summary":"Hot"}
// output:
//{"Date":"2019-08-01T00:00:00-07:00","TemperatureCelsius":25,"Summary":"Hot"}
namespace DeserializeFromFileAsync
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
    }

    public class Program
    {
        public static async Task Main2()
        {
            string fileName = "WeatherForecast.json";
            using (FileStream openStream = File.OpenRead(fileName))
            {
                WeatherForecast weatherForecast =
                await JsonSerializer.DeserializeAsync<WeatherForecast>(openStream);
                Console.WriteLine($"Date: {weatherForecast?.Date}");
                Console.WriteLine($"TemperatureCelsius: {weatherForecast?.TemperatureCelsius}");
                Console.WriteLine($"Summary: {weatherForecast?.Summary}");

            }
        }
    }
    // output:
    //Date: 8/1/2019 12:00:00 AM -07:00
    //TemperatureCelsius: 25
    //Summary: Hot


}





//保存sim变量实时值，尚未完成  json格式
namespace R2R.helper
{
    public class var_Latest
    {
        public static void backup()
        {

        }
        public static void pickup()
        {

        }
    }
}
