using Microsoft.JSInterop;

namespace PetScanner.Services;

public class LocalStorageService
{
    private readonly IJSRuntime jsRuntime;

    public LocalStorageService( IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    public async Task<string> GetScanHistory()
    {
        var scanHistory = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "ScanHistory");

        return scanHistory;
    }

    public async Task SetScanHistory()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", "ScanHistory", "");
    }
}

