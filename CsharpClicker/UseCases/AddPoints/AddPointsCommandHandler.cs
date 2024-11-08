﻿using CsharpClicker.DoomainServices;
using CsharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsharpClicker.UseCases.AddPoints;

public class AddPointsCommandHandler : IRequestHandler<AddPointsCommand, Unit>
{
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IAppDbContext appDbContext;

    public AddPointsCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
    {
        this.currentUserAccessor = currentUserAccessor;
        this.appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(AddPointsCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();
        var user = await appDbContext.ApplicationUsers
                            .Include(user => user.UserBoosts)
                            .ThenInclude(boosts => boosts.Boost)
                            .FirstAsync();

        var points = user.UserBoosts.GetProfit(shouldCalculateAutoBoosts: request.IsAuto) * request.Times;

        user.CurrentScore += points;
        user.RecordScore += points;

        await appDbContext.SaveChangesAsync();

        return Unit.Value;
    }
}
