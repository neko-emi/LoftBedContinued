using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace Nekoemi.LoftBed
{
    public class CompLoftBed : ThingComp
    {
        // called on game load, struture build, structure move
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            //BedCache.Add(parent);
            foreach(Thing t in parent.Position.GetThingList(parent.Map)){
                if( t == parent ) continue;
                if( LoftBedUtility.isLoftBed(t) ){
                    Log.Message("[d] CompLoftBed: destroying " + t + " at " + t.Position);
                    t.Destroy();
                    break; // otherwise we get "Collection was modified; enumeration operation may not execute."
                }
            }
        }

        /*public override void PostDeSpawn(Map map, DestroyMode mode = DestroyMode.Vanish)
        {
            //BedCache.Remove(parent, map);
            base.PostDeSpawn(map, mode);
        }*/
    }
}
