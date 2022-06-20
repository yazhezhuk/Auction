using Auction.Core.Entities;
using Auction.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Auction.Core.Services;

public class LotService
{
    private readonly ILogger<LotService> _logger; 
    private readonly ILotRepository _lotRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILotBetsRepository _lotBetsRepository;

    public LotService(ILogger<LotService> logger,ILotRepository lotRepository,
        IUserRepository userRepository,
        ILotBetsRepository lotBetsRepository
    )
    {
        _logger = logger;
        _lotRepository = lotRepository;
        _userRepository = userRepository;
        _lotBetsRepository = lotBetsRepository;
    }

    public async Task AddBet(string userId, int lotId, decimal betAmount)
    {
        var user = _userRepository.Get(userId);
        if (user == null) throw new ArgumentException($"Such user does not exist");
        
        var lot = await _lotRepository.Get(lotId);
        if (lot == null) throw new ArgumentException($"Lot with id:{lotId} does not exist");

        if (betAmount <= (lot.MaxBet?.BetAmount ?? lot.MinBet))
            throw new ArgumentException("Your bet lower than previous bet, please bet more");
        
        var bet = new LotBet(userId, lotId, betAmount);
        _lotBetsRepository.Add(bet);
    }
    
    
}