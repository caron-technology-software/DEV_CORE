using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caron.FileFormats.Denninson
{
    //000 Spread Nr.
    //000 ³   Section Nr.
    //000 ³   ³   Section begin 
    //000 ³   ³   ³     Section end 
    //000 ³   ³   ³     ³     Marker ID 
    //000 ³   ³   ³     ³     ³               Marker Filename 
    //000 V   V   V     V     V               V

    public class Section : IBuildFromParameters<Section>
    {
        public int SpreaderNumber { get; set; }
        public int SectionNumber { get; set; }
        public int SectionBegin { get; set; }
        public int SectionEnd { get; set; }
        public string MarkerId { get; set; }
        public string MarkerFilename { get; set; }

        public Section()
        {
            //--
        }

        public Section(int spreaderNumber, int sectionNumber, int sectionBegin, int sectionEnd, string markerId, string markerFilename)
        {
            SpreaderNumber = spreaderNumber;
            SectionNumber = sectionNumber;
            SectionBegin = sectionBegin;
            SectionEnd = sectionEnd;
            MarkerId = markerId;
            MarkerFilename = markerFilename;
        }

        public override string ToString()
        {
            return $"[SECTION]\nSpreaderNumber={SpreaderNumber} SectionNumber={SectionNumber} SectionBegin={SectionBegin} SectionEnd={SectionEnd} SectionLength={SectionEnd - SectionBegin} MarkerId=\"{MarkerId}\" MarkerFilename=\"{MarkerFilename}\"\n";
        }

        public Section BuildFromParameters(string[] parameters)
        {
            Section section = new Section();

            section.SpreaderNumber = int.Parse(parameters[0]);
            section.SectionNumber = int.Parse(parameters[1]);
            section.SectionBegin = int.Parse(parameters[2]);
            section.SectionEnd = int.Parse(parameters[3]);
            section.MarkerId = parameters[4];
            section.MarkerFilename = parameters[5];

            return section;
        }
    }
}

