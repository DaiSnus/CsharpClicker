﻿using CsharpClicker.Domain;

namespace CsharpClicker.DoomainServices;

public static class BoostProfitCalculationExtensions
{
    public static long GetProfit(this IEnumerable<UserBoost> userBoosts, bool shouldCalculateAutoBoosts = false)
    {
        if (shouldCalculateAutoBoosts)
        {
            return userBoosts
                .Where(ub => ub.Boost.IsAuto)
                .Sum(ub => ub.Quantity * ub.Boost.Profit);
        }

        return 6 + userBoosts
            .Where(ub => !ub.Boost.IsAuto)
            .Sum(ub => ub.Quantity * ub.Boost.Profit);
    }
}
