using System;
using System.ComponentModel.DataAnnotations;
using Caron.FileFormats.Gerber.Parametric;
using Caron.FileFormats.Gerber;
using Newtonsoft.Json;

namespace Caron.FileFormats.Gerber.Parametric.Coalescend
{
    public class GerberCommandXY : GerberCommand, IEquatable<GerberCommandXY>
    {
        public bool Equals(GerberCommandXY? other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (ReferenceEquals(null, other.X)) return false;
            if (ReferenceEquals(null, other.Y)) return false;

            if (ReferenceEquals(null, X)) return false;
            if (ReferenceEquals(null, Y)) return false;

            if (ReferenceEquals(this, other)) return true;

            return X.Value.Equals(other.X.Value) && Y.Value.Equals(other.Y.Value);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GerberCommandXY)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X?.Value ?? 0, Y?.Value ?? 0);
        }

        public static bool operator ==(GerberCommandXY? left, GerberCommandXY? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GerberCommandXY? left, GerberCommandXY? right)
        {
            return !Equals(left, right);
        }

        public GerberCommandX X { get; set; }

        public GerberCommandY Y { get; set; }

        public override string ToString()
        {
            return $"[X,Y] = [{X?.Value}, {Y?.Value}]";
        }
    }
}
