using Hiscore.Core.Models;

namespace Hiscore.Core.Providers {
  public record PlayerSkill : IPlayerSkill {
    public Skill Skill { get; init; }
    public int Rank { get; init; }
    public ushort Level { get; init; }
    public long Experience { get; init; }
  }
}