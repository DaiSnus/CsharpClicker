﻿using AutoMapper;
using CsharpClicker.Infrastructure.Abstractions;
using CsharpClicker.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsharpClicker.UseCases.GetBoosts;

public class GetBoostsQueryHandler : IRequestHandler<GetBoostsQuery, IReadOnlyCollection<BoostDto>>
{
    private IAppDbContext appDbContext;
    private IMapper mapper;

    public GetBoostsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<IReadOnlyCollection<BoostDto>> Handle(GetBoostsQuery request, CancellationToken cancellationToken)
    {
        return await mapper
            .ProjectTo<BoostDto>(appDbContext.Boosts)
            .ToArrayAsync();
    }
}
