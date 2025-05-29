using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

using OpenCvSharp;

using ProRob.Extensions.Object;

namespace Caron.FileFormats.Gerber
{
    public static class GerberHelper
    {
        public static bool IsCurveClosed(List<Point> curve)
        {
            if (curve.Last() == curve.First())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<LabelPoint> GetLabelsFromSectionLimits(List<LabelPoint> labels, int sectionStart, int sectionEnd)
        {
            return labels.Clone().Where(x => x.Point.X >= sectionStart && x.Point.Y <= sectionEnd).ToList();
        }

        public static List<Piece> GetPiecesFromSectionLimits(List<Piece> pieces, int sectionStart, int sectionEnd)
        {
            List<Piece> p = null;

#if TRUE
            {
                p = new List<Piece>();

                for (int idxPiece = 0; idxPiece < pieces.Count; idxPiece++)
                {
                    var piece = pieces[idxPiece].Clone();

                    var x = piece.SubPieces.SelectMany(x => x.Points.Select(x => x.X));

                    if (x.All(x => x >= sectionStart) && x.Any(x => x <= sectionEnd))
                    {
                        p.Add(piece);
                    }
                }
            }
#else
            {
                p = pieces.Clone();

                for (int i = 0; i < p.Count; i++)
                {
                    for (int j = 0; j < p[i].SubPieces.Count; j++)
                    {
                        for (int k = p[i].SubPieces[j].Points.Count - 1; k >= 0; k--)
                        {
                            var point = p[i].SubPieces[j].Points[k];

                            if (!(point.X >= sectionStart && point.X <= sectionEnd))
                            {
                                //Console.WriteLine("POINT: REMOVED");
                                p[i].SubPieces[j].Points.RemoveAt(k);
                            }
                            {
                                //Console.WriteLine("POINT: MAINTAINED");
                            }
                        }
                    }
                }

                //Rimozione piece/subpiece
                for (int i = p.Count - 1; i >= 0; i--)
                {
                    for (int j = p[i].SubPieces.Count - 1; j >= 0; j--)
                    {
                        int n1 = p[i].SubPieces[j].Points.Count;
                        int n2 = pieces[i].SubPieces[j].Points.Count();

                        //Console.WriteLine($"n1={n1} - n2={n2}");

                        if (n1 != n2)
                        {
                            //Console.WriteLine("REMOVE");
                            p[i].SubPieces.RemoveAt(j);
                        }
                    }

                    if (p[i].SubPieces.Count == 0)
                    {
                        p.RemoveAt(i);
                    }
                }
            }
#endif
            //Console.WriteLine("END");
            return p;
        }

        public static (int, int) GetMaxDimensions(List<Piece> knifePieces, List<Piece> penPieces)
        {
            Point maxPointPen;
            Point maxPointKnife;
            IEnumerable<int> elems;

            var knifeEnumerable = knifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points);
            var penEnumerable = penPieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points);

            elems = knifeEnumerable.Select(x => x.X);
            maxPointKnife.X = (elems.Count() == 0) ? 0 : (int)elems.Max();

            elems = knifeEnumerable.Select(x => x.Y);
            maxPointKnife.Y = (elems.Count() == 0) ? 0 : (int)elems.Max();

            elems = penEnumerable.Select(x => x.X);
            maxPointPen.X = (elems.Count() == 0) ? 0 : (int)elems.Max();

            elems = penEnumerable.Select(x => x.Y);
            maxPointPen.Y = (elems.Count() == 0) ? 0 : (int)elems.Max();

            return (Math.Max(maxPointKnife.X, maxPointPen.X), Math.Max(maxPointKnife.Y, maxPointPen.Y));
        }

        public static (int, int) GetMinDimensions(List<Piece> knifePieces, List<Piece> penPieces)
        {
            Point minPointPen;
            Point minPointKnife;

            IEnumerable<int> elems;

            var knifeEnumerable = knifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points);
            var penEnumerable = penPieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points);

            elems = knifeEnumerable.Select(x => x.X);
            minPointKnife.X = (elems.Count() == 0) ? 0 : (int)elems.Min();

            elems = knifeEnumerable.Select(x => x.Y);
            minPointKnife.Y = (elems.Count() == 0) ? 0 : (int)elems.Min();

            elems = penEnumerable.Select(x => x.X);
            minPointPen.X = (elems.Count() == 0) ? 0 : (int)elems.Min();

            elems = penEnumerable.Select(x => x.Y);
            minPointPen.Y = (elems.Count() == 0) ? 0 : (int)elems.Min();

            return (Math.Min(minPointKnife.X, minPointPen.X), Math.Min(minPointKnife.Y, minPointPen.Y));
        }

        public static (List<Point>, List<List<Point>>) FuseCurves(in List<List<Point>> curves)
        {
            //var sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            int nCurves = curves.Count();

            if (nCurves <= 1)
            {
                return (new List<Point>(), curves);
            }

            var curvesToFuse = new List<Point>();
            var curvesToSave = new List<List<Point>>();

            var isContigued = CheckGerberContinuity(curves).ToList();

            int startIdx = isContigued.FindIndex(x => x == true);

            /*var idxToFuse = isContigued.Where(x => x == true).TakeWhile(x => x == true).Skip(1).Select((x,y)=>y).ToList();

            if (idxToFuse.Count() < 2)
            {
                return (new List<Point>(), curves);
            }*/

            var idxToFuse = new List<int>();

            // Massimo intervallo
            if (startIdx != -1)
            {
                int idx = startIdx;

                while (idx < nCurves)
                {
                    if (isContigued[idx] == true)
                    {
                        idxToFuse.Add(idx);
                        idxToFuse.Add(idx + 1);
                    }
                    else
                    {
                        break;
                    }

                    idx++;
                }
            }
            else
            {
                return (new List<Point>(), curves);
            }

            idxToFuse = idxToFuse.Distinct().ToList();

            var idxToSave = Enumerable.Range(0, nCurves).Except(idxToFuse).ToList();

            foreach (var i in idxToFuse)
            {
                curvesToFuse.AddRange(curves[i]);
            }

            foreach (var i in idxToSave)
            {
                curvesToSave.Add(curves[i]);
            }

            //Console.WriteLine($"[FuseCurves] elapsed time: {sw.Elapsed} ms");
            //Console.WriteLine($"FuseCurves: PRE={nCurves} POST={curvesToSave.Count()+1}");

            return (curvesToFuse, curvesToSave);
        }

        public static bool[] CheckGerberContinuity(in List<List<Point>> curves)
        {
            int nc = curves.Count;

            bool[] isContigued = new bool[nc];

            if (nc <= 1)
            {
                return isContigued;
            }

            var ranges = Enumerable.Range(0, nc).ToArray();

            for (int i = 0; i < nc - 1; i++)
            {
                var c1 = curves[ranges[i]];
                var c2 = curves[ranges[i + 1]];

                isContigued[i] = CheckContinuity(c1, c2);
            }

            return isContigued;

            bool CheckContinuity(in List<Point> input1, in List<Point> input2)
            {
                var n1 = input1.Count();
                var n2 = input2.Count();

                //if (n1 <=2 || n2 <=2)
                //{
                //    return true;
                //}

                if (
                    (input1[n1 - 2].Equals(input2[0])) ||
                    (input1[n1 - 1].Equals(input2[0]))
                   )
                {
                    return true;
                }
                else if (
                         (input1[0].Equals(input2[n2 - 2])) ||
                         (input1[0].Equals(input2[n2 - 1]))
                        )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void PrintPointsPerPiece(List<Piece> pieces)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                Console.WriteLine($"Piece {i}");
                for (int j = 0; j < pieces[i].SubPieces.Count(); j++)
                {
                    for (int k = 0; k < pieces[i].SubPieces[j].Points.Count; k++)
                    {
                        var p = pieces[i].SubPieces[j].Points[k];
                        Console.WriteLine($"\t{p}");
                    }
                }
            }
        }

        public static void RescalePiecesPoints(List<Piece> pieces, double scalingFactor)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                for (int j = 0; j < pieces[i].SubPieces.Count(); j++)
                {
                    for (int k = 0; k < pieces[i].SubPieces[j].Points.Count; k++)
                    {
                        var p = pieces[i].SubPieces[j].Points[k];

                        p.X = (p.X == 0) ? 0 : Convert.ToInt32((double)p.X / scalingFactor);
                        p.Y = (p.Y == 0) ? 0 : Convert.ToInt32((double)p.Y / scalingFactor);

                        pieces[i].SubPieces[j].Points[k] = p;

                    }
                }
            }
        }

        public static void ApplyScale(List<LabelPoint> labelPoints, float scale)
        {
            for (int i = 0; i < labelPoints.Count; i++)
            {
                var labelPoint = labelPoints[i];
                var point = labelPoint.Point;

                point.X = (int)((float)point.X * scale);
                point.Y = (int)((float)point.Y * scale);

                labelPoint.Point = point;
                labelPoints[i] = labelPoint;
            }
        }

        public static void ApplyScale(List<Point> points, float scale)
        {
            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];

                p.X = (int)((float)p.X * scale);
                p.Y = (int)((float)p.Y * scale);

                points[i] = p;
            }
        }

        public static void ApplyScale(List<Piece> pieces, float scale)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                for (int j = 0; j < pieces[i].SubPieces.Count(); j++)
                {
                    for (int k = 0; k < pieces[i].SubPieces[j].Points.Count; k++)
                    {
                        var p = pieces[i].SubPieces[j].Points[k];

                        p.X = (int)((float)p.X * scale);
                        p.Y = (int)((float)p.Y * scale);

                        pieces[i].SubPieces[j].Points[k] = p;
                    }
                }
            }
        }

        public static void RotateLabelsAxisX(List<LabelPoint> labelPoints, int maxX)
        {
            for (int i = 0; i < labelPoints.Count; i++)
            {
                var p = labelPoints[i].Point;

                p.X = maxX - p.X;

                labelPoints[i].Point = p;
            }
        }

        public static void RotateLabelsAxisY(List<LabelPoint> labelPoints, int maxY)
        {
            for (int i = 0; i < labelPoints.Count; i++)
            {
                var p = labelPoints[i].Point;

                p.Y = maxY - p.Y;

                labelPoints[i].Point = p;
            }
        }

        public static void RotatePiecesAxisX(List<Piece> pieces, int maxX)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                for (int j = 0; j < pieces[i].SubPieces.Count(); j++)
                {
                    for (int k = 0; k < pieces[i].SubPieces[j].Points.Count; k++)
                    {
                        var p = pieces[i].SubPieces[j].Points[k];

                        p.X = maxX - p.X;

                        pieces[i].SubPieces[j].Points[k] = p;
                    }
                }
            }
        }

        public static void RotatePiecesAxisY(List<Piece> pieces, int maxY)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                for (int j = 0; j < pieces[i].SubPieces.Count(); j++)
                {
                    for (int k = 0; k < pieces[i].SubPieces[j].Points.Count; k++)
                    {
                        var p = pieces[i].SubPieces[j].Points[k];

                        p.Y = maxY - p.Y;

                        pieces[i].SubPieces[j].Points[k] = p;
                    }
                }
            }
        }

        public static void Rotate180(List<Piece> knifePieces, List<Piece> penPieces, List<LabelPoint> labelPoints)
        {
            (int maxX, int maxY) = GerberHelper.GetMaxDimensions(knifePieces, penPieces);

            RotatePiecesAxisX(knifePieces, maxX);
            RotatePiecesAxisX(penPieces, maxX);
            RotateLabelsAxisX(labelPoints, maxX);

            RotatePiecesAxisY(knifePieces, maxY);
            RotatePiecesAxisY(penPieces, maxY);
            RotateLabelsAxisY(labelPoints, maxY);
        }

        public static void ApplyLimits(List<Piece> pieces, Size size, int delta)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                for (int j = 0; j < pieces[i].SubPieces.Count(); j++)
                {
                    for (int k = 0; k < pieces[i].SubPieces[j].Points.Count; k++)
                    {
                        var p = pieces[i].SubPieces[j].Points[k];

                        if (p.X >= size.Width)
                        {
                            p.X = size.Width - delta;
                        }

                        if (p.X < delta)
                        {
                            p.X = delta;
                        }

                        if (p.Y >= size.Height)
                        {
                            p.Y = size.Height - delta;
                        }

                        if (p.Y < delta)
                        {
                            p.Y = delta;
                        }

                        pieces[i].SubPieces[j].Points[k] = p;
                    }
                }
            }
        }

        public static void ApplyOffset(List<LabelPoint> labels, Point offset)
        {
            for (int i = 0; i < labels.Count; i++)
            {
                var p = labels[i].Point;

                p.X = p.X + offset.X;
                p.Y = p.Y + offset.Y;

                labels[i].Point = p;
            }
        }

        public static void ApplyOffset(List<Piece> pieces, Point offset)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                for (int j = 0; j < pieces[i].SubPieces.Count(); j++)
                {
                    for (int k = 0; k < pieces[i].SubPieces[j].Points.Count; k++)
                    {
                        var p = pieces[i].SubPieces[j].Points[k];

                        p.X = p.X + offset.X;
                        p.Y = p.Y + offset.Y;

                        pieces[i].SubPieces[j].Points[k] = p;
                    }
                }
            }
        }

        public static List<List<Point>> GetPointsInListOfListFormat(Piece piece)
        {
            var points = new List<List<Point>>();

            piece.SubPieces.ForEach(subpiece =>
            {
                points.Add(subpiece.Points);
            });

            return points;
        }

        public static List<List<Point>> GetPointsInListOfListFormat(List<Piece> pieces)
        {
            var points = new List<List<Point>>();

            pieces.ForEach(piece =>
            {
                piece.SubPieces.ForEach(subpiece =>
                {
                    points.Add(subpiece.Points);
                });
            });

            return points;
        }

    }
}