using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData.Binding;
using Hiscore.Core.Exceptions;
using Hiscore.Core.Models;
using Hiscore.Core.Providers.OldSchool;
using Hiscore.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Hiscore.ViewModels {
  public class PlayerStatsViewModel : ReactiveObject, IActivatableViewModel {
    public ViewModelActivator Activator { get; } = new();

    [Reactive] public string Name { get; set; } = "";
    [Reactive] public Mode Mode { get; set; } = Mode.Normal;
    [Reactive] public PlayerStatsState State { get; set; } = PlayerStatsState.Empty;
    [Reactive] public IEnumerable<PlayerSkillViewModel> OldSchoolSkills { get; set; }

    public PlayerStatsViewModel() {
      this.WhenActivated(dispose => {
        var empty = new List<PlayerSkillViewModel>();

        this
          // Wait for `Name` property to change, throttle events, and trim string. 
          .WhenValueChanged(x => x.Name)
          .Select(name => name?.Trim())
          .Throttle(TimeSpan.FromMilliseconds(800))
          // Set state to loading
          .ObserveOn(RxApp.MainThreadScheduler)
          .Do(_ => State = PlayerStatsState.Loading)
          // On async scheduler, with 2s timeout
          .ObserveOn(RxApp.TaskpoolScheduler)
          .SelectMany(name => Observable.If(
            () => String.IsNullOrEmpty(name),
            // Return an empty list
            Observable.Return((PlayerStatsState.Empty, empty)),
            // Get stats and map to `PlayerSkillViewModel`
            Observable
              .FromAsync(cancel => _oldSchoolHighScoreProvider.GetStats(name!, Mode, cancel))
              .Timeout(TimeSpan.FromSeconds(2))
              .Select(stats => stats.Skills.Select(skill => new PlayerSkillViewModel(skill)).ToList())
              .Select(skills => (PlayerStatsState.Found, skills))
              .Catch((PlayerNotFoundException _) => Observable.Return((PlayerStatsState.NotFound, empty)))
              .Catch((Exception _) => Observable.Return((PlayerStatsState.Error, empty)))
          ))
          // Update `State` and `OldSchoolSkills`
          .ObserveOn(RxApp.MainThreadScheduler)
          .Subscribe(
            value => {
              var (state, skills) = value;

              State = state;
              OldSchoolSkills = skills;
            }
          )
          .DisposeWith(dispose);
      });
    }

    readonly OldSchoolHighScoreProvider _oldSchoolHighScoreProvider = new();
  }
}