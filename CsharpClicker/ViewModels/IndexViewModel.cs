using CsharpClicker.UseCases.GetBoosts;
using CsharpClicker.UseCases.GetCurrentUser;

namespace CsharpClicker.ViewModels;

public class IndexViewModel
{
    public UserDto User { get; init; }

    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
}
