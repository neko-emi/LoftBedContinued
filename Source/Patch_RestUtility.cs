using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Nekoemi.LoftBed
{
	[HarmonyPatch(typeof(RestUtility), "CurrentBed")]
	[HarmonyPatch(new Type[] { typeof(Pawn), typeof(int?) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Out })]
	static class Patch_CurrentBed
	{
		static void Postfix(ref Building_Bed __result, Pawn p, ref int? sleepingSlot)
		{
			if (__result != null || p == null || p.Map == null)
				return;

			List<Thing> things = p.Position.GetThingList(p.Map);
			List<Building_Bed> beds = new List<Building_Bed>();
			for (int i = 0; i < things.Count; i++)
			{
				if (things[i] is Building_Bed bed)
				{
					beds.Add(bed);
				}
			}

			if (beds.Count < 2)
				return;

			foreach (Building_Bed bed in beds)
			{
				List<Pawn> owners = bed.OwnersForReading;
				for (int i = 0; i < owners.Count; i++)
				{
					if (owners[i] == p)
					{
						__result = bed;
						sleepingSlot = i;
						return;
					}
				}
			}

			// keep original result?
		}
	}
	/*
	[HarmonyPatch(typeof(RestUtility), "CanUseBedNow")]
	static class Patch_CanUseBedNow
	{
		static void Postfix(ref bool __result, Thing bedThing, Pawn sleeper, bool checkSocialProperness, bool allowMedBedEvenIfSetToNoCare = false, GuestStatus? guestStatusOverride = null)
		{
			if(bedThing == null)
			{
				return;
			}
			Building_Bed curBed = (Building_Bed)bedThing;

			// prevent loft bed from kicking a pawn out of a medical bed that it is currently in
			if (__result == false && curBed != null && curBed.Medical && curBed.CurOccupants != null && curBed.CurOccupants.Contains(sleeper))
			{
				__result = true;
				return;
			}
			
			if (__result == false && !curBed.Medical)
			{
				List<Thing> things = curBed.Position.GetThingList(curBed.Map);
				List<Building_Bed> beds = new List<Building_Bed>();
				for (int i = 0; i < things.Count; i++)
				{
					if (things[i] is Building_Bed bed)
					{
						if(bed.OwnersForReading.Contains(sleeper) && bed.AnyUnoccupiedSleepingSlot)
						{
							__result = true;
							return;
						}
					}
				}
			}

		}
	
	}*/

	/*[HarmonyPatch(typeof(RestUtility), "KickOutOfBed")]
	static class Patch_KickOutOfBed
	{
		static bool Prefix(Pawn pawn, Building_Bed bed)
		{
			if (pawn == null || !pawn.Spawned || (!pawn.Dead && !pawn.GetPosture().InBed()))
			{
				return true;
			}

			int? sleepingSlot;
			Building_Bed building_Bed = pawn.CurrentBed(out sleepingSlot);
			if (building_Bed != bed)
			{
				if (building_Bed == null)
				{
					bed = null;
				}
				else
				{
					Log.Error("Tried to kick pawn " + pawn.ToStringSafe() + " out of a bed they're not currently in.");
				}
			}

			pawn.jobs.posture &= ~PawnPosture.InBedMask;
			if (bed != null && (pawn.Downed || pawn.Deathresting))
			{
				pawn.Position = bed.GetFootSlotPos(sleepingSlot.Value);
			}
			return false;
		}
	}*/
}
