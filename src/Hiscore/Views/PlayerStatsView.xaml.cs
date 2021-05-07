using System.Reactive.Disposables;
using System.Windows;
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
          .OneWayBind(
            ViewModel,
            viewModel => viewModel.IsLoading,
            view => view.PART_Loading.Visibility,
            isLoading => isLoading ? Visibility.Visible : Visibility.Collapsed
          )
          .DisposeWith(dispose);
        
        this
          .OneWayBind(
            ViewModel,
            viewModel => viewModel.IsLoading,
            view => view.PART_Card.Visibility,
            isLoading => !isLoading ? Visibility.Visible : Visibility.Collapsed
          )
          .DisposeWith(dispose);
        
        this
          .OneWayBind(
            ViewModel,
            viewModel => viewModel.OldSchoolSkills,
            view => view.PART_OldSchoolSkills.ItemsSource
          )
          .DisposeWith(dispose);
      });
    }
  }
}