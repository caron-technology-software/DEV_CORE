using System;
using System.Collections.Generic;
using System.Linq;

namespace Caron.FileFormats.Denninson
{
	public class DenninsonWorkingTable
	{
		public List<string> MarkersName { get; set; }
		public List<int> MarkersStart { get; set; }
		public List<int> MarkersStop { get; set; }
		public List<int> MarkersLength { get; set; }
		public List<int> Offsets { get; set; }
		public List<string> Colors { get; set; }
		public List<List<int>> Plyes { get; set; }
		//GPIx223
		public List<List<int>> PlyesDone { get; set; }
		//GPFx223
		public int FrontAllowance { get; set; }
		public int RearAllowance { get; set; }
		public int FrontSplice { get; set; }
		public int RearSplice { get; set; }
		public List<int> OverlapZoneSpreadPoints { get; set; }
		public List<int> OverlapZoneCutPoints { get; set; }

		public int GetTotalLength()
		{
			int totalLength = -1;

			if (MarkersStart.Count > 0 && MarkersStop.Count > 0)
			{
				int minPos = MarkersStart.Min();
				int maxPos = MarkersStop.Max();

				totalLength = (maxPos - minPos) + FrontAllowance + RearAllowance;
			}

			return totalLength;
		}

		public DenninsonWorkingTable()
		{
			//--
		}
	}
}