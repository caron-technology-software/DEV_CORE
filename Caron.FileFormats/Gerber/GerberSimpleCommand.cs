//MMIx18
using System;
using System.Linq;

namespace Caron.FileFormats.Gerber
{
    public abstract class GerberSimpleCommand : GerberCommand
    {
        public string Token { get; }
        public abstract string Kind { get; }

        /// <summary>
        /// Gets or sets the commands following the current one in the same block
        /// </summary>
        public string? TrailingContent { get; protected set; }


        protected GerberSimpleCommand(string token, bool computeTrailingContent = true)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Token cannot be null or whitespace.", nameof(token));

            Token = token;

            if (Token.Length > Kind.Length && computeTrailingContent)
            {//il primo valore viene saltato perchè identifica il tipo di comando es. H , M30 ...
                var right = token.Skip(1).ToList();
                if (right.Count > 0)
                {
                    TrailingContent = new string(right.ToArray());
                }
            }
        }

        public override string ToString()
        {
            return Kind ?? base.ToString()!;
        }
    }
}
