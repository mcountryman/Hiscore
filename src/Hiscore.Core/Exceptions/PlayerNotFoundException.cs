using System;

namespace Hiscore.Core.Exceptions {
  public class PlayerNotFoundException : Exception {
    public PlayerNotFoundException(string playerName) : base($"Player `{playerName}` not found!") {}
  }
}