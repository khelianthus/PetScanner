using PetScanner.Models;
using System.Collections.Immutable;
using System.IO.Ports;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PetScanner.Services;

public class FetchFromArduino
{

    private readonly HttpClient httpClient;
    private Pet Pet { get; set; } = new Pet();

    public FetchFromArduino(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ScanResult> GetData()
    {
        //var ardo = new SerialPort();
        //ardo.PortName = "COM5";
        //ardo.BaudRate = 9600;

        //using (var serialPort = new SerialPort(ardo.PortName, ardo.BaudRate))
        //{
        //    serialPort.Open();
        //    serialPort.Write(response);
        //};

        string ipAddress = "192.168.0.124";
        string serverUrl = $"http://{ipAddress}/";

        ScanResult scanResult;


        Console.WriteLine(httpClient.BaseAddress);
        httpClient.BaseAddress = new Uri(serverUrl);
        Console.WriteLine("new baseAdress:", httpClient.BaseAddress);
        Console.WriteLine(httpClient.BaseAddress);


        var request = new HttpRequestMessage(HttpMethod.Get, serverUrl);
        request.Headers.Add("Accept", "application/json");

    
        try
        {
            var response = await httpClient.SendAsync(request);
            Console.WriteLine("HELLO");
            Console.WriteLine(response.Content);
            var responseBody = response.Content.ReadAsStringAsync();

            string responseData = await response.Content.ReadAsStringAsync();
            scanResult = JsonSerializer.Deserialize<ScanResult>(responseData);
            return scanResult;

        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR");
            Console.WriteLine(ex.ToString());
        }


        //ASP .NET response
        //if (response.IsSuccessStatusCode)
        //if (response.IsSuccessStatusCode)
        //{
        //    try
        //    {

        //        var responseBody = response.Content.ReadAsStringAsync();
        //        //return JsonSerializer.Deserialize<ScanResult>(responseBody);
        //        //map response to objekt
        //        //ResponseDTO ResponseDTO = new
        //        //{
        //        //    Id = response.Id,
        //        //    Name = response.Name,
        //        //    TimeStamp = response.TimeStamp,
        //        //};

        //        ////Pet = new Pet
        //        ////{
        //        ////    Id = response.Id,
        //        ////    Name = response.Name,
        //        ////    TimeOfScan = response.TimeStamp.ToDateTime(),
        //        ////};

        //        //////Add new scan to history
        //        ////Pet.ScanHistory.Add(response.TimeStamp.ToDateTime());

        //        return await responseBody;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new HttpRequestException($"Failed to fetch data. Status code: {response.StatusCode}"); 
        //    }
        //}
        //Console.WriteLine("Error: " + response.StatusCode);
        return null; ;
    }
}

public class ScanResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("time")]
    public int Time { get; set; }
}
