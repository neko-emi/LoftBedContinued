using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace zed_0xff.LoftBed
{
    public class CompLoftBed : ThingComp
    {
        public static HashSet<ThingWithComps> loftBeds = new HashSet<ThingWithComps>();

        public static bool isLoftBed(Thing t){
            return t is ThingWithComps twc && loftBeds.Contains(twc);
        }

        // only to handle hospitality GuestBed.Swap()
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            loftBeds.Add(parent);
            foreach(Thing t in parent.Position.GetThingList(parent.Map)){
                if( t == parent ) continue;
                //Log.Warning("[d] t: " + t + " def: " + t.def + " type: " + t.GetType());
                if( isLoftBed(t) ){
                    Log.Message("[d] CompLoftBed: destroying " + t + " at " + t.Position);
                    t.Destroy();
                    break; // otherwise we get "Collection was modified; enumeration operation may not execute."
                }
            }
        }

        public override void PostDeSpawn(Map map)
        {
            loftBeds.Remove(parent);
            base.PostDeSpawn(map);
        }
    }
}
