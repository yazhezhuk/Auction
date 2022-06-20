using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace Auction.Core.Entities;

public class AuctionUser : IdentityUser, IBaseEntity<string>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal Balance { get; set; }
}