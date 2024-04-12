namespace PetScanner.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }

    public DateTime TimeOfScan { get; set; }

    public List<DateTime> ScanHistory { get; set; } = new List<DateTime>();
}
