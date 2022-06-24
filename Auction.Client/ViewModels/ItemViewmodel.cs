using Auction.Core.Entities;

namespace WebAPI.ViewModels;

public class ItemViewModel
{
    public decimal InitialPrice { get; set; }
    public string Description { get; set; }
    public byte[] Photo { get; set; }
}