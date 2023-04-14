using System;
using System.Collections.Generic;
using Verse;
using UnityEngine;

namespace zed_0xff.LoftBed
{
    public class LoftBedSettings : ModSettings
    {
        public float f1 = -28f;
        public float f2 = 0.5f;
//        public float f3 = -1.3f;
        public float maxFillPercent = 0.5f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref f1, "f1", -28f);
            Scribe_Values.Look(ref f2, "f2", 0.5f);
//            Scribe_Values.Look(ref f2, "f3", -1.3f);
            Scribe_Values.Look(ref maxFillPercent, "maxFillPercent", 0.5f);
            base.ExposeData();
        }
    }

    public class LoftBedMod : Mod
    {
        public static LoftBedSettings Settings { get; private set; }

        public LoftBedMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<LoftBedSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label("Loft bed label shift: " + Math.Round(Settings.f1,1));
            Settings.f1 = listingStandard.Slider(Settings.f1, -100f, 100f);

            listingStandard.Label("Pawn perspective shift when laying on a loft bed: " + Math.Round(Settings.f2,1));
            Settings.f2 = listingStandard.Slider(Settings.f2, -10f, 10f);

//            listingStandard.Label("Ground-level bed pawn z-order shift when under a loft bed: " + Math.Round(Settings.f3,1));
//            Settings.f3 = listingStandard.Slider(Settings.f3, -5f, 5f);

            listingStandard.Label("Max fill percent of ground-level building that allows placing a loft bed over it: " + Math.Round(Settings.maxFillPercent,2));
            Settings.maxFillPercent = listingStandard.Slider(Settings.maxFillPercent, 0, 1.0f);

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "LoftBed".Translate();
        }
    }
}
