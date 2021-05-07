namespace Hiscore.Core.Models {
  /// <summary>
  /// Player skill information.
  /// </summary>
  public interface IPlayerSkill {
    /// <summary>
    /// The type of skill.
    /// </summary>
    Skill Skill { get; }
    /// <summary>
    /// The global rank.
    /// </summary>
    int Rank { get; }
    /// <summary>
    /// The skill level.
    /// </summary>
    ushort Level { get; }
    /// <summary>
    /// The skill experience points.
    /// </summary>
    long Experience { get; }
  }
}