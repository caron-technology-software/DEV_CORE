using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob
{
    public class ApplicationVersion : IEquatable<ApplicationVersion>
    {
        public int Major { get; private set; } = 0;
        public int Minor { get; private set; } = 0;
        public int Patch { get; private set; } = 0;
        public string Build { get; private set; } = String.Empty;

        public ApplicationVersion(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Build = ApplicationInfo.CommitHash;
        }

        public override string ToString()
        {
            if (Major==0 && Minor==0 && Patch==0)
            {
                return $"Build: {Build}";
            }

            return $"{Major}.{Minor}.{Patch} ({Build})";
        }

        #region IEquatable
        public override bool Equals(object obj)
        {
            return Equals(obj as ApplicationVersion);
        }

        public bool Equals(ApplicationVersion other)
        {
            return other != null &&
                   Major == other.Major &&
                   Minor == other.Minor &&
                   Patch == other.Patch &&
                   Build == other.Build;
        }

        public static bool operator ==(ApplicationVersion left, ApplicationVersion right)
        {
            return EqualityComparer<ApplicationVersion>.Default.Equals(left, right);
        }

        public static bool operator !=(ApplicationVersion left, ApplicationVersion right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
