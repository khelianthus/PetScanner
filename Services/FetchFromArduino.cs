using PetScanner.Models;
using System.Collections.Immutable;

namespace PetScanner.Services;

public class FetchFromArduino
{
    private Pet Pet { get; set; } = new Pet();


    private async Task GetData()
    {
        //make http call to arduino
        //get response
        var response = "";

        //ASP .NET response
        //if (response.IsSuccessStatusCode)
        if (response == "200")
        {
            try
            {
                //map response to objekt
                //ResponseDTO ResponseDTO = new
                //{
                //    Id = response.Id,
                //    Name = response.Name,
                //    TimeStamp = response.TimeStamp,
                //};

                Pet = new Pet
                {
                    Id = response.Id,
                    Name = response.Name,
                    TimeOfScan = response.TimeStamp.ToDateTime(),
                };

                //Add new scan to history
                Pet.ScanHistory.Add(response.TimeStamp.ToDateTime());
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
