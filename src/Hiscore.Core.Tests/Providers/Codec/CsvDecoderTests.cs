using System.Linq;
using Hiscore.Core.Models;
using Hiscore.Core.Providers.Codec;
using NUnit.Framework;

namespace Hiscore.Core.Tests.Providers.OldSchool.Codec {
  [TestFixture]
  public class CsvDecoderTests {
    [Test]
    public void TestDecode() {
      var decoder = new CsvDecoder();
      var decoded = decoder.Decode(TEST_CSV);
      
      var skills = decoded.Skills.ToArray();
      
      Assert.That(skills.First().Skill, Is.EqualTo(Skill.Overall));
      Assert.That(skills.First().Rank, Is.EqualTo(696497));
      Assert.That(skills.First().Level, Is.EqualTo(1515));
      Assert.That(skills.First().Experience, Is.EqualTo(116997516));
      
      Assert.That(skills.Last().Skill, Is.EqualTo(Skill.Construction));
      Assert.That(skills.Last().Rank, Is.EqualTo(753575));
      Assert.That(skills.Last().Level, Is.EqualTo(51));
      Assert.That(skills.Last().Experience, Is.EqualTo(116599));
    }

    const string TEST_CSV = @"696497,1515,116997516
90188,99,15784826
191520,98,12950607
50539,99,22640159
103726,99,24874333
108927,99,20670204
166787,85,3258682
254865,96,10370826
935373,70,753585
-1,1,-1
825200,68,605550
1659878,53,136636
1564679,50,101766
1056267,60,275037
1296125,50,101341
1595446,50,101364
197664,81,2363160
1246027,52,133324
1006796,53,137596
1437536,41,42391
390438,77,1491551
1111215,29,12124
1480633,9,1000
753575,51,116599
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
14980,745
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
4632,750
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
-1,-1
";
  }
}