using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Nekoemi.LoftBed
{
	[HarmonyPatch(typeof(PawnUtility), "GainComfortFromCellIfPossible")]
	static class Patch_GainComfortFromCellIfPossible
	{
		// vanilla GainComfortFromCellIfPossible() might skip LoftBed if there's another building under it
		static bool Prefix(ref Pawn p, int delta, bool chairsOnly)
		{
			if (!p.Spawned || !p.IsHashIntervalTick(15, delta) || chairsOnly)
				return true;

			Building_Bed currentBed = p.CurrentBed();
			if (LoftBedUtility.isLoftBed(currentBed))
			{
				PawnUtility.GainComfortFromThingIfPossible(p, currentBed, delta);
				return false;
			}

			return true;

			// returns only Loft beds!
			//var loft_bed = BedCache.currentBedFor(p);
			//if (loft_bed == null)
			//	return true;

			//PawnUtility.GainComfortFromThingIfPossible(p, loft_bed, delta);
			//return false;
		}
	}
}
