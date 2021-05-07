using System.Reactive.Disposables;
using System.Windows.Controls;
using Hiscore.ViewModels;
using ReactiveUI;

namespace Hiscore.Views {
  public partial class PlayerSkillView {
    public PlayerSkillView() {
      InitializeComponent();

      this.WhenActivated(dispose => {
        // this
        //   .OneWayBind(
        //     ViewModel,
        //     viewModel => viewModel.Icon,
        //     view => view.PART_Icon.Source
        //   )
        //   .DisposeWith(dispose);

        this
          .OneWayBind(
            ViewModel,
            viewModel => viewModel.Name,
            view => view.PART_Skill.Text
          )
          .DisposeWith(dispose);

        this
          .OneWayBind(
            ViewModel,
            viewModel => viewModel.Rank,
            view => view.PART_Rank.Text,
            value => value.ToString("N0")
          )
          .DisposeWith(dispose);

        this
          .OneWayBind(
            ViewModel,
            viewModel => viewModel.Level,
            view => view.PART_Level.Text,
            value => value.ToString("N0")
          )
          .DisposeWith(dispose);

        this
          .OneWayBind(
            ViewModel,
            viewModel => viewModel.Experience,
            view => view.PART_Experience.Text,
            value => value.ToString("N0")
          )
          .DisposeWith(dispose);
      });
    }
  }
}