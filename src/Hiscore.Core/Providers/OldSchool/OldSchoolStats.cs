using System.Collections.Generic;
using Hiscore.Core.Models;

namespace Hiscore.Core.Providers.OldSchool {
  public record OldSchoolStats : IPlayerStats {
    public IEnumerable<IPlayerSkill> Skills { get; init; }
  }
}