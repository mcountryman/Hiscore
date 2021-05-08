using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using Hiscore.Assets;
using Hiscore.Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Hiscore.ViewModels {
  public class PlayerSkillViewModel : ReactiveObject {
    public string Name => _skill.Skill.ToString();
    public int Rank => _skill.Rank;
    public ushort Level => _skill.Level;
    public long Experience => _skill.Experience;

    public string Icon => _skill.Skill switch {
      Skill.Overall => "overall",
      Skill.Agility => "osrs-agility",
      Skill.Attack => "osrs-attack",
      Skill.Defence => "osrs-defense",
      Skill.Strength => "osrs-str",
      Skill.HitPoints => "osrs-hitpoints",
      Skill.Ranged => "osrs-range",
      Skill.Prayer => "osrs-prayer",
      Skill.Magic => "osrs-magic",
      Skill.Cooking => "osrs-cooking",
      Skill.Woodcutting => "osrs-woodcutting",
      Skill.Fletching => "osrs-fletching",
      Skill.Fishing => "osrs-fishing",
      Skill.Firemaking => "osrs-firemaking",
      Skill.Crafting => "osrs-crafting",
      Skill.Smithing => "osrs-smithing",
      Skill.Mining => "osrs-mining",
      Skill.Herblore => "osrs-herblore",
      Skill.Thieving => "osrs-thieving",
      Skill.Slayer => "osrs-slayer",
      Skill.Farming => "osrs-farming",
      Skill.Runecraft => "osrs-runecrafting",
      Skill.Hunter => "osrs-hunter",
      Skill.Construction => "osrs-constructions",
      _ => throw new ArgumentOutOfRangeException()
    };

    public PlayerSkillViewModel(IPlayerSkill skill) {
      _skill = skill;
    }

    readonly IPlayerSkill _skill;
  }
}