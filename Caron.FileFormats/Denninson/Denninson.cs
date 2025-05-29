using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ProRob;
using ProRob.Extensions.String;
//GPIx223
using Caron.FileFormats.CutTicketX;
//GPFx223

namespace Caron.FileFormats.Denninson
{
    public partial class Denninson
    {
        private const int MaxNumberOfChars = 130;

        public bool FileParsed { get; private set; } = false;
        public bool CheckMarkersContinuity { get; set; } = false;

        private List<Section> sections;
        private List<Step> steps;
        private List<OverlapZone> overlapZones;
        private GeneralAllowance generalAllowance;
        private SpliceAllowance spliceAllowance;

        public List<Section> SpreadSections { get => sections; }
        public List<Step> SpreadSteps { get => steps; }
        public List<OverlapZone> SpreadOverlapZones { get => overlapZones; }
        public GeneralAllowance SpreadGeneralAllowance { get => generalAllowance; }
        public SpliceAllowance SpreadSpliceAllowance { get => spliceAllowance; }

        public bool CheckFile()
        {
            if (!FileParsed)
            {
                return false;
            }

            if (CheckMarkersContinuity)
            {
                var markerFilenames = sections.Select(x => x.MarkerFilename).ToList();
                var uniqueMarkerFilenames = markerFilenames.Distinct().ToList();

                // Verifica MarkerFilename unico (vi possono essere MarkerFile uguali,
                // ma solo rispetto alle sezioni differenti e comunque devono essere continui)
                foreach (var marker in uniqueMarkerFilenames)
                {
                    var idx = Enumerable.Range(0, markerFilenames.Count).Where(x => markerFilenames[x] == marker).ToList();
                    var diff = Enumerable.Range(1, idx.Count - 1).Select(x => idx[x] - idx[x - 1]).ToList();

                    var res = diff.Any(x => !x.Equals(1));

                    if (res)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public List<Section> GetSectionsFromMarkerFileName(string markerFilename)
        {
            if (!FileParsed)
            {
                return null;
            }

            return sections.Where(x => x.MarkerFilename == markerFilename).ToList();
        }

        public string[] GetGerberFiles()
        {
            if (!FileParsed)
            {
                return null;
            }

            var files = sections.Select(x => x.MarkerFilename).Distinct().ToArray();

            for (int i = 0; i < files.Count(); i++)
            {
                files[i] = files[i].Trim(' ');
            }

            return files;
        }

        //GPIx223
        public bool ParseFileCutFile(string path)
        {
            FileParsed = false;

            //////////////////////////////////GPIx223A//////////////////////////////////////////////////////////////////////////////////////////////
            //
            //const int StartIndexTotalGeneralAllowance = 13;
            const int StartIndexMarkerName = 24;
            const int StartIndexMarkerID = 24;
            const int StartIndexSectionStart = 40;
            const int StartIndexMarkerFilename = 40;
            const int StartIndexSectionEnd = 18;
            const int StartIndexOrderId = 4;
            const int StartIndexSpreadNr = 13;
            const int StartIndexColorId = 33;
            const int StartIndexRollId = 59;
            const int StartIndexNrOfPly = 29;
            //const int StartIndexOrderId = 4;
            //const int StartIndexSpreadNumber = 13;
            //const int OrderIdLength = 8;

            int allowance = 0;
            int offset = 0;

            //CutTicketContext.CutFilePath = "C:\\CUT\\TUBE-1.ctx";
            CutTicketContext.CutFilePath = path;
            CutTicketContext.CutTicket = CutTicketHelper.DeserializeCutTicket(CutTicketContext.CutFilePath);

            //////////controlli pre caricamento:
            if (CutTicketContext.CutTicket == null)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            if (CutTicketContext.CutTicket.MarkerReport == null)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            if (CutTicketContext.CutTicket.SpreadList == null)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            if (CutTicketContext.CutTicket.MarkerReport.Length == 0)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            if (CutTicketContext.CutTicket.SpreadList.Count() == 0)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            //////////controlli pre caricamento - fine.

            string createDenninsonFromCutfile = "";
            createDenninsonFromCutfile = "999 01 00 01.01.2024\r\n";
            createDenninsonFromCutfile = createDenninsonFromCutfile + DenninsonConverter.Constants.Caron.Headers.Section201;

            //------------------------------------------------
            // Section 201
            //------------------------------------------------
            const int MinimunLineLength = 50;
            const int MaximumLineLength = 90;
            //string line201 = "201 001 001 00000 01058 393C210AS-Sec01 393C210AS.gbr                                     ";
            string line201 = "201 001 001 00000                                                                         ";
            if (line201.Length < MinimunLineLength)
            {
                line201 = line201.PadRight(MinimunLineLength);
            }
            //string markerName = line201.Substring(StartIndexMarkerID, StartIndexMarkerFilename - StartIndexMarkerID).Trim();
            string markerID = CutTicketContext.CutTicket.MarkerReport.MarkerName;
            string markerFileName = "";
            if (CutTicketContext.CutTicket.JobFile != null)
            {
                markerFileName = Path.GetFileName(CutTicketContext.CutTicket.JobFile);
            }
            if (markerFileName.Length > (MaximumLineLength - StartIndexMarkerFilename - 1))
            {
                markerFileName = markerFileName.Substring(0, MaximumLineLength - StartIndexMarkerFilename - 1);
            }
            if (markerID.Length > (StartIndexMarkerFilename - StartIndexMarkerID - 1))
            {
                markerID = markerID.Substring(0, StartIndexMarkerFilename - StartIndexMarkerID - 1);
            }
            line201 = line201.ReplaceChars($"{markerFileName}", StartIndexMarkerFilename);
            line201 = line201.ReplaceChars($"{markerID}", StartIndexMarkerID);
            float sectionEndInch = (float)CutTicketContext.CutTicket.MarkerReport.Length;
            int sectionEndMm = (int)(sectionEndInch * 25.4f);
            string sectionEnd = sectionEndMm.ToString("D5");
            if (sectionEnd.Length > 5)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            line201 = line201.ReplaceChars($"{sectionEnd}", StartIndexSectionEnd);
            createDenninsonFromCutfile = createDenninsonFromCutfile +
                                    line201 + "\r\n";

            //------------------------------------------------
            // Header002
            //------------------------------------------------
            createDenninsonFromCutfile = createDenninsonFromCutfile + DenninsonConverter.Constants.Caron.Headers.Section002;

            //GPIx235
            //string line002 = "002 PIPPO 1  001 001 001 001 007 NERO                      R123                     1 0000 EW 120000 0000 0000";
            string line002 = "002          001 001 001 001                               R123                     1 0000 EW 000000 0000 0000";
            if (CutTicketContext.CutTicket.MarkerReport.SpreadMode != null)
            {
                if ((CutTicketContext.CutTicket.MarkerReport.SpreadMode.Trim() == "SinglePly")
                || (CutTicketContext.CutTicket.MarkerReport.SpreadMode.Trim() == "")
                )
                {
                    line002 = "002          001 001 001 001                               R123                     1 0000 EW 000000 0000 0000";
                }
                else
                {
                    if (CutTicketContext.CutTicket.MarkerReport.SpreadMode.Trim() == "FaceToFace")
                    {
                        line002 = "002          001 001 001 001                               R123                     1 0000 ZZ 000000 0000 0000";
                    }
                    else
                    {
                        if (CutTicketContext.CutTicket.MarkerReport.SpreadMode.Trim() == "Tubular")
                        {
                            line002 = "002          001 001 001 001                               R123                     1 0000 SL 000000 0000 0000";
                        }
                        else
                        {
                            line002 = "002          001 001 001 001                               R123                     1 0000 EW 000000 0000 0000";
                        }
                    }
                }
            }
            //GPFx235

            bool changeColorName = false;
            foreach (var item in CutTicketContext.CutTicket.SpreadList)
            {
                int i = 0;
                foreach (var item2 in CutTicketContext.CutTicket.SpreadList)
                {
                    if (item.Color==item2.Color)
                    {
                        if (i > 0)
                        {
                            item2.Color = item2.Color + i.ToString();
                            changeColorName = true;
                        }
                        i++;
                    }
                }
            }
            //{  se nomecolore nel vettore uguale aggiorna i successivi con nomecolore1, nomecolore2, nomecolore3,... }
            if (changeColorName)
            {
                String xml;
                xml = CutTicketHelper.SerializeCutTicket(CutTicketContext.CutTicket);
                File.WriteAllText(CutTicketContext.CutFilePath, xml);   //aggiorna l'xml se c'è un cambiamento colore se non no...
            }

            foreach (var item in CutTicketContext.CutTicket.SpreadList)
            {
                string orderId = Path.GetFileNameWithoutExtension(CutTicketContext.CutFilePath);   //StartIndexOrderId
                if (orderId.Length > (StartIndexSpreadNr - StartIndexOrderId - 1))
                {
                    orderId = orderId.Substring(0, StartIndexSpreadNr - StartIndexOrderId - 1);
                }
                line002 = line002.ReplaceChars($"{orderId}", StartIndexOrderId);
                string colorId = item.Color;
                if (colorId.Length > (StartIndexRollId - StartIndexColorId - 1))
                {
                    colorId = colorId.Substring(0, StartIndexRollId - StartIndexColorId - 1);
                }
                line002 = line002.ReplaceChars($"{colorId}", StartIndexColorId);
                string nrOfPly = item.Ply.ToString("D3");
                if (item.Ply == 0)
                {
                    //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                    //msgBox.ShowDialog();
                    return false;
                }
                if (nrOfPly.Length > 3)
                {
                    //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                    //msgBox.ShowDialog();
                    return false;
                }
                line002 = line002.ReplaceChars($"{nrOfPly}", StartIndexNrOfPly);

                //GPIx235
                if (CutTicketContext.CutTicket.SpreaderConfigurationId != null)
                {
                    string spreaderConfigurationId = CutTicketContext.CutTicket.SpreaderConfigurationId;
                    string spreaderConfigurationIdNumber = "1";
                    switch (spreaderConfigurationId.Trim())
                    {
                        case "jeans":
                            spreaderConfigurationIdNumber = "1";
                            break;
                        case "":
                            spreaderConfigurationIdNumber = "1";
                            break;
                        case "wool":
                            spreaderConfigurationIdNumber = "2";
                            break;
                        case "cotton":
                            spreaderConfigurationIdNumber = "3";
                            break;
                        case "C4":
                            spreaderConfigurationIdNumber = "4";
                            break;
                        case "C5":
                            spreaderConfigurationIdNumber = "5";
                            break;
                        case "C6":
                            spreaderConfigurationIdNumber = "6";
                            break;
                        case "C7":
                            spreaderConfigurationIdNumber = "7";
                            break;
                        case "C8":
                            spreaderConfigurationIdNumber = "8";
                            break;
                        case "C9-1":
                            spreaderConfigurationIdNumber = "9";
                            break;
                        case "C9-2":
                            spreaderConfigurationIdNumber = "9";
                            break;
                    }
                    line002 = line002.ReplaceChars($"{spreaderConfigurationIdNumber}", 84); //Parameter-Set inizio
                }
                //GPFx235

                line002 = line002 + "\r\n";
                createDenninsonFromCutfile = createDenninsonFromCutfile +
                                    line002; // + "\r\n";
            }

            //------------------------------------------------
            // Allowance 
            //------------------------------------------------
            createDenninsonFromCutfile = createDenninsonFromCutfile + DenninsonConverter.Constants.Caron.Headers.Section007;
            //string line007 = "007 PIPPO 1  0025 0025";
            string line007 = "007                   ";
            const int StartIndexGeneralAllowanceBeginOfSpread = 13;  //=StartIndexSpreadNr
            const int StartIndexGeneralAllowanceEndOfSpread = 18;
            string orderIdX = Path.GetFileNameWithoutExtension(CutTicketContext.CutFilePath);   //StartIndexOrderId
            if (orderIdX.Length > (StartIndexSpreadNr - StartIndexOrderId - 1))
            {
                orderIdX = orderIdX.Substring(0, StartIndexSpreadNr - StartIndexOrderId - 1);
            }
            line007 = line007.ReplaceChars($"{orderIdX}", StartIndexOrderId);
            string GeneralAllowanceBeginOfSpreadfPly = "0000";
            if (GeneralAllowanceBeginOfSpreadfPly.Length > 4)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            line007 = line007.ReplaceChars($"{GeneralAllowanceBeginOfSpreadfPly}", StartIndexGeneralAllowanceBeginOfSpread);
            string GeneralAllowanceEndOfSpreadfPly = "0000";
            if (GeneralAllowanceEndOfSpreadfPly.Length > 4)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            line007 = line007.ReplaceChars($"{GeneralAllowanceEndOfSpreadfPly}", StartIndexGeneralAllowanceEndOfSpread);
            createDenninsonFromCutfile = createDenninsonFromCutfile +
                                    line007 + "\r\n";
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            createDenninsonFromCutfile = createDenninsonFromCutfile + DenninsonConverter.Constants.Caron.Headers.Section014;
            //string line014 = "014 001 01990 01990";
            string line014 = "014                ";
            const int StartIndexNumberOfOverlapzone = 4;  //=StartIndexSpreadNr
            const int StartIndexCutPointMM = 8;
            const int StartIndexSpreadPointMM = 14;
            float CutPointMMInch;
            int CutPointMMMm;
            string CutPoint;
            float SpreadPointMMInch;
            int SpreadPointMMMm;
            string SpreadPoint;
            int ix = 1;
            if (CutTicketContext.CutTicket.Splices != null)
            {
                foreach (var item in CutTicketContext.CutTicket.Splices)
                {
                    line014 = "014                ";
                    CutPointMMInch = (float)item.Start;
                    CutPointMMMm = (int)(CutPointMMInch * 25.4f);
                    CutPoint = CutPointMMMm.ToString("D5");
                    SpreadPointMMInch = (float)item.End;
                    SpreadPointMMMm = (int)(SpreadPointMMInch * 25.4f);
                    SpreadPoint = SpreadPointMMMm.ToString("D5");
                    string numberOfOverlapzone = ix.ToString("D3");
                    if (numberOfOverlapzone.Length > (StartIndexCutPointMM - StartIndexNumberOfOverlapzone - 1))
                    {
                        numberOfOverlapzone = numberOfOverlapzone.Substring(0, StartIndexCutPointMM - StartIndexNumberOfOverlapzone - 1);
                    }
                    line014 = line014.ReplaceChars($"{numberOfOverlapzone}", StartIndexNumberOfOverlapzone);

                    //string CutPoint;
                    if (CutPoint.Length > (StartIndexSpreadPointMM - StartIndexCutPointMM - 1))
                    {
                        CutPoint = CutPoint.Substring(0, StartIndexSpreadPointMM - StartIndexCutPointMM - 1);
                    }
                    line014 = line014.ReplaceChars($"{CutPoint}", StartIndexCutPointMM);
                    //string SpreadPoint;
                    if (SpreadPoint.Length > 5)
                    {
                        SpreadPoint = SpreadPoint.Substring(0, 5);
                    }
                    line014 = line014.ReplaceChars($"{SpreadPoint}", StartIndexSpreadPointMM);

                    line014 = line014 + "\r\n";
                    createDenninsonFromCutfile = createDenninsonFromCutfile +
                                        line014; // + "\r\n";
                    ix++;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            createDenninsonFromCutfile = createDenninsonFromCutfile + DenninsonConverter.Constants.Caron.Headers.Section118;
            //string line007 = "118 PIPPO 1  0025 0025";
            string line118 = "118                   ";
            orderIdX = Path.GetFileNameWithoutExtension(CutTicketContext.CutFilePath);   //StartIndexOrderId
            if (orderIdX.Length > (StartIndexSpreadNr - StartIndexOrderId - 1))
            {
                orderIdX = orderIdX.Substring(0, StartIndexSpreadNr - StartIndexOrderId - 1);
            }
            line118 = line118.ReplaceChars($"{orderIdX}", StartIndexOrderId);
            GeneralAllowanceBeginOfSpreadfPly = "0000";
            //float GeneralAllowanceBeginOfSpreadfPlyInch = (float)CutTicketContext.CutTicket.Splices[0].Start;
            //int GeneralAllowanceBeginOfSpreadfPlyMm = (int)(GeneralAllowanceBeginOfSpreadfPlyInch * 25.4f);
            //GeneralAllowanceBeginOfSpreadfPly = GeneralAllowanceBeginOfSpreadfPlyMm.ToString("D4");
            if (GeneralAllowanceBeginOfSpreadfPly.Length > 4)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            line118 = line118.ReplaceChars($"{GeneralAllowanceBeginOfSpreadfPly}", StartIndexGeneralAllowanceBeginOfSpread);
            GeneralAllowanceEndOfSpreadfPly = "0000";
            //float GeneralAllowanceEndOfSpreadfPlyInch = (float)CutTicketContext.CutTicket.Splices[0].End;
            //int GeneralAllowanceEndOfSpreadfPlyMm = (int)(GeneralAllowanceEndOfSpreadfPlyInch * 25.4f);
            //GeneralAllowanceEndOfSpreadfPly = GeneralAllowanceEndOfSpreadfPlyMm.ToString("D4");
            if (GeneralAllowanceEndOfSpreadfPly.Length > 4)
            {
                //var msgBox = new MachineMessageBox(Localization.Warning, Localization.FileNotCorrect);
                //msgBox.ShowDialog();
                return false;
            }
            line118 = line118.ReplaceChars($"{GeneralAllowanceEndOfSpreadfPly}", StartIndexGeneralAllowanceEndOfSpread);
            createDenninsonFromCutfile = createDenninsonFromCutfile +
                                    line118 + "\r\n";

            //qui:
            File.WriteAllText("C:\\CUT\\TUBE-1_ctx.txt", createDenninsonFromCutfile);
            createDenninsonFromCutfile = createDenninsonFromCutfile;// +
                                                                    //line118+"\r\n";
            var srcDenninson = createDenninsonFromCutfile.SplitNewline();
            /////////////////////////GPIx223A///////////////////////////////////////////////////////////////////////////////////////////////////////

            //////////var srcDenninson = File.ReadAllLines(path);

            //////////srcDenninson = DenninsonConverter.Convert(srcDenninson).SplitNewline();

            ////////////file mat per ora se lungo 0 gli da file errato:
            //////////if (srcDenninson.Length == 0)
            //////////{
            //////////    return false;
            //////////}
            //////////////// file DENNISON convertito al tipo DENNISON CARON
            ////////////System.IO.File.WriteAllLines(@"C:\WORKINGS\DENISON_CARON_DA_448209A+_001.001", srcDenninson);

            for (int i = 0; i < srcDenninson.Length; i++)
            {
                srcDenninson[i] = srcDenninson[i].PadRight(MaxNumberOfChars);
            }

            sections = new List<Section>();
            steps = new List<Step>();
            overlapZones = new List<OverlapZone>();

            sections = Denninson.Parser.GetSections(srcDenninson);
            steps = Denninson.Parser.GetSteps(srcDenninson);
            //GPIx223 A
            int i01 = 0;
            foreach (var item in CutTicketContext.CutTicket.SpreadList)
            {
                steps[i01].NumberOfPlyesDone = item.PliesSpread;
                i01++;
            }
            //GPFx223 A
            overlapZones = Denninson.Parser.GetOverlapsZones(srcDenninson);

            var generalAllowances = Denninson.Parser.GetGeneralAllowance(srcDenninson);
            if (generalAllowances.Count() == 0)
            {
                generalAllowance = new GeneralAllowance();
                generalAllowance.BeginOfSpreader = Constants.DefaultAllowance;
                generalAllowance.EndOfSpreader = Constants.DefaultAllowance;
            }
            else
            {
                generalAllowance = generalAllowances.First();
            }

            var spliceAllowances = Denninson.Parser.GetSpliceAllowance(srcDenninson);
            if (spliceAllowances.Count() == 0)
            {
                spliceAllowance = new SpliceAllowance();
                spliceAllowance.BeginOfSpreader = Constants.DefaultAllowance;
                spliceAllowance.EndOfSpreader = Constants.DefaultAllowance;
            }
            else
            {
                spliceAllowance = spliceAllowances.First();
            }

            if (true)
            {
                sections.ForEach(x => { Console.WriteLine(x); });
                steps.ForEach(x => { Console.WriteLine(x); });
                overlapZones.ForEach(x => { Console.WriteLine(x); });
                Console.WriteLine(generalAllowance);
                Console.WriteLine(spliceAllowance);
            }

            FileParsed = true;

            return true;
        }
        //GPFx223

        public bool ParseFile(string path, uint? enableFlip = 0)//enableFlip opzionale e di default a 0
        {
            FileParsed = false;

            var srcDenninson = File.ReadAllLines(path);

            srcDenninson = DenninsonConverter.Convert(srcDenninson).SplitNewline();

            //file mat per ora se lungo 0 gli da file errato:
            if (srcDenninson.Length==0)
            {
                return false;
            }
            ////// file DENNISON convertito al tipo DENNISON CARON
            //System.IO.File.WriteAllLines(@"C:\WORKINGS\DENISON_CARON_DA_448209A+_001.001", srcDenninson);

            for (int i=0;i<srcDenninson.Length;i++)
            {
                srcDenninson[i] = srcDenninson[i].PadRight(MaxNumberOfChars);
            }

            sections = new List<Section>();
            steps = new List<Step>();
            overlapZones = new List<OverlapZone>();

            sections = Denninson.Parser.GetSections(srcDenninson);
            steps = Denninson.Parser.GetSteps(srcDenninson);
            overlapZones = Denninson.Parser.GetOverlapsZones(srcDenninson);

            //MMIx13 specchiate le overlap zone di 180°
            //if (FilesFormat.IsLectraVersion && enableFlip == 1)
            if (enableFlip == 1)
            {
                int precCutPoint = 0;
                foreach (var x in overlapZones)
                {
                    precCutPoint = x.CutPoint;

                    x.CutPoint = Denninson.GetSpreadLength(srcDenninson) - x.SpreadPoint;
                    x.SpreadPoint = Denninson.GetSpreadLength(srcDenninson) - precCutPoint;
                }
            }
            //MMFx13

            var generalAllowances = Denninson.Parser.GetGeneralAllowance(srcDenninson);
            if (generalAllowances.Count() == 0)
            {
                generalAllowance = new GeneralAllowance();
                generalAllowance.BeginOfSpreader = Constants.DefaultAllowance;
                generalAllowance.EndOfSpreader = Constants.DefaultAllowance;
            }
            else
            {
                generalAllowance = generalAllowances.First();
            }

            var spliceAllowances = Denninson.Parser.GetSpliceAllowance(srcDenninson);
            if (spliceAllowances.Count() == 0)
            {
                spliceAllowance = new SpliceAllowance();
                spliceAllowance.BeginOfSpreader = Constants.DefaultAllowance;
                spliceAllowance.EndOfSpreader = Constants.DefaultAllowance;
            }
            else
            {
                spliceAllowance = spliceAllowances.First();
            }

            if (true)
            {
                sections.ForEach(x => { Console.WriteLine(x); });
                steps.ForEach(x => { Console.WriteLine(x); });
                overlapZones.ForEach(x => { Console.WriteLine(x); });
                Console.WriteLine(generalAllowance);
                Console.WriteLine(spliceAllowance);
            }

            FileParsed = true;

            return true;
        }

        public DenninsonWorkingTable GetWorkingTableData()
        {
            if (!FileParsed)
            {
                return default;
            }

            //ProConsole.WriteLine($"Delta:", ConsoleColor.Red);
            var sections = SpreadSections.ToList();

            var delta = SpreadSections.Skip(1).Zip(SpreadSections, (curr, prev) => curr.SectionBegin - prev.SectionEnd).ToList();
            //delta.ForEach(x => Console.WriteLine($"{x}"));

            var gerberFileIndex = new List<int>();
            gerberFileIndex.Add(0);
            for (int i = 0; i < delta.Count - 1; i++)
            {
                //Verifico presenza OFFSET
                if (delta[i] > 0)
                {
                    gerberFileIndex.Add(i + 1);
                }
            }

            //Offset
            var offsets = new List<int>();
            for (int i = 1; i < gerberFileIndex.Count; i++)
            {
                int index = gerberFileIndex[i];

                var s = sections[index];
                var ps = sections[index - 1]; //precedent

                int start = s.SectionBegin;
                int stop = ps.SectionEnd;

                offsets.Add(start - stop); 
            }

            //GPIx67
            //caso 1 marker:
            if (gerberFileIndex.Count == 1)
            {
                offsets.Add(0);
            }
            else
            {
                //Caso: 2 marker
                if (delta.Count == 1 && offsets.Count == 0)
                {
                    offsets.Add(delta[0]);
                }
            }
            //GPFx67

            ProConsole.WriteLine($"Offsets:", ConsoleColor.Red);
            offsets.ForEach(x => Console.WriteLine($"{x}"));

            //ProConsole.WriteLine($"MarkersIndex:", ConsoleColor.Red);
            //gerberFileIndex.ForEach(x => Console.WriteLine($"{x}"));

            ProConsole.WriteLine($"Gerber Files:", ConsoleColor.Red);
            for (int i = 0; i < gerberFileIndex.Count; i++)
            {
                int idxStart = 0;
                int idxStop = 0;

                idxStart = gerberFileIndex[i];
                if (i != gerberFileIndex.Count - 1)
                {
                    idxStop = gerberFileIndex[i + 1] - 1;
                }
                else
                {
                    idxStop = sections.Count - 1;
                }

                int length = sections[idxStop].SectionEnd - sections[idxStart].SectionBegin;

                Console.WriteLine($"Gerber file {i} - Sections: {idxStart}-{idxStop} - Length: {length}");
            }

            ProConsole.WriteLine($"Markers:", ConsoleColor.Red);
            var markers = SpreadSections.ToList();

            var markersStart = new List<int>();
            var markersStop = new List<int>();
            var markersLength = new List<int>();

            for (int i = 0; i < markers.Count; i++)
            {
                int start = sections[i].SectionBegin;
                int stop = markers[i].SectionEnd;
                int length = stop - start;

                markersStart.Add(start);
                markersStop.Add(stop);
                markersLength.Add(length);

                Console.WriteLine($"Marker {i} - Start:{start} End:{stop} Length: {length}");
            }

            ProConsole.WriteLine($"Colors:", ConsoleColor.Red);
            var colors = SpreadSteps.Select(x => x.ColorId).Distinct().ToList();
            colors.ForEach(x => Console.WriteLine($"{x}"));

            var totalPlyes = new List<List<int>>();
            //GPIx223
            var totalPlyesDone = new List<List<int>>();
            //GPFx223
            foreach (var color in colors)
            {
                ProConsole.WriteLine($"[COLOR: {color}] Steps:", ConsoleColor.Red);

                var plyes = new List<int>(new int[sections.Count]);
                //GPIx223 A
                var plyesDone = new List<int>(new int[sections.Count]);
                //GPFx223 A
                var steps = SpreadSteps.Where(x => x.ColorId == color).ToList();

                foreach (var step in steps)
                {
                    var sectionFrom = step.SectionFrom;
                    var sectionTo = step.SectionTo;

                    //Console.WriteLine($"sectionFrom: {sectionFrom} sectionTo: {sectionTo}");

                    for (int idx = sectionFrom - 1; idx <= sectionTo - 1; idx++)
                    {
                        plyes[idx] += step.NumberOfPly;
                        //GPIx223 A
                        plyesDone[idx] += step.NumberOfPlyesDone;
                        //GPFx223 A
                    }
                }

                totalPlyes.Add(plyes);
                //GPIx223 A
                totalPlyesDone.Add(plyesDone);
                //GPFx223 A

                ProConsole.WriteLine($"Plyes:", ConsoleColor.Yellow);
                foreach (var ply in plyes)
                {
                    Console.Write($"{ply} ");
                }
                Console.WriteLine("");
            }

            //Overlaps
            var overlapZoneCutPoints = new List<int>();
            var overlapZoneSpreadPoints = new List<int>();

            foreach (var overlap in SpreadOverlapZones)
            {
                overlapZoneCutPoints.Add(overlap.CutPoint);
                overlapZoneSpreadPoints.Add(overlap.SpreadPoint);
            }

            var workingTable = new DenninsonWorkingTable()
            {
                MarkersName = sections.Select(x => x.MarkerId).ToList(),
                MarkersStart = markersStart,
                MarkersStop = markersStop,
                MarkersLength = markersLength,
                Offsets = offsets,
                Colors = colors,
                Plyes = totalPlyes,
                //GPIx223 A
                PlyesDone = totalPlyesDone,
                //GPFx223 A
                FrontAllowance = SpreadGeneralAllowance.BeginOfSpreader,
                RearAllowance = SpreadGeneralAllowance.EndOfSpreader,
                FrontSplice = SpreadSpliceAllowance.BeginOfSpreader,
                RearSplice = SpreadSpliceAllowance.EndOfSpreader,
                OverlapZoneCutPoints = overlapZoneCutPoints,
                OverlapZoneSpreadPoints = overlapZoneSpreadPoints
            };

            return workingTable;
        }
    }
}
