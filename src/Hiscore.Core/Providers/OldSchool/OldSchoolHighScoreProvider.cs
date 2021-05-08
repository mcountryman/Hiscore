using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Hiscore.Core.Exceptions;
using Hiscore.Core.Models;
using Hiscore.Core.Providers.OldSchool.Codec;

namespace Hiscore.Core.Providers.OldSchool {
  public class OldSchoolHighScoreProvider : IHighScoreProvider {
    const string URL = "https://secure.runescape.com";
    const string PATH_STATS = "index_lite.ws";
    const string PATH_SCORES = "overall.ws";

    public Game Game => Game.OldSchoolRunescape;
    
    public OldSchoolHighScoreProvider() {
      _http = new HttpClient();
      _csvDecoder = new CsvDecoder();
    }
    
    public async Task<IPlayerStats> GetStats(string playerName, Mode mode, CancellationToken cancellationToken) {
      var uri = GetUri(URL, GetModeSegment(mode), PATH_STATS + $"?player={playerName}");
      var response = await _http.GetAsync(uri, cancellationToken);
      if (response.StatusCode == HttpStatusCode.NotFound)
        throw new PlayerNotFoundException(playerName);
      
      var content = await response.Content.ReadAsStringAsync(cancellationToken);
      return _csvDecoder.Decode(content);
    }

    Uri GetUri(params string[] parts) {
      return new(string.Join('/', parts));
    }

    string GetModeSegment(Mode type) {
      return type switch {
        Mode.None => "m=hiscore_oldschool",
        Mode.Normal => "m=hiscore_oldschool",
        Mode.IronMan => "m=hiscore_oldschool_ironman",
        Mode.Ultimate => "m=hiscore_oldschool_ultimate",
        Mode.Hardcore => "m=hiscore_oldschool_hardcore_ironman",
        Mode.DeadMan => "m=hiscore_oldschool_deadman",
        Mode.Seasonal => "m=hiscore_oldschool_seasonal",
        Mode.Tournament => "m=hiscore_oldschool_tournament",
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Unexpected PlayerType `{type}`")
      };
    }

    readonly HttpClient _http;
    readonly CsvDecoder _csvDecoder;
  }
}