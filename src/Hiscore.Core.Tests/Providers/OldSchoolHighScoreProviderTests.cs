﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hiscore.Core.Models;
using Hiscore.Core.Providers.OldSchool;
using NUnit.Framework;

namespace Hiscore.Core.Tests.Providers {
  [TestFixture]
  public class OldSchoolHighScoreProviderTests {
    [Test]
    public async Task TestGetStatsForPlayer() {
      var provider = new OldSchoolHighScoreProvider();
      var stats = await provider.GetStats("le me", Mode.Normal, CancellationToken.None);
      var skills = stats.Skills.ToList();
      
      return;
    }
  }
}