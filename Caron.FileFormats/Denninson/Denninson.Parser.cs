using System;
using System.Linq;
using System.Collections.Generic;

namespace Caron.FileFormats.Denninson
{
    public partial class Denninson
    {
        private static class Parser
        {
            public static List<Section> GetSections(string[] srcDenninson)
            {
                return GetEntity<Section>(srcDenninson, Constants.Headers.Section);
            }

            public static List<Step> GetSteps(string[] srcDenninson)
            {
                return GetEntity<Step>(srcDenninson, Constants.Headers.Step);
            }

            public static List<OverlapZone> GetOverlapsZones(string[] srcDenninson)
            {
                return GetEntity<OverlapZone>(srcDenninson, Constants.Headers.OverlapZone);
            }

            public static List<GeneralAllowance> GetGeneralAllowance(string[] srcDenninson)
            {
                return GetEntity<GeneralAllowance>(srcDenninson, Constants.Headers.GeneralAllowance);
            }

            public static List<SpliceAllowance> GetSpliceAllowance(string[] srcDenninson)
            {
                return GetEntity<SpliceAllowance>(srcDenninson, Constants.Headers.SpliceAllowance);
            }

            public static int[] GetStartIndexParameters(string[] srcDenninson, string header)
            {
                var idxs = Denninson.Parser.GetIndexes(srcDenninson, header);

                if (idxs.Count() == 0)
                {
                    return null;
                }

                var idxLine = idxs.First() - 1;
                var line = srcDenninson[idxLine];

                int[] indexes = Enumerable.Range(0, line.Length)
                                          .Where(x => line[x].Equals(Constants.ParametersSeparator))
                                          .ToArray();

                return indexes;
            }

            public static List<T> GetEntity<T>(string[] srcDenninson, string header) where T : IBuildFromParameters<T>, new()
            {
                var list = new List<T>();

                var idxDenninson = Denninson.Parser.GetIndexes(srcDenninson, header);
                var idxStartParameters = Denninson.Parser.GetStartIndexParameters(srcDenninson, header);

                foreach (var idx in idxDenninson)
                {
                    var parameters = GetParametersFromLine(srcDenninson[idx], idxStartParameters);
                    T entity = new T();
                    list.Add(entity.BuildFromParameters(parameters));
                }

                return list;
            }

            public static string[] GetParametersFromLine(string input, int[] idxStartParameters)
            {
                var indexes = idxStartParameters.Append(input.Length + 1).ToArray();
                int nParameters = indexes.Length - 1;
                var parameters = new string[nParameters];

                for (int i = 0; i < nParameters; i++)
                {
                    int idxStart = indexes[i];
                    int idxStop = indexes[i + 1] - 1;

                    parameters[i] = input.Substring(idxStart, idxStop - idxStart).TrimEnd(' ');
                }

                return parameters;
            }

            private static int[] GetIndexes(string[] source, string pattern)
            {
                return source.Select((code, index) => (code: code, index: index))
                             .Where(x => x.code.Length >= pattern.Length &&
                                    x.code.Substring(0, pattern.Length) == pattern)
                             .Select(x => x.index).ToArray();

            }
        }
    }
}