using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DynamicData.Binding;
using Hiscore.Core;
using Hiscore.Core.Models;
using Hiscore.Core.Providers;
using Hiscore.Core.Providers.OldSchool;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Hiscore.ViewModels {
  public class PlayerStatsViewModel : ReactiveObject, IActivatableViewModel {
    public ViewModelActivator Activator { get; } = new ViewModelActivator();
    
    [Reactive] public string Name { get; set; } = "";
    [Reactive] public Mode Mode { get; set; } = Mode.Normal;
    [Reactive] public bool IsLoading { get; set; } = false;
    [Reactive] public IEnumerable<PlayerSkillViewModel> OldSchoolSkills { get; set; }

    public PlayerStatsViewModel() {
      this.WhenActivated(dispose => {
        var nameObserver = this
          .WhenValueChanged(x => x.Name)
          .Where(x => !String.IsNullOrEmpty(x))
          .Throttle(TimeSpan.FromMilliseconds(800));

        var skillsObserver = nameObserver
          .SelectMany(GetOldSchoolSkills);

        nameObserver
          .ObserveOn(RxApp.MainThreadScheduler)
          .Subscribe(_ => IsLoading = true)
          .DisposeWith(dispose);

        skillsObserver
          .ObserveOn(RxApp.MainThreadScheduler)
          .Subscribe(skills => {
            IsLoading = false;
            OldSchoolSkills = skills;
          });
      });
    }

    async Task<IEnumerable<PlayerSkillViewModel>> GetOldSchoolSkills(string? name, CancellationToken cancellationToken) {
      try {
        var stats = await _oldSchoolHighScoreProvider.GetStats(name, Mode, cancellationToken);
        return stats.Skills.Select(skill => new PlayerSkillViewModel(skill));
      } catch (Exception ex) {
        Debug.WriteLine(ex.ToString());
        return new List<PlayerSkillViewModel>();
      }
    }

    readonly OldSchoolHighScoreProvider _oldSchoolHighScoreProvider = new();
  }
}