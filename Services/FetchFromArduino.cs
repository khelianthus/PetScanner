using Microsoft.JSInterop;
using PetScanner.Models.DTO;
using System.Text.Json;

namespace PetScanner.Services;

public class FetchFromArduino
{
    private readonly HttpClient httpClient;
    private readonly IJSRuntime jsRuntime;
    private readonly LocalStorageService localStorageService;

    public FetchFromArduino(HttpClient httpClient, IJSRuntime jsRuntime, LocalStorageService localStorageService)
    {
        this.httpClient = httpClient;
        this.jsRuntime = jsRuntime;
        this.localStorageService = localStorageService;
    }

    public async Task <List<TimeResponse>?> GetTimeOfScan()
    {
        string serverIpAddress = "192.168.0.125";
        string serverUrl = $"http://{serverIpAddress}/";

        var scanHistory = await localStorageService.GetScanHistory();

        if (string.IsNullOrEmpty(scanHistory))
        {
            await localStorageService.SetScanHistory();
        }

        httpClient.BaseAddress = new Uri(serverUrl);

        var request = new HttpRequestMessage(HttpMethod.Get, serverUrl);
        request.Headers.Add("Accept", "application/json");
        var response = await httpClient.SendAsync(request);

        try
        {
            string responseData = await response.Content.ReadAsStringAsync();

            var scanResponse = JsonSerializer.Deserialize<ScanResponse>(responseData);

            var timeResponses = scanResponse?.Scans;

            if (timeResponses == null || timeResponses.Count == 0)
            {
                return null;
            }

            var scanJson = JsonSerializer.Serialize(timeResponses);

            var scanHistoryJson = await localStorageService.GetScanHistory();
            
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", "ScanHistory", scanJson);

            return timeResponses; 
        }

        catch (Exception ex)
        {
            throw new HttpRequestException($"Failed to fetch from Arduino. Message: {ex.Message}");
        }
    }
}

