using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using OpenCvSharp;

using ProRob;

using Caron.FileFormats.Denninson;
using Caron.FileFormats.Gerber;
using Caron.FileFormats.CutTicket;

namespace Caron
{
    public static partial class FabricPlacement
    {
        internal static void AdjustImage(ref Mat image, float pixelToMm)
        {
            Cv2.Resize(image, image, new Size(image.Width * pixelToMm, image.Height * pixelToMm));
        }

        internal static void AddCross(Mat image, Point point, Scalar color, int lineLength = 15, int shiftX = 0, int shiftY = 0)
        {
            int x = point.X + shiftX;
            int y = point.Y + shiftY;

            var p1 = new Point(x + lineLength, y);
            var p2 = new Point(x, y - lineLength);
            var p3 = new Point(x - lineLength, y);
            var p4 = new Point(x, y + lineLength);

            Cv2.Line(image, p1, p3, color, 4, LineTypes.AntiAlias);
            Cv2.Line(image, p2, p4, color, 4, LineTypes.AntiAlias);
        }

        internal static void AddCross(Mat image, int x, int delta = 50)
        {
            var p1 = new Point(x - delta, image.Height - 1);
            var p2 = new Point(x + delta, image.Height - 1);
            var p0 = new Point(x, image.Height - 1);
            var p3 = new Point(x, image.Height - 1 - delta);

            Cv2.Line(image, p1, p2, Scalar.Yellow, 4, LineTypes.AntiAlias);
            Cv2.Line(image, p0, p3, Scalar.Yellow, 4, LineTypes.AntiAlias);
        }

        internal static void AddButterfly(Mat image, int start, int stop, int height, bool invertButterflies)
        {
            if (invertButterflies)
            {
                AddVerticalLine(image, start, Scalar.Green);
                AddVerticalLine(image, stop, Scalar.Red);
            }
            else
            {
                AddVerticalLine(image, start, Scalar.Red);
                AddVerticalLine(image, stop, Scalar.Green);
            }

            var p1 = new Point(start, image.Height);
            var p2 = new Point(stop, image.Height);
            var p3 = p2 + new Point(0, -height);
            var p4 = p1 + new Point(0, -height);

            Cv2.Line(image, p1, p4, Scalar.DarkGray, 4, LineTypes.AntiAlias);
            Cv2.Line(image, p2, p3, Scalar.DarkGray, 4, LineTypes.AntiAlias);
            Cv2.Line(image, p1, p3, Scalar.DarkGray, 4, LineTypes.AntiAlias);
            Cv2.Line(image, p2, p4, Scalar.DarkGray, 4, LineTypes.AntiAlias);
        }

        internal static void AddVerticalLine(Mat image, int x, Scalar color)
        {
            var p1 = new Point(x, 0);
            var p2 = new Point(x, image.Height);

            Cv2.Line(image, p1, p2, color, 2, LineTypes.AntiAlias);
        }

        internal static void AddLabelsToImage(Mat image, List<LabelPoint> labelPoints)
        {
            const double FontScale = 4;
            const int Thickness = 2;

            foreach (var labelPoint in labelPoints)
            {
                var point = labelPoint.Point;

                var textSize = Cv2.GetTextSize(labelPoint.Label, HersheyFonts.HersheyPlain, FontScale, Thickness, out int _);

                int deltaX = 0;
                int deltaY = 0;

                AddCross(image, point, Scalar.Yellow, lineLength: 15, shiftX: -5, shiftY: 5);

                // X
                if ((point.X + textSize.Width) > image.Width)
                {
                    deltaX = -(point.X + textSize.Width - image.Width) - (int)FontScale * 3;
                }

                // Y
                if ((point.Y - textSize.Height) < 0)
                {
                    deltaY = textSize.Height - point.Y + (int)FontScale * 3;
                }

                point.X += deltaX;
                point.Y += deltaY;

                Cv2.PutText(image, labelPoint.Label, point, HersheyFonts.HersheyPlain, FontScale, Scalar.Red, Thickness, LineTypes.AntiAlias);
            }
        }

        internal static void AddPiecesToImage(Mat image, List<Piece> knifePoints, List<Piece> penPoints, Scalar color)
        {
            penPoints.ForEach(piece =>
            {
                piece.SubPieces.ForEach(subpiece =>
                {
                    for (int idxPoint = 0; idxPoint < subpiece.Points.Count - 1; idxPoint++)
                    {
                        var p1 = subpiece.Points[idxPoint];
                        var p2 = subpiece.Points[idxPoint + 1];

                        Cv2.Line(image, p1, p2, Scalar.Gray, 1, LineTypes.AntiAlias);
                    }

                    if (GerberHelper.IsCurveClosed(subpiece.Points))
                    {
                        var p1 = subpiece.Points.First();
                        var p2 = subpiece.Points.Last();

                        Cv2.Line(image, p1, p2, Scalar.Gray, 1, LineTypes.AntiAlias);
                    }
                });
            });

            knifePoints.ForEach(piece =>
            {
                var points = new List<List<Point>>();

                piece.SubPieces.ForEach(subpiece =>
                {
                    points.Add(subpiece.Points);

                    for (int idxPoint = 0; idxPoint < subpiece.Points.Count - 1; idxPoint++)
                    {
                        var p1 = subpiece.Points[idxPoint];
                        var p2 = subpiece.Points[idxPoint + 1];

                        Cv2.Line(image, p1, p2, Scalar.Gray, 1, LineTypes.AntiAlias);
                    }

                    if (GerberHelper.IsCurveClosed(subpiece.Points))
                    {
                        var p1 = subpiece.Points.First();
                        var p2 = subpiece.Points.Last();

                        Cv2.Line(image, p1, p2, Scalar.Gray, 1, LineTypes.AntiAlias);
                    }
                });

                List<Point> knifeContiguousCurve;

                (knifeContiguousCurve, points) = GerberHelper.FuseCurves(points);

                if (knifeContiguousCurve.Count() > 0)
                {
                    Cv2.FillPoly(image, new List<List<Point>>() { knifeContiguousCurve }, color, LineTypes.AntiAlias);
                }

                points.ForEach(x =>
                {
                    if (GerberHelper.IsCurveClosed(x))
                    {
                        Cv2.FillPoly(image, new List<List<Point>>() { x }, color, LineTypes.AntiAlias);
                    }
                });
            });

            penPoints.ForEach(piece =>
            {
                var listListPoint = GerberHelper.GetPointsInListOfListFormat(piece);

                for (int i = 0; i < listListPoint.Count; i++)
                {
                    for (int j = 0; j < listListPoint[i].Count - 1; j++)
                    {
                        Cv2.Line(image, listListPoint[i][j], listListPoint[i][j + 1], Scalar.Gray, 4, LineTypes.AntiAlias);
                    }
                }
            });
        }

        //MMIx50
        public static List<(double MinX, double MaxX, double MinY, double MaxY)> GetGeometriesBounds(List<Piece> knifePoints)
        {
            var geometriesBounds = new List<(double, double, double, double)>();

            foreach (var piece in knifePoints)
            {
                double minX = double.MaxValue, maxX = double.MinValue;
                double minY = double.MaxValue, maxY = double.MinValue;

                foreach (var subpiece in piece.SubPieces)
                {
                    //calcolo dei valori massimi e minimi per ogni geometria
                    foreach (var point in subpiece.Points)
                    {
                        if (point.X < minX) minX = point.X;
                        if (point.X > maxX) maxX = point.X;
                        if (point.Y < minY) minY = point.Y;
                        if (point.Y > maxY) maxY = point.Y;
                    }
                }

                geometriesBounds.Add((minX, maxX, minY, maxY));
            }

            return geometriesBounds;
        }
        //MMFx50

        internal static void AddPiecesToImageWithoutFilledPoly(Mat image, List<Piece> knifePoints, List<Piece> penPoints, Scalar color, int thickness = 1)
        {
            int i01 = 0;
            penPoints.ForEach(piece =>
            {
                piece.SubPieces.ForEach(subpiece =>
                {
                    for (int idxPoint = 0; idxPoint < subpiece.Points.Count - 1; idxPoint++)
                    {
                        var p1 = subpiece.Points[idxPoint];
                        var p2 = subpiece.Points[idxPoint + 1];

                        //if ((i01 == 22) || (i01 == 21))
                        //{
                            Cv2.Line(image, p1, p2, Scalar.Red, thickness, LineTypes.AntiAlias);
                        //}
                    }
                    i01++;

                    if (GerberHelper.IsCurveClosed(subpiece.Points))
                    {
                        var p1 = subpiece.Points.First();
                        var p2 = subpiece.Points.Last();

                        Cv2.Line(image, p1, p2, Scalar.Red, thickness, LineTypes.AntiAlias);
                    }
                });
            });

            knifePoints.ForEach(piece =>
            {
                var points = new List<List<Point>>();

                piece.SubPieces.ForEach(subpiece =>
                {
                    points.Add(subpiece.Points);

                    for (int idxPoint = 0; idxPoint < subpiece.Points.Count - 1; idxPoint++)
                    {
                        var p1 = subpiece.Points[idxPoint];
                        var p2 = subpiece.Points[idxPoint + 1];

                        Cv2.Line(image, p1, p2, Scalar.Gray, thickness, LineTypes.AntiAlias);
                    }

                    if (GerberHelper.IsCurveClosed(subpiece.Points))
                    {
                        var p1 = subpiece.Points.First();
                        var p2 = subpiece.Points.Last();

                        Cv2.Line(image, p1, p2, Scalar.Gray, thickness, LineTypes.AntiAlias);
                    }
                });
            });

            penPoints.ForEach(piece =>
            {
                var listListPoint = GerberHelper.GetPointsInListOfListFormat(piece);

                for (int i = 0; i < listListPoint.Count; i++)
                {
                    for (int j = 0; j < listListPoint[i].Count - 1; j++)
                    {
                        Cv2.Line(image, listListPoint[i][j], listListPoint[i][j + 1], Scalar.Gray, 4, LineTypes.AntiAlias);
                    }
                }
            });
        }
    }
}