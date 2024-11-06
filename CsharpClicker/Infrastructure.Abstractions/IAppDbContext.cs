using CsharpClicker.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsharpClicker.Infrastructure.Abstractions;

public interface IAppDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; }

    DbSet<ApplicationRole> ApplicationRoles { get; }

    DbSet<Boost> Boosts { get; }

    DbSet<UserBoost> UserBoosts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
