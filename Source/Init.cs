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
			//Harmony.DEBUG = true;
			Harmony harmony = new Harmony("Nekoemi.LoftBed");
			harmony.PatchAll();
		}
	}
}
