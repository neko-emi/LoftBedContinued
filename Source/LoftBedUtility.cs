using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Nekoemi.LoftBed
{
	public static class LoftBedUtility
	{
		public static bool isLoftBed(Thing t)
		{
			return t is ThingWithComps twc && twc.def != null && twc.def.building != null && twc.def.building.buildingTags != null & twc.def.building.buildingTags.Contains("LoftBed");
		}
	}
}
