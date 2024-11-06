using AutoMapper;
using CsharpClicker.DoomainServices;
using CsharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CsharpClicker.UseCases.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public GetCurrentUserQueryHandler(IAppDbContext appDbContext, ICurrentUserAccessor currentUserAccessor, IMapper mapper)
    {
        this.currentUserAccessor = currentUserAccessor;
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();

        var user = await appDbContext.ApplicationUsers
                        .Include(user => user.UserBoosts)
                        .ThenInclude(ub => ub.Boost)
                        .FirstAsync(user => user.Id == userId);

        var userDto = mapper.Map<UserDto>(user);

        userDto.ProfitPerClick = user.UserBoosts.GetProfit();
        userDto.ProfitPerSecond = user.UserBoosts.GetProfit(shouldCalculateAutoBoosts: true);

        return userDto;
    }
}
