using Hiscore.Core.Models;

namespace Hiscore.Core.Providers.OldSchool {
  public record OldSchoolSkill : IPlayerSkill {
    public Skill Skill { get; init; }
    public int Rank { get; init; }
    public ushort Level { get; init; }
    public long Experience { get; init; }
  }
}