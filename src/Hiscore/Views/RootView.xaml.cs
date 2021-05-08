using Hiscore.ViewModels;

namespace Hiscore {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class RootView {
    public RootView() {
      InitializeComponent();
      ViewModel = new RootViewModel();
    }
  }
}