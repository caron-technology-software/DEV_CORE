using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using Cv = OpenCvSharp;

namespace ProRob
{
    public class Drawing
    {
        public const int DeltaAntiOverlap = 1;
        public const int TriangleBase = 8;
        public const int ThicknessBarredRectangle = 1;
        public const int DeltaBarredRectangle = 3;

        public static int ScalePoint(int point, int length, int imageSize)
        {
            return (int)((float)point / (float)length * (float)imageSize);
        }

        public static void PutText(Mat img, string text, Cv.Point point, double fontScale, Scalar color, int fontThickness, bool isRighMachine)
        {
            if (isRighMachine)
            {
                Cv2.Flip(img, img, FlipMode.Y);

                //Considera la dimensione del testo
                var textSize = Cv2.GetTextSize(text, HersheyFonts.HersheyPlain, fontScale, fontThickness, out _);

                point.X = img.Width - point.X - textSize.Width;
            }

            Cv2.PutText(img, text, point, HersheyFonts.HersheyPlain, fontScale, color, fontThickness, LineTypes.AntiAlias);

            if (isRighMachine)
            {
                Cv2.Flip(img, img, FlipMode.Y);
            }
        }

        public static void DrawCenteredText(Mat img, int position, int tableLength, string text, Scalar color, bool isRighMachine)
        {
            const int Shift = 5;
            const double FontScale = 0.9;
            const int FontThickness = 1;

            int x = (int)((float)position / (float)tableLength * (float)(img.Width - 1)) + Shift;
            int y = img.Height / 2;
            var p = new Cv.Point(x, y);

            PutText(img, text, p, FontScale, color, FontThickness, isRighMachine);
        }

        public static void DrawText(Mat img, int position, int tableLength, string text, Scalar color, bool isRighMachine, bool upper = true)
        {
            const int Shift = 5;
            const double FontScale = 0.9;
            const int FontThickness = 1;

            int x = (int)((float)position / (float)tableLength * (float)(img.Width - 1)) + Shift;
            int y = upper ? img.Height / 8 : img.Height - img.Height / 8 + img.Height / 16 - 1;
            var p = new Cv.Point(x, y);

            PutText(img, text, p, FontScale, color, FontThickness, isRighMachine);
        }

        public static void DrawLine(Mat img, in int position, Scalar color, int length, int thickness = 1)
        {
            int p = (int)((float)position / (float)length * (float)(img.Width - 1));

            var p1 = new Cv.Point(p, 0);
            var p2 = new Cv.Point(p, img.Height - 1);
            Cv2.Line(img, p1, p2, color, thickness, LineTypes.AntiAlias);
        }

        public static void DrawLine(Mat img, int point, Scalar color)
        {
            var p1 = new Cv.Point(point, 0);
            var p2 = new Cv.Point(point, img.Height - 1);
            Cv2.Line(img, p1, p2, color, 1, LineTypes.AntiAlias);
        }

        public static void DrawRectangle(Mat img, in int start, in int stop, in int length, Scalar borderColor, in int deltaHeight = 0)
        {
            int thickness = 1;
            bool useDeltaAntiOverlap = true;

            int pStart = (int)((float)start / (float)length * (float)(img.Width - 1));
            int pStop = (int)((float)stop / (float)length * (float)(img.Width - 1));

            if (pStart > pStop)
            {
                int tmp = pStart;
                pStart = pStop;
                pStop = tmp;
            }

            if (useDeltaAntiOverlap)
            {
                //Per evitare overlap
                pStart += DeltaAntiOverlap;
                pStop -= DeltaAntiOverlap;
            }

            var p1 = new Cv.Point(pStart, 0 + deltaHeight);
            var p2 = new Cv.Point(pStop, img.Height - 1 - deltaHeight);

            //Cv2.Rectangle(img, p1, p2, internalColor, -1, LineTypes.AntiAlias);
            Cv2.Rectangle(img, p1, p2, borderColor, thickness, LineTypes.AntiAlias);
        }

        public static void DrawRectangleWithButterfly(Mat img, in int start, in int stop, in int length, Scalar borderColor, in int deltaHeight = 0)
        {
            int thickness = 1;
            bool useDeltaAntiOverlap = true;

            int pStart = (int)((float)start / (float)length * (float)(img.Width - 1));
            int pStop = (int)((float)stop / (float)length * (float)(img.Width - 1));

            if (pStart > pStop)
            {
                int tmp = pStart;
                pStart = pStop;
                pStop = tmp;
            }

            if (useDeltaAntiOverlap)
            {
                //Per evitare overlap
                pStart += DeltaAntiOverlap;
                pStop -= DeltaAntiOverlap;
            }

            var p1 = new Cv.Point(pStart, 0 + deltaHeight);
            var p2 = new Cv.Point(pStop, img.Height - 1 - deltaHeight);

            var p1d = new Cv.Point(pStart, img.Height - 1 - deltaHeight);
            var p2u = new Cv.Point(pStop, 0 + deltaHeight);

            Cv2.Line(img, p1, p2, borderColor, thickness, LineTypes.AntiAlias);
            Cv2.Line(img, p1d, p2u, borderColor, thickness, LineTypes.AntiAlias);

            Cv2.Rectangle(img, p1, p2, borderColor, thickness, LineTypes.AntiAlias);

        }

        ////////public static void DrawRectangle(Mat img, int start, int stop, Scalar internalColor, Scalar linesColor, int thickness = 2, bool useDeltaAntiOverlap = true)
        ////////{
        ////////    if (useDeltaAntiOverlap)
        ////////    {
        ////////        //Per evitare overlap
        ////////        start = start + DeltaAntiOverlap;
        ////////        stop = stop - DeltaAntiOverlap;
        ////////    }

        ////////    var p1 = new Cv.Point(start, 0);
        ////////    var p2 = new Cv.Point(stop, img.Height - 1);

        ////////    Cv2.Rectangle(img, p1, p2, internalColor, -1, LineTypes.AntiAlias);
        ////////    Cv2.Rectangle(img, p1, p2, linesColor, thickness, LineTypes.AntiAlias);
        ////////}

        public static void DrawTriangle(Mat img, int point, Scalar color)
        {
            //Spazio tra borbo superiore/inferiore
            const int BorderSpace = 1;

            //ClockWise points
            var p1 = new Cv.Point(point - TriangleBase, 0 + BorderSpace);
            var p2 = new Cv.Point(point + TriangleBase, 0 + BorderSpace);
            var p3 = new Cv.Point(point, img.Height - 1 - BorderSpace);

            var points = new List<Cv.Point>() { p1, p2, p3 };

            Cv2.FillConvexPoly(img, InputArray.Create(points), color, LineTypes.AntiAlias);
        }

        public static void DrawTriangle(Mat img, in int position, in int length)
        {
            int p = (int)((float)position / (float)length * (float)(img.Width - 1));

            //Spazio tra borbo superiore/inferiore
            const int BorderSpace = 1;

            //ClockWise points
            var p1 = new Cv.Point(p - TriangleBase, 0 + BorderSpace);
            var p2 = new Cv.Point(p + TriangleBase, 0 + BorderSpace);
            var p3 = new Cv.Point(p, img.Height - 1 - BorderSpace);

            var points = new List<Cv.Point>() { p1, p2, p3 };

            Cv2.FillConvexPoly(img, InputArray.Create(points), Scalar.Red, LineTypes.AntiAlias);
        }

        public static void DrawBarredRectangle(Mat img, int start, int stop, Scalar internalColor, Scalar linesColor, int thickness = 2, bool useDeltaAntiOverlap = true)
        {
            try
            {
                if (useDeltaAntiOverlap)
                {
                    //Per evitare overlap
                    start = start + DeltaAntiOverlap;
                    stop = stop - DeltaAntiOverlap;
                }

                //AntiClockWise points
                var p1 = new Cv.Point(start, 0);
                var p2 = new Cv.Point(start, img.Height - 1);
                var p3 = new Cv.Point(stop, img.Height - 1);
                var p4 = new Cv.Point(stop, 0);

                Cv2.Rectangle(img, p1, p3, internalColor, -1, LineTypes.AntiAlias);
                Cv2.Rectangle(img, p1, p3, linesColor, thickness, LineTypes.AntiAlias);

                Cv2.Line(img, p1, p3, linesColor, thickness, LineTypes.AntiAlias);
                Cv2.Line(img, p2, p4, linesColor, thickness, LineTypes.AntiAlias);
            }
            catch
            {
                //--
            }
        }

        public static void DrawBarredRectangle(Mat img, in int start, in int stop, int length, Scalar internalColor, Scalar linesColor, int thickness = 2, int delta = 0, bool useDeltaAntiOverlap = true)
        {
            try
            {
                int pStart = (int)((float)start / (float)length * (float)(img.Width - 1));
                int pStop = (int)((float)stop / (float)length * (float)(img.Width - 1));

                if (pStart > pStop)
                {
                    int tmp = pStart;
                    pStart = pStop;
                    pStop = tmp;
                }

                if (useDeltaAntiOverlap)
                {
                    //Per evitare overlap
                    pStart += DeltaAntiOverlap;
                    pStop -= DeltaAntiOverlap;
                }

                //AntiClockWise points
                var p1 = new Cv.Point(pStart, delta);
                var p2 = new Cv.Point(pStart, img.Height - 1 - delta);
                var p3 = new Cv.Point(pStop, img.Height - 1 - delta);
                var p4 = new Cv.Point(pStop, delta);

                Cv2.Rectangle(img, p1, p3, internalColor, -1, LineTypes.AntiAlias);
                Cv2.Rectangle(img, p1, p3, linesColor, thickness, LineTypes.AntiAlias);

                Cv2.Line(img, p1, p3, linesColor, thickness, LineTypes.AntiAlias);
                Cv2.Line(img, p2, p4, linesColor, thickness, LineTypes.AntiAlias);
            }
            catch
            {
                //--
            }
        }

        public static void DrawCross(Mat image, int x)
        {
            const int Delta = 50;
            var p1 = new Cv.Point(x - Delta, image.Height - 1);
            var p2 = new Cv.Point(x + Delta, image.Height - 1);
            var p0 = new Cv.Point(x, image.Height - 1);
            var p3 = new Cv.Point(x, image.Height - 1 - Delta);

            Cv2.Line(image, p1, p2, Scalar.Yellow, 4, LineTypes.AntiAlias);
            Cv2.Line(image, p0, p3, Scalar.Yellow, 4, LineTypes.AntiAlias);
        }

        public static void DrawVerticalLine(Mat image, int x, Scalar color)
        {
            var p1 = new Cv.Point(x, 0);
            var p2 = new Cv.Point(x, image.Height);

            Cv2.Line(image, p1, p2, color, 2, LineTypes.AntiAlias);
        }

        public static void DrawButterfly(Mat image, int start, int stop, int height, Scalar color, int thickness)
        {
            DrawVerticalLine(image, start, Scalar.Green);
            DrawVerticalLine(image, stop, Scalar.Red);

            var p1 = new Cv.Point(start, image.Height);
            var p2 = new Cv.Point(stop, image.Height);
            var p3 = p2 + new Cv.Point(0, -height);
            var p4 = p1 + new Cv.Point(0, -height);

            Cv2.Line(image, p1, p4, color, thickness, LineTypes.AntiAlias);
            Cv2.Line(image, p2, p3, color, thickness, LineTypes.AntiAlias);
            Cv2.Line(image, p1, p3, color, thickness, LineTypes.AntiAlias);
            Cv2.Line(image, p2, p4, color, thickness, LineTypes.AntiAlias);
        }
    }
}
