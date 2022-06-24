﻿using Auction.Core.Entities;

namespace Auction.Core.Interfaces;

public interface IAuthService
{
    Task<AuctionUser> LogIn(string email, string password);
    Task SignIn(string email, string password, string firstname, string surname);
}