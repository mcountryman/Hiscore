using System.Threading;
using System.Threading.Tasks;
using Hiscore.Core.Models;

namespace Hiscore.Core.Providers {
  public interface IHighScoreProvider {
    Task<IPlayerStats> GetStats(string player, Mode mode, CancellationToken cancellationToken);
  }
}