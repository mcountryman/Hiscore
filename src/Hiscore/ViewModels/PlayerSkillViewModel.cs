using System;
using System.IO;
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

    public BitmapFrame Icon => BitmapFrame.Create((MemoryStream) (_skill.Skill switch {
      Skill.Agility => Icons.OldSchoolAgility,
      Skill.Overall => Icons.Overall,
      Skill.Attack => Icons.OldSchoolAttack,
      Skill.Defence => Icons.OldSchoolDefense,
      Skill.Strength => Icons.OldSchoolStrength,
      Skill.HitPoints => Icons.OldSchoolHitpoints,
      Skill.Ranged => Icons.OldSchoolRange,
      Skill.Prayer => Icons.OldSchoolPrayer,
      Skill.Magic => Icons.OldSchoolMagic,
      Skill.Cooking => Icons.OldSchoolCooking,
      Skill.Woodcutting => Icons.OldSchoolWoodcutting,
      Skill.Fletching => Icons.OldSchoolFletching,
      Skill.Fishing => Icons.OldSchoolFishing,
      Skill.Firemaking => Icons.OldSchoolFiremaking,
      Skill.Crafting => Icons.OldSchoolCrafting,
      Skill.Smithing => Icons.OldSchoolSmithing,
      Skill.Mining => Icons.OldSchoolMining,
      Skill.Herblore => Icons.OldSchoolHerblore,
      Skill.Thieving => Icons.OldSchoolTheiving,
      Skill.Slayer => Icons.OldSchoolSlayer,
      Skill.Farming => Icons.OldSchoolFarming,
      Skill.Runecraft => Icons.OldSchoolRuneCrafting,
      Skill.Hunter => Icons.OldSchoolHunter,
      Skill.Construction => Icons.OldSchoolConstruction,
      _ => throw new ArgumentOutOfRangeException()
    }));

    public PlayerSkillViewModel(IPlayerSkill skill) {
      _skill = skill;
    }

    readonly IPlayerSkill _skill;
  }
}