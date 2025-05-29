//MMIx18
using System;

namespace Caron.FileFormats.Gerber
{
    public sealed class GerberUnknownCommand : GerberCommand
    {
        public string Token { get; }

        public GerberUnknownCommand(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Token cannot be null or whitespace.", nameof(token));
            Token = token;
        }
    }
}
