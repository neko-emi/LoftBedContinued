using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace zed_0xff.LoftBed {
    [HarmonyPatch(typeof(PawnRenderer), "GetBodyPos")]
    static class Patch_GetBodyPos
    {
        private static FieldInfo fPawn = AccessTools.Field(typeof(PawnRenderer), "pawn");

        private static readonly AccessTools.FieldRef<PawnRenderer, Pawn> _pawn = AccessTools.FieldRefAccess<PawnRenderer, Pawn>("pawn");

        // shift pawn's head position according to LoftBed's shift
        static void Postfix(PawnRenderer __instance, Vector3 drawLoc, ref bool showBody, ref Vector3 __result)
        {
            if( showBody ) return;

            Pawn pawn = _pawn(__instance);
            if( pawn == null ) return;

            Building_Bed bed = pawn.CurrentBed();
            if ( bed == null ) return;

            if ( bed.def == VThingDefOf.LoftBed ) {
                __result.z += LoftBedMod.Settings.f2;
            } else {
                // fixed by <altitudeLayer>MoteOverhead</altitudeLayer>
                /*
                List<Thing> things = pawn.Position.GetThingList(pawn.Map);
                for (int i = 0; i < things.Count; i++){
                    if( things[i] is Building_LoftBed){
                        // semi-hide lower pawn head under loft bed
                        __result.y += LoftBedMod.Settings.f3;
                        return;
                    }
                }
                */
            }
        }
    }
}
