using System;
using System.Linq;
using Hiscore.Core.Models;

namespace Hiscore.Core.Providers.Codec {
  /// <summary>
  /// Decode OSRS player stats from CSV.
  /// </summary>
  public class CsvDecoder {
    /// <summary>
    /// The number of skills.
    /// </summary>
    const int SKILLS_COUNT = 24;

    /// <summary>
    /// Decode player stats from supplied CSV.
    /// </summary>
    /// <param name="content">The CSV content.</param>
    /// <returns>Player statistics</returns>
    public PlayerStats Decode(string content) {
      var lines = content.Split('\n');

      return new PlayerStats {
        Skills = Enumerable.Range(0, SKILLS_COUNT)
          .Select(i => {
            var items = lines[i].Split(',');
            return new PlayerSkill {
              Skill = GetSkill(i),
              Rank = Convert.ToInt32(items[0]),
              Level = Convert.ToUInt16(items[1]),
              Experience = Convert.ToInt64(items[2]),
            } as IPlayerSkill;
          })
      };
    }

    Skill GetSkill(int index) {
      if (index < 0 || index > 23)
        return Skill.Unknown;
      return (Skill) index;
    }
  }
}