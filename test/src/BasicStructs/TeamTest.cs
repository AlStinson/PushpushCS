using NUnit.Framework;
using Pushpush;

using static Pushpush.Team;


public class TeamTest
{
  [Test]
  public void NoneTest()
  {
    Assert.IsTrue(None.isNone);
    Assert.IsFalse(None.isTeam);
  }
  [Test]
  public void WhiteTest()
  {
    Assert.IsTrue(White.isWhite);
    Assert.IsTrue(White.isTeam);
  }
  [Test]
  public void BlackTest()
  {
    Assert.IsTrue(Black.isBlack);
    Assert.IsTrue(Black.isTeam);
  }
}