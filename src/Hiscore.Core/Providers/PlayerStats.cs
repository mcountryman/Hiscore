using System.Collections.Generic;
using Hiscore.Core.Models;

namespace Hiscore.Core.Providers {
  public record PlayerStats : IPlayerStats {
    public IEnumerable<IPlayerSkill> Skills { get; init; }
  }
}