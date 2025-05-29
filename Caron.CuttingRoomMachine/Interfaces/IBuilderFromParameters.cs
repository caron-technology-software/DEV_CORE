using System;

namespace Caron
{
    public interface IBuildFromParameters<T>
    {
        T BuildFromParameters(string[] parameters);
    }
}
