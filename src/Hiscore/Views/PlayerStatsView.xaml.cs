using System;
using System.Reactive.Disposables;
using System.Windows;
using Hiscore.Models;
using Hiscore.ViewModels;
using ReactiveUI;

namespace Hiscore.Views {
  public partial class PlayerStatsView {
    public PlayerStatsView() {
      InitializeComponent();
      ViewModel = new PlayerStatsViewModel();

      this.WhenActivated(dispose => {
        this
          .Bind(
            ViewModel,
            viewModel => viewModel.Name,
            view => view.PART_Name.Text
          )
          .DisposeWith(dispose);

        this
          .Bind(
            ViewModel,
            viewModel => viewModel.Mode,
            view => view.PART_Mode.SelectedItem
          )
          .DisposeWith(dispose);

        ViewModel
          .WhenAnyValue(viewModel => viewModel.State)
          .Subscribe(state => {
            PART_Empty.Visibility = state == PlayerStatsState.Empty ? Visibility.Visible : Visibility.Collapsed;
            PART_Error.Visibility = state == PlayerStatsState.Error ? Visibility.Visible : Visibility.Collapsed;
            PART_Loading.Visibility = state == PlayerStatsState.Loading ? Visibility.Visible : Visibility.Collapsed;
            PART_NotFound.Visibility = state == PlayerStatsState.NotFound ? Visibility.Visible : Visibility.Collapsed;

            PART_SkillsCard.Visibility = state == PlayerStatsState.Found ? Visibility.Visible : Visibility.Collapsed;
          })
          .DisposeWith(dispose);

        this
          .OneWayBind(
            ViewModel,
            viewModel => viewModel.Skills,
            view => view.PART_Skills.ItemsSource
          )
          .DisposeWith(dispose);
      });
    }
  }
}