using System;
using System.Reactive.Disposables;
using System.Windows;
using Hiscore.Models;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Hiscore.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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
        
        ViewModel
          .WhenAnyValue(viewModel => viewModel.State)
          .Subscribe(state => {
            switch (state) {
              case PlayerStatsState.Error:
                PART_Empty.Visibility = Visibility.Collapsed;
                PART_Error.Visibility = Visibility.Visible;
                PART_Loading.Visibility = Visibility.Collapsed;
                PART_NotFound.Visibility = Visibility.Collapsed;
                
                PART_SkillLabels.Visibility = Visibility.Collapsed;
                PART_OldSchoolSkills.Visibility = Visibility.Collapsed;
                break;
              case PlayerStatsState.Found:
                PART_Empty.Visibility = Visibility.Collapsed;
                PART_Error.Visibility = Visibility.Collapsed;
                PART_Loading.Visibility = Visibility.Collapsed;
                PART_NotFound.Visibility = Visibility.Collapsed;

                PART_SkillLabels.Visibility = Visibility.Visible;
                PART_OldSchoolSkills.Visibility = Visibility.Visible;
                break;
              case PlayerStatsState.Loading:
                PART_Empty.Visibility = Visibility.Collapsed;
                PART_Error.Visibility = Visibility.Collapsed;
                PART_Loading.Visibility = Visibility.Visible;
                PART_NotFound.Visibility = Visibility.Collapsed;
                
                PART_SkillLabels.Visibility = Visibility.Collapsed;
                PART_OldSchoolSkills.Visibility = Visibility.Collapsed;
                break;
              case PlayerStatsState.NotFound:
                PART_Empty.Visibility = Visibility.Collapsed;
                PART_Error.Visibility = Visibility.Collapsed;
                PART_Loading.Visibility = Visibility.Collapsed;
                PART_NotFound.Visibility = Visibility.Visible;
                
                PART_SkillLabels.Visibility = Visibility.Collapsed;
                PART_OldSchoolSkills.Visibility = Visibility.Collapsed;
                break;
              default:
                PART_Empty.Visibility = Visibility.Visible;
                PART_Error.Visibility = Visibility.Collapsed;
                PART_Loading.Visibility = Visibility.Collapsed;
                PART_NotFound.Visibility = Visibility.Collapsed;
                
                PART_SkillLabels.Visibility = Visibility.Collapsed;
                PART_OldSchoolSkills.Visibility = Visibility.Collapsed;
                break;
            }
          });
        
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