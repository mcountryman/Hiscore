using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData.Binding;
using Hiscore.Core.Exceptions;
using Hiscore.Core.Models;
using Hiscore.Core.Providers;
using Hiscore.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Hiscore.ViewModels {
  public class PlayerStatsViewModel : ReactiveObject, IActivatableViewModel {
    public ViewModelActivator Activator { get; } = new();

    [Reactive] public string Name { get; set; } = "";
    [Reactive] public Mode Mode { get; set; } = Mode.Normal;
    [Reactive] public PlayerStatsState State { get; set; } = PlayerStatsState.Empty;
    [Reactive] public IEnumerable<PlayerSkillViewModel> Skills { get; set; } = Array.Empty<PlayerSkillViewModel>();

    public PlayerStatsViewModel() {
      this.WhenActivated(dispose => {
        var empty = new List<PlayerSkillViewModel>();

        this
          // Wait for `Name` property to change, throttle events, and trim string. 
          .WhenAnyValue(x => x.Name, x => x.Mode)
          .Select(value => (value.Item1?.Trim(), value.Item2))
          .Throttle(TimeSpan.FromMilliseconds(800))
          // Set state to loading
          .ObserveOn(RxApp.MainThreadScheduler)
          .Do(_ => State = PlayerStatsState.Loading)
          // On async scheduler, with 2s timeout
          .ObserveOn(RxApp.TaskpoolScheduler)
          .SelectMany(value => Observable.If(
            () => String.IsNullOrEmpty(value.Item1),
            // Return an empty list
            Observable.Return((PlayerStatsState.Empty, empty)),
            // Get stats and map to `PlayerSkillViewModel`
            Observable
              .FromAsync(cancel => _highScoreProvider.GetStats(value.Item1!, value.Item2, cancel))
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
              Skills = skills;
            }
          )
          .DisposeWith(dispose);
      });
    }

    readonly HighScoreProvider _highScoreProvider = new();
  }
}