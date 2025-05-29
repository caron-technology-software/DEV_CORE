#undef CREATE_SINGLE_GERBER_FILE_IMAGE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using OpenCvSharp;

using ProRob;

using Caron.FileFormats.Denninson;
using Caron.FileFormats.Gerber;
using Caron.FileFormats.CutTicket;
using System.Diagnostics;
using System.Drawing.Printing;
//GPIx231 3)
using Caron.FileFormats.CutTicketX;
using Caron.FileFormats.Gerber.OverlapFinder;
//GPFx231 3)

namespace Caron
{
    public static partial class FabricPlacement
    {
        public const int ButterflyHeight = 100; //[px]
        //MMIx50 possibili parametri configurabili
        public const int OverlappingPercentageForAreaIntersection = 70;
        public const int MaximumOverlapsWidth = 1000;
        public const bool IsFullGeometriesInsideOverlapsAllowed = false;
        //MMFx50
        public static bool CreateEmptyFabricImage(int height, int width, string pathFinalImage)
        {
            try
            {
                var image = new Mat(height, width, MatType.CV_8UC3);

                image.SetTo(Scalar.Gainsboro);

                int delta = 10;

                var p1 = new Point(0 + delta, 0 + delta);
                var p2 = new Point(width - delta, height - delta);
                var p3 = new Point(0 + delta, height - delta);
                var p4 = new Point(width - delta, 0 + delta);

                Cv2.Line(image, p1, p2, Scalar.WhiteSmoke, thickness: 1, lineType: LineTypes.AntiAlias);
                Cv2.Line(image, p3, p4, Scalar.WhiteSmoke, thickness: 1, lineType: LineTypes.AntiAlias);

                Cv2.Line(image, p1, p4, Scalar.WhiteSmoke, thickness: 1, lineType: LineTypes.AntiAlias);
                Cv2.Line(image, p1, p3, Scalar.WhiteSmoke, thickness: 1, lineType: LineTypes.AntiAlias);
                Cv2.Line(image, p2, p4, Scalar.WhiteSmoke, thickness: 1, lineType: LineTypes.AntiAlias);
                Cv2.Line(image, p2, p3, Scalar.WhiteSmoke, thickness: 1, lineType: LineTypes.AntiAlias);

                image.SaveImage(pathFinalImage);
            }
            catch
            {
                Console.WriteLine("[CreateEmptyFabricImage] FATAL ERROR");

                return false;
            }

            return true;
        }

        //LION CUT TICKET
        public static bool TryCreateFabricPlacementFromGerberFile(
                        string gerberPath,
                        string pathFinalImage,
                        IEnumerable<Splice> splices = null,
                        int maxHeightToScale = 3600,
                        bool addPiecesToImageWithoutFilledPoly = true,
                        bool useImageFillTecnique = true)
        {
            try
            {
                ProConsole.WriteLine($"Parsing Gerber file: '{gerberPath}'", ConsoleColor.Yellow);

                var gerber = new GerberFileFormat();

                (List<Piece> knifePieces, List<Piece> penPieces, GerberMeasurementSystem internationalSystemOfUnits) = gerber.ParseFile(gerberPath);//MMIx18

                (int w, int h) = GerberHelper.GetMaxDimensions(knifePieces, penPieces);

                Console.WriteLine($"Dimensions(Width:{w}, Height:{h})");

                bool scaleBigImage = h > maxHeightToScale;

                if (scaleBigImage)
                {
                    ProConsole.WriteTitle("SCALE FACTOR 1/10", ConsoleColor.Yellow);

                    h = (int)Math.Ceiling((float)h / 10.0f);
                    w = (int)Math.Ceiling((float)w / 10.0f);

                    GerberHelper.ApplyScale(knifePieces, 0.1f);
                    GerberHelper.ApplyScale(penPieces, 0.1f);
                }

                //GerberHelper.ApplyOffset(knifePieces, new Point(1, 0));
                //GerberHelper.ApplyOffset(penPieces, new Point(1, 0));

                using Mat gerberImage = new Mat(h, w, MatType.CV_8UC3);

                if (useImageFillTecnique)
                {
                    for (int i = 0; i < knifePieces.Count; i++)
                    {
                        //Console.WriteLine($"{i}/{knifePieces.Count}");

                        using var pieceImage = new Mat(h + 1, w + 1, MatType.CV_8UC3);
                        using var grayImage = new Mat();

                        AddPiecesToImage(pieceImage, new List<Piece> { knifePieces[i] }, penPieces, Scalar.LightBlue);

                        Cv2.CvtColor(pieceImage, grayImage, ColorConversionCodes.BGRA2GRAY);
                        Cv2.Threshold(grayImage, grayImage, 1, 255, ThresholdTypes.Binary);

                        Cv2.FindContours(grayImage, out Point[][] contours, out HierarchyIndex[] hierarchyIndexes, RetrievalModes.External, ContourApproximationModes.ApproxNone);

                        Cv2.FillPoly(gerberImage, contours, Scalar.LightBlue, LineTypes.AntiAlias);
                    }
                }
                else
                {
                    AddPiecesToImage(gerberImage, knifePieces, penPieces, Scalar.LightBlue);
                }

                if (addPiecesToImageWithoutFilledPoly)
                {
                    AddPiecesToImageWithoutFilledPoly(gerberImage, knifePieces, penPieces, Scalar.DarkGray, thickness: 2);
                }

                //----------------------------------
                // Add splices
                //----------------------------------
                if (splices != null && splices.Count() > 0)
                {
                    foreach (var s in splices)
                    {
                        AddVerticalLine(gerberImage, s.Start, Scalar.Green);
                        AddVerticalLine(gerberImage, s.End, Scalar.Red);
                    }
                }

                //----------------------------------
                // Labels
                //----------------------------------
                var labelPoints = GerberFileFormat.GetLabelPointsFromFile(gerberPath);
                GerberHelper.ApplyScale(labelPoints, 0.1f);

                //if (internationalSystemOfUnits == false)
                if (internationalSystemOfUnits == GerberMeasurementSystem.Imperial_1_100_Inch)//MMIx18
                {
                    GerberHelper.ApplyScale(labelPoints, 2.54f);
                }

                GerberHelper.ApplyScale(labelPoints, 10.0f);

                if (scaleBigImage)
                {
                    GerberHelper.ApplyScale(labelPoints, 0.1f);
                }

                Console.WriteLine($"Number of labels: {labelPoints.Count}");

                foreach (var labelPoint in labelPoints)
                {
                    double fontScale = 4;
                    int thickness = 2;

                    AddCross(gerberImage, labelPoint.Point, Scalar.Yellow, lineLength: 15, shiftX: -5, shiftY: 5);

                    var textSize = Cv2.GetTextSize(labelPoint.Label, HersheyFonts.HersheyPlain, fontScale, thickness, out int baseline);

                    int deltaX = 0;
                    int deltaY = 0;

                    // X
                    if ((labelPoint.Point.X + textSize.Width) > gerberImage.Width)
                    {
                        deltaX = -(labelPoint.Point.X + textSize.Width - gerberImage.Width) - (int)fontScale * 3;
                    }

                    // Y
                    if ((labelPoint.Point.Y - textSize.Height) < 0)
                    {
                        deltaY = textSize.Height - labelPoint.Point.Y + (int)fontScale * 3;
                    }

                    var p = labelPoint.Point;
                    p.X += deltaX;
                    p.Y += deltaY;

                    labelPoint.Point = p;

                    Cv2.PutText(gerberImage, labelPoint.Label, labelPoint.Point, HersheyFonts.HersheyPlain, fontScale, Scalar.Red, thickness, LineTypes.AntiAlias);
                }

                //----------------------------------
                // Raw points
                //----------------------------------
                //var rawPoints = GerberFileFormat.GetRawPointsFromFile(gerberPath);

                //foreach (var point in rawPoints)
                //{
                //    Cv2.Circle(gerberImage, point, radius: 1, Scalar.Red, lineType: LineTypes.AntiAlias);
                //}

                gerberImage.SaveImage(pathFinalImage);

                return true;
            }
            catch
            {
                Console.WriteLine("[TryCreateFabricPlacementFromGerberFile] FATAL ERROR");

                return false;
            }
        }

        private static void PlotPiecesPointWithInfos(Mat image, List<Piece> pieces, Scalar color)
        {
            var points = pieces.SelectMany(x => x.SubPieces.SelectMany(x => x.Points)).ToList();

            var x = points.Select(x => x.X).Sum() / points.Count;
            var y = points.Select(x => x.X).Sum() / points.Count;

            Cv2.PutText(image, $"{x},{y}", new Point(x, y), HersheyFonts.HersheyPlain, 10.0, color);

            for (int i = 0; i < points.Count(); i++)
            {
                Cv2.Circle(image, points[i].X, points[i].Y, 4, color, 2);
                Cv2.PutText(image, $"{points[i].X},{points[i].Y}", points[i], HersheyFonts.HersheyPlain, 1.0, color);
            }
        }

        //GPIx67
        //MASK LION DENNINSON E JSON
        public static bool TryCreateFabricPlacementImage(
                            bool setDenninsonGerberStartFormulaX,
                            string pathDenninson,
                            string additionalFilesGerberSearchFolder,
                            IEnumerable<string> gerbersPath,
                            string pathFinalImage,
                            float pxToMmm,
                            bool rotate180,
                            bool invertButterflies,
                            bool flipImage,
                            bool isRightMachine,
                            out List<string> gerberFilesUsed,
                            uint flipOverlap = 0,//MMIx28
                            bool addPiecesToImageWithoutFilledPoly = true,
                            bool addGerberLabels = true,
                            bool useImageFillTechnique = true,
                            bool plotPiecesPoints = false)
        //GPFx67
        {

            int fabricHeight = int.MinValue;

            var knifePieces = new List<List<Piece>>();
            var penPieces = new List<List<Piece>>();
            var labelPoints = new List<List<LabelPoint>>();

            int fabricWidthFromGerber = 0;

            gerberFilesUsed = new List<string>();

            //Console.WriteLine("---------------------------------");
            //foreach (var item in gerbersPath)
            //{
            //    Console.WriteLine($"\t{item}");
            //}
            //Console.WriteLine("---------------------------------");

            try
            {
                var path = Path.GetDirectoryName(pathDenninson);

                // Parsing file denninson e verifica errori
                Console.WriteLine($"Parsing Denninson file: '{Path.GetFileName(pathDenninson)}'");

                var denninson = new Denninson();
                denninson.ParseFile(pathDenninson, flipOverlap);//MMIx28
                denninson.CheckMarkersContinuity = false;

                if (!denninson.CheckFile())
                {
                    Console.WriteLine($"File denninson {pathDenninson} not correct");

                    return false;
                }

                string[] gerberFiles = denninson.GetGerberFiles();
                List<int> gerbersLenght = new List<int>();//MMIx27 fix

                #region  Parsing Gerbers
                foreach (var gbr in gerberFiles)
                {
                    Console.WriteLine($"Parsing Gerber file: '{gbr}'");

                    //var geometryImporter = new UnsecuredGeometryImporterFacade();//MMIx50

                    //var gerber = new GerberFileFormat(geometryImporter);//MMIx50

                    var gerber = new GerberFileFormat();

                    var query = gerbersPath.Where(x => x.Contains(gbr));

                    //Console.WriteLine("--QUERY--");
                    //foreach (var item in query)
                    //{
                    //    Console.WriteLine($"\n{item}");
                    //}
                    //Console.WriteLine("--QUERY--");

                    string fullpathGerber = string.Empty;

                    if (query is null || query.Count() == 0)
                    {
                        var pathGerberToCheck = Path.Combine(additionalFilesGerberSearchFolder, gbr);

                        if (File.Exists(pathGerberToCheck))
                        {
                            fullpathGerber = Path.Combine(additionalFilesGerberSearchFolder, gbr);
                        }
                        else
                        {
                            Console.WriteLine($"File {pathGerberToCheck} doesn't exist");

                            return false;
                        }
                    }
                    else
                    {
                        fullpathGerber = query.First();
                    }

                    gerberFilesUsed.Add(fullpathGerber);

                     (List<Piece> currentKnifePieces, List<Piece> currentPenPieces, GerberMeasurementSystem internationalSystemOfUnit) = gerber.ParseFile(fullpathGerber);//MMIx18 bool internationalSystemOfUnit
                    var currentLabelPoints = GerberFileFormat.GetLabelPointsFromFile(fullpathGerber);

                    //MMIx18
                    if (internationalSystemOfUnit == GerberMeasurementSystem.Imperial_1_100_Inch)//standard
                    {
                        GerberHelper.ApplyScale(currentLabelPoints, 2.54f);
                    }
                    else if (internationalSystemOfUnit == GerberMeasurementSystem.Imperial_1_1000_Inch)//G70
                    {
                        GerberHelper.ApplyScale(currentLabelPoints, 0.254f);//00.254
                    }
                    else if (internationalSystemOfUnit == GerberMeasurementSystem.Metric_1_10_Millimeters)//G71
                    {
                        GerberHelper.ApplyScale(currentLabelPoints, 1f);//0.1 metri
                    }
                    //MMFx18

                    if (rotate180)
                    {
                        Console.WriteLine("Rotating pieces..");
                        GerberHelper.Rotate180(currentKnifePieces, currentPenPieces, currentLabelPoints);
                    }

                    #region MARKES_IMAGES
                    {
                        (int w, int h) = GerberHelper.GetMaxDimensions(currentKnifePieces, currentKnifePieces);

                        Mat gerberImage = new Mat(h, w, MatType.CV_8UC3);
                        AddPiecesToImage(gerberImage, currentKnifePieces, currentPenPieces, Scalar.LightBlue);
                        //gerberImage.SaveImage(Path.Combine(Path.GetDirectoryName(pathFinalImage),$"{gbr}.png"));
                    }
                    #endregion

                    #region DEBUG
                    ////foreach (var piece in currentKnifePieces)
                    ////{
                    ////    var maxX = piece.subPieces.SelectMany(x => x.points.Select(y => y.X)).Max();
                    ////    var minX = piece.subPieces.SelectMany(x => x.points.Select(y => y.X)).Min();
                    ////    var maxY = piece.subPieces.SelectMany(x => x.points.Select(y => y.Y)).Max();
                    ////    var minY = piece.subPieces.SelectMany(x => x.points.Select(y => y.Y)).Min();

                    ////    Console.WriteLine($"PIECE: maxX:{maxX} minX:{minX} maxY:{maxY} minY:{minY}");
                    ////}
                    #endregion

                    knifePieces.Add(currentKnifePieces);
                    penPieces.Add(currentPenPieces);
                    labelPoints.Add(currentLabelPoints);

                    (int currentPiecesMaxX, int currentPiecesMaxY) = GerberHelper.GetMaxDimensions(currentKnifePieces, currentKnifePieces);
                    (int currentPiecesMinX, int currentPiecesMinY) = GerberHelper.GetMinDimensions(currentKnifePieces, currentKnifePieces);

                    int deltaX = (currentPiecesMaxX - currentPiecesMinX);
                    int deltaY = (currentPiecesMaxY - currentPiecesMinY);

                    fabricWidthFromGerber += deltaX;
                    fabricHeight = Math.Max(fabricHeight, currentPiecesMaxY);

                    //MMIx27 fix lista contenente le lunghezze dei gerber
                    gerbersLenght.Add(currentPiecesMaxX);

                    Console.WriteLine($"[X] max:{currentPiecesMaxX} min:{currentPiecesMinX} delta:{deltaX}");
                    Console.WriteLine($"[Y] max:{currentPiecesMaxY} min:{currentPiecesMinY} delta:{deltaY}");
                }
                #endregion

                //Aggiunta offset disegno farfalla
                fabricHeight += 0;

                int fabricWidth = denninson.SpreadSections.Last().SectionEnd + 1;// -denninson.SpreadGeneralAllowance.EndOfSpreader;// + denninson.SpreadGeneralAllowance.BeginOfSpreader;// + denninson.SpreadGeneralAllowance.EndOfSpreader;
                float scale = (float)(fabricWidth) / (float)(fabricWidthFromGerber);

                Console.WriteLine($"Fabric width: {fabricWidth}");
                Console.WriteLine($"Fabric width (gerber) {fabricWidthFromGerber}");
                Console.WriteLine($"Fabric height: {fabricHeight}");
                Console.WriteLine($"Scale: {scale}");

                Mat fabricPlacementImage = new Mat(fabricHeight, fabricWidth, MatType.CV_8UC3);
                var listCross = new List<int>();

                int counterX = 0;
                int counterY = 0;

                var labels = new List<LabelPoint>();

                var allGeometryRange = new List<(double, double, double, double)>();//MMIx50

                var geometryRangeLater = new List<(double, double, double, double)>();//MMIx50

                for (int idxGbr = 0; idxGbr < gerberFiles.Count(); idxGbr++)
                {
                    ProConsole.WriteLine($"\nDrawing Gerber file: '{gerberFiles[idxGbr]}'", ConsoleColor.Green);

                    var sections = denninson.GetSectionsFromMarkerFileName(gerberFiles[idxGbr]);
                    var nSections = sections.Count();

                    Console.WriteLine($"nSections: {nSections}");

                    int gerberStart = sections.First().SectionBegin;
                    listCross.Add(gerberStart);

                    for (int idxSct = 0; idxSct < nSections; idxSct++)
                    {
                        bool noPieces = false;

                        //MMIx27 fix controlla se un gerber ha più sezioni
                        bool isGerberComposed = (sections[idxSct].SectionEnd - sections[idxSct].SectionBegin) < gerbersLenght[idxGbr] ? true : false;

                        // Devo determinare se devo considerare una sezione del marker o meno. In presenza di offset
                        // si determina l'utilizzo di un nuovo marker, in assenza => incrocio  
                        if (idxSct > 0 && sections[idxSct].SectionBegin > sections[idxSct - 1].SectionEnd)
                        {
                            gerberStart = sections[idxSct].SectionBegin;
                        }

                        //GPIx67
                        // Linea di codice aggiunta per poter gestire correttamente le sezioni (IF precedente non utile, al momento)
                        //////if (setDenninsonGerberStartFormulaX)
                        if (false)
                        {
                            //////introdotta il 21 aprile 2021 per cliente rose sormonti:
                            gerberStart = sections[idxSct].SectionBegin;
                        }
                        else
                        {
                            gerberStart = 0;
                        }
                        //GPFx67

                        var sectionStart = sections[idxSct].SectionBegin - gerberStart;
                        var sectionEnd = sections[idxSct].SectionEnd - gerberStart;
                        int sectionLength = sectionEnd - sectionStart;

                        ProConsole.WriteLine($"#{idxSct + 1:00} (CUT) Start: {sectionStart} End: {sectionEnd} (diff: {sectionLength})", ConsoleColor.Yellow);
                        ProConsole.WriteLine($"gerberStart: {gerberStart}");

                        //MMIx23
                        var currentKnifePieces = new List<Piece>();
                        var currentPenPieces = new List<Piece>();
                        var currentLabelPoints = new List<LabelPoint>();

                        //if (nSections > 1)
                        //MMIx27 fix true solo se ci sono più sezioni in un gerber unico e non prendo gli elementi senza coordinate
                        if (isGerberComposed)
                        {
                            int delta = 0;
                            
                            currentKnifePieces = (GerberHelper.GetPiecesFromSectionLimits(knifePieces[idxGbr],
                                     sectionStart - denninson.SpreadSpliceAllowance.BeginOfSpreader - delta,
                                     sectionEnd + denninson.SpreadSpliceAllowance.EndOfSpreader + delta))?
                                     .Where(x => x.SubPieces.Count > 0).ToList() ?? new List<Piece>(); 

                            currentPenPieces = GerberHelper.GetPiecesFromSectionLimits(penPieces[idxGbr],
                                    sectionStart - denninson.SpreadSpliceAllowance.BeginOfSpreader - delta,
                                    sectionEnd + denninson.SpreadSpliceAllowance.EndOfSpreader + delta)?
                                     .Where(x => x.SubPieces.Count > 0).ToList() ?? new List<Piece>();

                            currentLabelPoints = GerberHelper.GetLabelsFromSectionLimits(labelPoints[idxGbr],
                                       sectionStart - denninson.SpreadSpliceAllowance.BeginOfSpreader - delta,
                                       sectionEnd + denninson.SpreadSpliceAllowance.EndOfSpreader + delta);
                            
                        }
                        //MMFx23

                        if ((currentKnifePieces.Count == 0) && (currentPenPieces.Count == 0))
                        {
                            ProConsole.WriteLine("[WARNING] KnifePieces and PenPieces are empty", ConsoleColor.Red);

                            //MMIx27 fix non copio gli elementi che non contengono coordinate
                            currentKnifePieces = knifePieces[idxGbr]?
                                .Where(x => x.SubPieces.Count > 0).ToList() ?? new List<Piece>();
                            currentPenPieces = penPieces[idxGbr]?
                                .Where(x => x.SubPieces.Count > 0).ToList() ?? new List<Piece>();

                            //currentKnifePieces = knifePieces[idxGbr];
                            //currentPenPieces = penPieces[idxGbr];
                            //MMFx27 fix

                            noPieces = true;
                            //continue;
                        }

                        ProConsole.WriteLine($"Points - knife: {currentKnifePieces.Count} pen: {currentPenPieces.Count}", ConsoleColor.Yellow);

                        var xCoords = currentKnifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Select(x => x.X);
                        var yCoords = currentKnifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Select(x => x.Y);

                        int xMax = xCoords.Max();
                        int xMin = xCoords.Min();

                        int yMax = yCoords.Max();
                        int yMin = yCoords.Min();

                        int gerberLength = xMax - xMin;

                        Console.WriteLine($"\tXmax:{xMax} Xmin:{xMin} (d:{xMax - xMin})");
                        Console.WriteLine($"\tYmax:{yMax} Ymin:{yMin} (d:{yMax - yMin})");

                        counterX += currentKnifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Select(x => x.X).Max();
                        counterY += currentKnifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Select(x => x.Y).Max();

                        //Non considero Allowance (viene considerata nel PLC)
                        int piecesOffset = 0;

                        if (noPieces)
                        {                           
                            piecesOffset = sectionStart;

                            //MMIx27
                            //se i punti attuali sono maggiori di gerber lenght                         
                            if (xMax > gerberLength) 
                            {
                                piecesOffset = sectionStart - xMin;//MMIx27 fix
                            }
                            //MMFx27
                        }
                        else
                        {
                            piecesOffset = gerberStart + 0; //denninson.SpreadGeneralAllowance.BeginOfSpreader;
                        }

                        Console.WriteLine($"PIECES OFFSET: {piecesOffset}");

                        GerberHelper.ApplyOffset(currentKnifePieces, new Point(piecesOffset, 0));
                        GerberHelper.ApplyOffset(currentPenPieces, new Point(piecesOffset, 0));
                        GerberHelper.ApplyOffset(currentLabelPoints, new Point(piecesOffset, 0));

                        labels.AddRange(currentLabelPoints);

#if PLOT_DEBUG
                        PlotPiecesPointWithInfos(fabricPlacementImage, currentKnifePieces, Scalar.Red);
#endif
                        if (useImageFillTechnique)
                        {
                            var sw = new Stopwatch();
                            sw.Start();

                            for (int i = 0; i < currentKnifePieces.Count; i++)
                            {
                                //Console.WriteLine($"{i}/{knifePieces.Count}");

                                using var pieceImage = new Mat(fabricPlacementImage.Height + 1, fabricPlacementImage.Width + 1, MatType.CV_8UC3);
                                using var grayImage = new Mat();

                                AddPiecesToImage(pieceImage, new List<Piece> { currentKnifePieces[i] }, currentPenPieces, Scalar.LightBlue);

                                Cv2.CvtColor(pieceImage, grayImage, ColorConversionCodes.BGRA2GRAY);
                                Cv2.Threshold(grayImage, grayImage, 1, 255, ThresholdTypes.Binary);

                                Cv2.FindContours(grayImage, out Point[][] contours, out HierarchyIndex[] hierarchyIndexes, RetrievalModes.External, ContourApproximationModes.ApproxNone);

                                Cv2.FillPoly(fabricPlacementImage, contours, Scalar.LightBlue, LineTypes.AntiAlias);
                            }

                            sw.Stop();
                            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds:0}ms");
                        }
                        else
                        {
                            AddPiecesToImage(fabricPlacementImage, currentKnifePieces, currentPenPieces, Color.GetColor(idxSct));                       
                        }

                        if (addPiecesToImageWithoutFilledPoly)
                        {
                            AddPiecesToImageWithoutFilledPoly(fabricPlacementImage, currentKnifePieces, currentPenPieces, Scalar.DarkGray);
                        }

                        //MMIx50
                        //calcolo dei valori massimi e minimi per ogni geometria
                        var geometryRange = GetGeometriesBounds(currentKnifePieces);
                        allGeometryRange.AddRange(geometryRange);                        
                        //MMFx50
                    }
                }

                //MMIx50
                #region CALCOLO DEI SORMONTI DAL GERBER
                //ordinati i punti per xminima e poi per grandezza decrescente
                allGeometryRange = allGeometryRange
                    .OrderBy(i => i.Item1)
                        .ThenByDescending(i => i.Item2 - i.Item1)
                            .ToList();

                geometryRangeLater = allGeometryRange;//copiata perchè poi viene svuotata ma la devo utilizzare anche dopo

                // Second step. Find all geometries having a partial overlapping geometry within
                var areas = new List<SpreadArea>();

                while (allGeometryRange.Count > 0)
                {
                    // Crea una nuova area
                    var area = new SpreadArea()
                    {
                        Geometries = new List<(double MinX, double MaxX, double MinY, double MaxY)>(),
                        Left = 0,
                        Right = 0
                    };

                    // Seleziona tutte le geometrie che possono appartenere a questa area
                    for (int index = 0; index < allGeometryRange.Count; index++)
                    {
                        var gp = allGeometryRange[index];
                        var minX = (decimal)gp.Item1;
                        var maxX = (decimal)gp.Item2;

                        // Aggiungi la geometria più larga all'area
                        if (area.Geometries.Count == 0)
                        {
                            area.Geometries.Add(gp);
                        }
                        else
                        {
                            // Controlla se la geometria è totalmente contenuta nell'area
                            if (minX >= area.Left && maxX <= area.Right)
                            {
                                area.Geometries.Add(gp);
                            }
                            // Controlla se la geometria è in gran parte contenuta nell'area
                            else if (minX >= area.Left && maxX > area.Right)
                            {
                                var width = maxX - minX;
                                var overlappingPercentage = width == 0 ? 100 :
                                    ((area.Right - minX) / width) * 100;

                                // Se la sovrapposizione è superiore al 70%, aggiungi la geometria all'area
                                if (overlappingPercentage >= OverlappingPercentageForAreaIntersection)
                                {
                                    area.Geometries.Add(gp);
                                }
                            }
                        }

                        // Aggiorna i limiti dell'area
                        var mostRightGeometry = area.Geometries
                            .OrderByDescending(g => g.MaxX)
                            .FirstOrDefault();

                        var mostLeftGeometry = area.Geometries
                            .OrderBy(g => g.MinX)
                            .FirstOrDefault();

                        if (mostRightGeometry != default)
                            area.Right = (decimal)mostRightGeometry.MaxX;

                        if (mostLeftGeometry != default)
                            area.Left = (decimal)mostLeftGeometry.MinX;
                    }

                    //Le geometrie aggiunte all'area vengono rimosse dalla lista in modo che non vengano riconsiderate in iterazioni successive.
                    allGeometryRange = allGeometryRange
                        .Where(g => !area.Geometries.Contains(g))
                        .ToList();

                    areas.Add(area);
                } 

                var retModel = new GeometryOverlapsModel
                {
                    Overlaps = new List<GeometryOverlap>()
                };

                //individuati punti di taglio e stesura
                for (var index = 0; index < areas.Count - 1; index++) 
                {
                    var overlap = new GeometryOverlap()
                    {
                        CutPoint = areas[index].Right,
                        SpreadPoint = areas[index + 1].Left
                    };

                    if (overlap.CutPoint < overlap.SpreadPoint)
                    {
                        overlap = new GeometryOverlap()
                        {
                            CutPoint = overlap.SpreadPoint,
                            SpreadPoint = overlap.CutPoint
                        };
                    }

                    retModel.Overlaps.Add(overlap);
                }

                //se gli overlap si sovrappongono rimuovo il più grande
                var overlapsToRemove = new List<GeometryOverlap>();

                for (var index = 0; index < retModel.Overlaps.Count - 1; index++)
                {
                    var overlap = retModel.Overlaps.ElementAt(index);
                    var nextOverlap = retModel.Overlaps.ElementAt(index + 1);

                    if (nextOverlap.SpreadPoint <= overlap.CutPoint)
                    {
                        var overlapWidth = overlap.CutPoint - overlap.SpreadPoint;
                        var nextOverlapWidth = nextOverlap.CutPoint - nextOverlap.SpreadPoint;

                        if (overlapWidth > nextOverlapWidth)
                            overlapsToRemove.Add(overlap);
                        else
                            overlapsToRemove.Add(nextOverlap);
                    }
                }
                //rimuovo gli overlap che si sovrappongono
                overlapsToRemove.ForEach(o =>
                {
                    retModel.Overlaps.Remove(o);
                });

                // Assign random ids to computed overlaps
                for (var index = 0; index < retModel.Overlaps.Count; index++)
                {
                    retModel.Overlaps.ElementAt(index).Id = Guid.NewGuid().ToString();
                }

                // FIlter out overlaps exceeding maximum allowed width 
                if (MaximumOverlapsWidth != null && MaximumOverlapsWidth > 0)
                {
                    retModel.Overlaps = retModel.Overlaps.Where(o =>
                    {
                        return o.CutPoint - o.SpreadPoint <= MaximumOverlapsWidth;
                    }).ToList();
                }

                //se una geometria è completamente all'interno di un sormonto questo viene rimosso
                if (!IsFullGeometriesInsideOverlapsAllowed)
                { 
                    var geometries = geometryRangeLater;
                    //mantengo solo quelle che rispettano la condizione
                    retModel.Overlaps = retModel.Overlaps.Where(o =>
                    {
                        // If at least one geometry is fully included in overlap points, filter out the overlap
                        return !geometries.Any(g =>
                        {
                            var minX = (decimal)g.Item1;
                            var maxX = (decimal)g.Item2;
                            //se vero la geometria (g) è completamente inclusa nel sormonto
                            return minX >= o.SpreadPoint && maxX <= o.CutPoint;
                        });

                    }).ToList();
                }

                Console.WriteLine("Overlap from gerber file:");
                foreach(var o in retModel.Overlaps)
                {
                    Console.WriteLine($"cut point: {o.CutPoint}, spread point: {o.SpreadPoint}");
                }
                #endregion
                //MMFx50

                //Specchiatura per sormonti
                if (invertButterflies)
                {
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.Y);
                }

                Console.WriteLine("\nDrawing Overlaps..");
                foreach (var overlap in denninson.SpreadOverlapZones)
                {
                    AddButterfly(fabricPlacementImage, overlap.CutPoint, overlap.SpreadPoint, ButterflyHeight, invertButterflies);

                    Console.WriteLine($"Overlap: {overlap.CutPoint} {overlap.SpreadPoint}");
                }

                if (invertButterflies)
                {
                    //Rispecchiatura dopo sormonti
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.Y);
                }

                AdjustImage(ref fabricPlacementImage, pxToMmm);
                Console.WriteLine(fabricPlacementImage);

                Console.WriteLine($"Saving image to {pathFinalImage}..");

                //Specchiatura per disegno X
                fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.X);

                //Disegno X per centratura tessuto
                listCross.ForEach(x =>
                {
                    AddCross(fabricPlacementImage, x);
                    AddVerticalLine(fabricPlacementImage, x, Scalar.Yellow);
                });

                //Rispecchiatura per disegno X
                fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.X);

                if (flipImage)
                {
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.X);
                }

                if (isRightMachine)
                {
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.Y);
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.X);
                }

                if (addGerberLabels)
                {
                    if (flipImage)
                    {
                        GerberHelper.RotateLabelsAxisY(labels, fabricPlacementImage.Height);
                    }

                    if (isRightMachine)
                    {
                        GerberHelper.RotateLabelsAxisY(labels, fabricPlacementImage.Height);

                        GerberHelper.RotateLabelsAxisX(labels, fabricPlacementImage.Width);
                    }

                    AddLabelsToImage(fabricPlacementImage, labels);
                }

                var ret = fabricPlacementImage.SaveImage(pathFinalImage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Source}-{ex.Message}");

                return false;
            }

            return true;
        }

        //GPIx223
        //GPIx67
        //MASK CUT TICKET
        public static bool TryCreateFabricPlacementImageCutFile(
                            bool setDenninsonGerberStartFormulaX,
                            string pathDenninson,
                            string additionalFilesGerberSearchFolder,
                            IEnumerable<string> gerbersPath,
                            string pathFinalImage,
                            float pxToMmm,
                            bool rotate180,
                            bool invertButterflies,
                            bool flipImage,
                            bool isRightMachine,
                            out List<string> gerberFilesUsed,
                            bool addPiecesToImageWithoutFilledPoly = true,
                            bool addGerberLabels = true,
                            bool useImageFillTechnique = true,
                            bool plotPiecesPoints = false,
                            string CutTicketLetterCutFile = ""
                            )
        //GPFx67
        {

            int fabricHeight = int.MinValue;

            var knifePieces = new List<List<Piece>>();
            var penPieces = new List<List<Piece>>();
            var labelPoints = new List<List<LabelPoint>>();

            int fabricWidthFromGerber = 0;

            gerberFilesUsed = new List<string>();

            //Console.WriteLine("---------------------------------");
            //foreach (var item in gerbersPath)
            //{
            //    Console.WriteLine($"\t{item}");
            //}
            //Console.WriteLine("---------------------------------");

            try
            {
                var path = Path.GetDirectoryName(pathDenninson);

                // Parsing file denninson e verifica errori
                Console.WriteLine($"Parsing Denninson file: '{Path.GetFileName(pathDenninson)}'");

                var denninson = new Denninson();
                denninson.ParseFileCutFile(pathDenninson);
                denninson.CheckMarkersContinuity = false;

                if (!denninson.CheckFile())
                {
                    Console.WriteLine($"File denninson {pathDenninson} not correct");

                    return false;
                }

                string[] gerberFiles = denninson.GetGerberFiles();

                #region  Parsing Gerbers
                foreach (var gbr in gerberFiles)
                {
                    Console.WriteLine($"Parsing Gerber file: '{gbr}'");

                    var gerber = new GerberFileFormat();

                    var query = gerbersPath.Where(x => x.Contains(gbr));

                    //Console.WriteLine("--QUERY--");
                    //foreach (var item in query)
                    //{
                    //    Console.WriteLine($"\n{item}");
                    //}
                    //Console.WriteLine("--QUERY--");

                    string fullpathGerber = string.Empty;

                    if (query is null || query.Count() == 0)
                    {
                        var pathGerberToCheck = Path.Combine(additionalFilesGerberSearchFolder, gbr);

                        if (File.Exists(pathGerberToCheck))
                        {
                            fullpathGerber = Path.Combine(additionalFilesGerberSearchFolder, gbr);
                        }
                        else
                        {
                            
                            //GPIx235
                            var denninsonFileName = pathDenninson;
                            var denninsonFileExtension = Path.GetExtension(pathDenninson).ToLower(); //  ".CTX"   ->   ".ctx"
                            if ((denninsonFileExtension == ".ctx") && (File.Exists(denninsonFileName)))
                            {

                            }
                            else
                            {
                                Console.WriteLine($"File {pathGerberToCheck} doesn't exist");
                                return false;
                            }
                            //GPFx235
                        }
                    }
                    else
                    {
                        fullpathGerber = query.First();
                    }

                    //GPIx231 3)
                    //fullpathGerber
                    //"C:\\WORKINGS\\TUBE.CUT"
                    #region Aggiornamento fullpathGerber se CUT TICKET in elaborazione ".ctx" con PathCutTicket
                    {
                        var denninsonFileName = pathDenninson;
                        var denninsonFileExtension = Path.GetExtension(pathDenninson).ToLower(); //  ".CTX"   ->   ".ctx"		
                        if ((denninsonFileExtension == ".ctx") && (File.Exists(denninsonFileName)))
                        {
                            CutTicketContext.CutFilePath = denninsonFileName;   //path;
                            CutTicketContext.CutTicket = CutTicketHelper.DeserializeCutTicket(CutTicketContext.CutFilePath);
                            string fullpathGerberX = fullpathGerber;
                            fullpathGerber = CutTicketContext.CutTicket.JobFile;  //contiene il disegno della stesura e nel cut_ticket ce n'è uno solo!!!

                            //GPIx235
                            //bisogna cambiargli il public string CutTicketLetterCutFile { get; set; }  //"CutTicketLetterCutFile": "h:\\",
                            //------>CutTicketLetterCutFile
                            string str01 = CutTicketLetterCutFile.Substring(0, 1) + fullpathGerber.Substring(1);
                            //////////string str01 = pathDenninson.Substring(0, 1) + fullpathGerber.Substring(1); //path denninson mi dice se devo andare in H: o C:
                            fullpathGerber = str01;
                            if (File.Exists(fullpathGerber))
                            //if (fullpathGerber.IndexOf(":\\") > -1)
                            {
                                
                            }
                            else
                            {
                                fullpathGerber = fullpathGerberX;
                            }
                            
                            if (!(File.Exists(fullpathGerber)))
                            {
                                Console.WriteLine($"File Cutticket gerber {fullpathGerber} doesn't exist");
                                return false;
                            }
                            //GPFx235
                        }
                    }
                    #endregion
                    //GPFx231 3)

                    gerberFilesUsed.Add(fullpathGerber);

                    (List<Piece> currentKnifePieces, List<Piece> currentPenPieces, GerberMeasurementSystem internationalSystemOfUnit) = gerber.ParseFile(fullpathGerber);//MMIx18
                    var currentLabelPoints = GerberFileFormat.GetLabelPointsFromFile(fullpathGerber);

                    //MMIx18
                    if (internationalSystemOfUnit == GerberMeasurementSystem.Imperial_1_100_Inch)//standard
                    {
                        GerberHelper.ApplyScale(currentLabelPoints, 2.54f);
                    }
                    else if (internationalSystemOfUnit == GerberMeasurementSystem.Imperial_1_1000_Inch)//G70
                    {
                        GerberHelper.ApplyScale(currentLabelPoints, 0.254f);//00.254
                    }
                    else if (internationalSystemOfUnit == GerberMeasurementSystem.Metric_1_10_Millimeters)//G71
                    {
                        GerberHelper.ApplyScale(currentLabelPoints, 1f);//0.1 metri
                    }
                    //MMFx18

                    if (rotate180)
                    {
                        Console.WriteLine("Rotating pieces..");
                        GerberHelper.Rotate180(currentKnifePieces, currentPenPieces, currentLabelPoints);
                    }

                    #region MARKES_IMAGES
                    {
                        (int w, int h) = GerberHelper.GetMaxDimensions(currentKnifePieces, currentKnifePieces);

                        Mat gerberImage = new Mat(h, w, MatType.CV_8UC3);
                        AddPiecesToImage(gerberImage, currentKnifePieces, currentPenPieces, Scalar.LightBlue);
                        //gerberImage.SaveImage(Path.Combine(Path.GetDirectoryName(pathFinalImage),$"{gbr}.png"));
                    }
                    #endregion

                    #region DEBUG
                    ////foreach (var piece in currentKnifePieces)
                    ////{
                    ////    var maxX = piece.subPieces.SelectMany(x => x.points.Select(y => y.X)).Max();
                    ////    var minX = piece.subPieces.SelectMany(x => x.points.Select(y => y.X)).Min();
                    ////    var maxY = piece.subPieces.SelectMany(x => x.points.Select(y => y.Y)).Max();
                    ////    var minY = piece.subPieces.SelectMany(x => x.points.Select(y => y.Y)).Min();

                    ////    Console.WriteLine($"PIECE: maxX:{maxX} minX:{minX} maxY:{maxY} minY:{minY}");
                    ////}
                    #endregion

                    knifePieces.Add(currentKnifePieces);
                    penPieces.Add(currentPenPieces);
                    labelPoints.Add(currentLabelPoints);

                    (int currentPiecesMaxX, int currentPiecesMaxY) = GerberHelper.GetMaxDimensions(currentKnifePieces, currentKnifePieces);
                    (int currentPiecesMinX, int currentPiecesMinY) = GerberHelper.GetMinDimensions(currentKnifePieces, currentKnifePieces);

                    int deltaX = (currentPiecesMaxX - currentPiecesMinX);
                    int deltaY = (currentPiecesMaxY - currentPiecesMinY);

                    fabricWidthFromGerber += deltaX;
                    fabricHeight = Math.Max(fabricHeight, currentPiecesMaxY);

                    Console.WriteLine($"[X] max:{currentPiecesMaxX} min:{currentPiecesMinX} delta:{deltaX}");
                    Console.WriteLine($"[Y] max:{currentPiecesMaxY} min:{currentPiecesMinY} delta:{deltaY}");
                }
                #endregion

                //Aggiunta offset disegno farfalla
                fabricHeight += 0;

                int fabricWidth = denninson.SpreadSections.Last().SectionEnd + 1;// -denninson.SpreadGeneralAllowance.EndOfSpreader;// + denninson.SpreadGeneralAllowance.BeginOfSpreader;// + denninson.SpreadGeneralAllowance.EndOfSpreader;
                float scale = (float)(fabricWidth) / (float)(fabricWidthFromGerber);

                Console.WriteLine($"Fabric width: {fabricWidth}");
                Console.WriteLine($"Fabric width (gerber) {fabricWidthFromGerber}");
                Console.WriteLine($"Fabric height: {fabricHeight}");
                Console.WriteLine($"Scale: {scale}");

                Mat fabricPlacementImage = new Mat(fabricHeight, fabricWidth, MatType.CV_8UC3);
                var listCross = new List<int>();

                int counterX = 0;
                int counterY = 0;

                var labels = new List<LabelPoint>();

                for (int idxGbr = 0; idxGbr < gerberFiles.Count(); idxGbr++)
                {
                    ProConsole.WriteLine($"\nDrawing Gerber file: '{gerberFiles[idxGbr]}'", ConsoleColor.Green);

                    var sections = denninson.GetSectionsFromMarkerFileName(gerberFiles[idxGbr]);
                    var nSections = sections.Count();

                    Console.WriteLine($"nSections: {nSections}");

                    int gerberStart = sections.First().SectionBegin;
                    listCross.Add(gerberStart);

                    for (int idxSct = 0; idxSct < nSections; idxSct++)
                    {
                        bool noPieces = false;

                        // Devo determinare se devo considerare una sezione del marker o meno. In presenza di offset
                        // si determina l'utilizzo di un nuovo marker, in assenza => incrocio  
                        if (idxSct > 0 && sections[idxSct].SectionBegin > sections[idxSct - 1].SectionEnd)
                        {
                            gerberStart = sections[idxSct].SectionBegin;
                        }

                        //GPIx67
                        // Linea di codice aggiunta per poter gestire correttamente le sezioni (IF precedente non utile, al momento)
                        //////if (setDenninsonGerberStartFormulaX)
                        if (false)
                        {
                            //////introdotta il 21 aprile 2021 per cliente rose sormonti:
                            gerberStart = sections[idxSct].SectionBegin;
                        }
                        else
                        {
                            gerberStart = 0;
                        }
                        //GPFx67

                        var sectionStart = sections[idxSct].SectionBegin - gerberStart;
                        var sectionEnd = sections[idxSct].SectionEnd - gerberStart;
                        int sectionLength = sectionEnd - sectionStart;

                        ProConsole.WriteLine($"#{idxSct + 1:00} (CUT) Start: {sectionStart} End: {sectionEnd} (diff: {sectionLength})", ConsoleColor.Yellow);
                        ProConsole.WriteLine($"gerberStart: {gerberStart}");

                        //MMIx23
                        var currentKnifePieces = new List<Piece>();
                        var currentPenPieces = new List<Piece>();
                        var currentLabelPoints = new List<LabelPoint>();

                        if (nSections > 1)
                        {
                            int delta = 0;

                            currentKnifePieces = GerberHelper.GetPiecesFromSectionLimits(knifePieces[idxGbr],
                                     sectionStart - denninson.SpreadSpliceAllowance.BeginOfSpreader - delta,
                                     sectionEnd + denninson.SpreadSpliceAllowance.EndOfSpreader + delta);

                            currentPenPieces = GerberHelper.GetPiecesFromSectionLimits(penPieces[idxGbr],
                                    sectionStart - denninson.SpreadSpliceAllowance.BeginOfSpreader - delta,
                                    sectionEnd + denninson.SpreadSpliceAllowance.EndOfSpreader + delta);

                            currentLabelPoints = GerberHelper.GetLabelsFromSectionLimits(labelPoints[idxGbr],
                                       sectionStart - denninson.SpreadSpliceAllowance.BeginOfSpreader - delta,
                                       sectionEnd + denninson.SpreadSpliceAllowance.EndOfSpreader + delta);
                        }       
                        //MMFx23

                        if ((currentKnifePieces.Count == 0) && (currentPenPieces.Count == 0))
                        {
                            ProConsole.WriteLine("[WARNING] KnifePieces and PenPieces are empty", ConsoleColor.Red);

                            currentKnifePieces = knifePieces[idxGbr];
                            currentPenPieces = penPieces[idxGbr];

                            noPieces = true;
                            //continue;
                        }

                        ProConsole.WriteLine($"Points - knife: {currentKnifePieces.Count} pen: {currentPenPieces.Count}", ConsoleColor.Yellow);

                        var xCoords = currentKnifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Select(x => x.X);
                        var yCoords = currentKnifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Select(x => x.Y);

                        int xMax = xCoords.Max();
                        int xMin = xCoords.Min();

                        int yMax = yCoords.Max();
                        int yMin = yCoords.Min();

                        int gerberLength = xMax - xMin;

                        Console.WriteLine($"\tXmax:{xMax} Xmin:{xMin} (d:{xMax - xMin})");
                        Console.WriteLine($"\tYmax:{yMax} Ymin:{yMin} (d:{yMax - yMin})");

                        counterX += currentKnifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Select(x => x.X).Max();
                        counterY += currentKnifePieces.SelectMany(x => x.SubPieces).SelectMany(x => x.Points).Select(x => x.Y).Max();

                        //Non considero Allowance (viene considerata nel PLC)
                        int piecesOffset = 0;

                        if (noPieces)
                        {
                            piecesOffset = sectionStart;

                            //MMIx27
                            float res = 0;
                            //se i punti attuali sono maggiori di gerber lenght                         
                            if (xMax > gerberLength)
                            {
                                //di quanti marker in avanti sono spostati i punti
                                res = (xMax - gerberLength) / gerberLength;
                                //sottrazione per applicare un ofset corretto -10 di ofset - 1 di delta che sono aggiunti sopra
                                var generalOffset = denninson.GetWorkingTableData().Offsets[idxGbr];
                                piecesOffset = (int)(piecesOffset - (gerberLength * res) - (generalOffset * res));
                            }
                            //MMFx27
                        }
                        else
                        {
                            piecesOffset = gerberStart + 0; //denninson.SpreadGeneralAllowance.BeginOfSpreader;
                        }

                        Console.WriteLine($"PIECES OFFSET: {piecesOffset}");

                        GerberHelper.ApplyOffset(currentKnifePieces, new Point(piecesOffset, 0));
                        GerberHelper.ApplyOffset(currentPenPieces, new Point(piecesOffset, 0));
                        GerberHelper.ApplyOffset(currentLabelPoints, new Point(piecesOffset, 0));

                        labels.AddRange(currentLabelPoints);

#if PLOT_DEBUG
                        PlotPiecesPointWithInfos(fabricPlacementImage, currentKnifePieces, Scalar.Red);
#endif
                        if (useImageFillTechnique)
                        {
                            var sw = new Stopwatch();
                            sw.Start();

                            for (int i = 0; i < currentKnifePieces.Count; i++)
                            {
                                //Console.WriteLine($"{i}/{knifePieces.Count}");

                                using var pieceImage = new Mat(fabricPlacementImage.Height + 1, fabricPlacementImage.Width + 1, MatType.CV_8UC3);
                                using var grayImage = new Mat();

                                AddPiecesToImage(pieceImage, new List<Piece> { currentKnifePieces[i] }, currentPenPieces, Scalar.LightBlue);

                                Cv2.CvtColor(pieceImage, grayImage, ColorConversionCodes.BGRA2GRAY);
                                Cv2.Threshold(grayImage, grayImage, 1, 255, ThresholdTypes.Binary);

                                Cv2.FindContours(grayImage, out Point[][] contours, out HierarchyIndex[] hierarchyIndexes, RetrievalModes.External, ContourApproximationModes.ApproxNone);

                                Cv2.FillPoly(fabricPlacementImage, contours, Scalar.LightBlue, LineTypes.AntiAlias);
                            }

                            sw.Stop();
                            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds:0}ms");
                        }
                        else
                        {
                            AddPiecesToImage(fabricPlacementImage, currentKnifePieces, currentPenPieces, Color.GetColor(idxSct));
                        }

                        if (addPiecesToImageWithoutFilledPoly)
                        {
                            AddPiecesToImageWithoutFilledPoly(fabricPlacementImage, currentKnifePieces, currentPenPieces, Scalar.DarkGray);
                        }
                    }
                }

                //Specchiatura per sormonti
                if (invertButterflies)
                {
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.Y);
                }

                Console.WriteLine("\nDrawing Overlaps..");
                foreach (var overlap in denninson.SpreadOverlapZones)
                {
                    //MMIx58 sul grafico del piazzato mancano gli allowance dei sormonti ** non ancora messo **
                    AddButterfly(fabricPlacementImage, overlap.CutPoint, overlap.SpreadPoint, ButterflyHeight, invertButterflies);

                    Console.WriteLine($"Overlap: {overlap.CutPoint} {overlap.SpreadPoint}");
                }

                if (invertButterflies)
                {
                    //Rispecchiatura dopo sormonti
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.Y);
                }

                AdjustImage(ref fabricPlacementImage, pxToMmm);
                Console.WriteLine(fabricPlacementImage);

                Console.WriteLine($"Saving image to {pathFinalImage}..");

                //Specchiatura per disegno X
                fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.X);

                //Disegno X per centratura tessuto
                listCross.ForEach(x =>
                {
                    AddCross(fabricPlacementImage, x);
                    AddVerticalLine(fabricPlacementImage, x, Scalar.Yellow);
                });

                //Rispecchiatura per disegno X
                fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.X);

                if (flipImage)
                {
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.X);
                }

                if (isRightMachine)
                {
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.Y);
                    fabricPlacementImage = fabricPlacementImage.Flip(FlipMode.X);
                }

                if (addGerberLabels)
                {
                    if (flipImage)
                    {
                        GerberHelper.RotateLabelsAxisY(labels, fabricPlacementImage.Height);
                    }

                    if (isRightMachine)
                    {
                        GerberHelper.RotateLabelsAxisY(labels, fabricPlacementImage.Height);

                        GerberHelper.RotateLabelsAxisX(labels, fabricPlacementImage.Width);
                    }

                    AddLabelsToImage(fabricPlacementImage, labels);
                }

                var ret = fabricPlacementImage.SaveImage(pathFinalImage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Source}-{ex.Message}");

                return false;
            }

            return true;
        }
        //GPFx223
    }
}