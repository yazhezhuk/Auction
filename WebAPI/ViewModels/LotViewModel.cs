namespace WebAPI.ViewModels;

public class LotViewModel
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public ItemViewModel Item { get; set; }
}