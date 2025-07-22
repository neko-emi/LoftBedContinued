using RimWorld;
using Verse;
using HarmonyLib;

namespace Nekoemi.LoftBed
{
    [StaticConstructorOnStartup]
    public class Init
    {
        static Init()
        {
            Harmony harmony = new Harmony("Nekoemi.LoftBed");
            harmony.PatchAll();
        }
    }
}
