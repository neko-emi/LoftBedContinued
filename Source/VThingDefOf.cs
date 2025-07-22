using RimWorld;
using Verse;

namespace Nekoemi.LoftBed
{
    [DefOf]
    public static class VThingDefOf
    {
        public static ThingDef LoftBed;

        static VThingDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(VThingDefOf));
    }
}
