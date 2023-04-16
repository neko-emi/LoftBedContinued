using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace zed_0xff.LoftBed
{
    class BedCache
    {
        private static HashSet<ThingWithComps> loftBeds = new HashSet<ThingWithComps>();
        private static Dictionary<int, Dictionary<IntVec3, ThingWithComps>> mapPosLoftBeds = new Dictionary<int, Dictionary<IntVec3, ThingWithComps>>();

        // null-safe
        public static bool isLoftBed(Thing t){
            return t is ThingWithComps twc && loftBeds.Contains(twc);
        }

        // null-safe
        public static ThingWithComps currentBedFor(Pawn p){
            if( p == null )
                return null;

            if( !mapPosLoftBeds.ContainsKey(p.Map.uniqueID) )
                return null;

            ThingWithComps t = null;
            if (mapPosLoftBeds[p.Map.uniqueID].TryGetValue(p.Position, out t)){
                if( t is Building_Bed bed && bed.OwnersForReading.Contains(p) ){
                    return bed;
                }
            }

            return null;
        }

        public static void Add(ThingWithComps t){
            loftBeds.Add(t);
            if( !mapPosLoftBeds.ContainsKey(t.Map.uniqueID) ){
                mapPosLoftBeds.Add(t.Map.uniqueID, new Dictionary<IntVec3, ThingWithComps>());
            }
            mapPosLoftBeds[t.Map.uniqueID].Add(t.Position, t);
        }

        public static void Remove(ThingWithComps t, Map map){
            loftBeds.Remove(t);
            if( mapPosLoftBeds.ContainsKey(t.Map.uniqueID) ){
                mapPosLoftBeds[map.uniqueID].Remove(t.Position);
            }
        }
    }
}
