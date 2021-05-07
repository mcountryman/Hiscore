using System.Collections.Generic;

namespace Hiscore.Core.Models {
  /// <summary>
  /// Player statistics.
  /// </summary>
  public interface IPlayerStats {
    /// <summary>
    /// The player skill information.
    /// </summary>
    IEnumerable<IPlayerSkill> Skills { get; }
  }
}