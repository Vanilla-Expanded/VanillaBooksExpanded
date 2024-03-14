using HarmonyLib;
using Verse;

namespace VanillaBooksExpanded
{
    public class VanillaBooksExpandedMod : Mod
    {
        public VanillaBooksExpandedMod(ModContentPack content) : base(content)
        {
            new Harmony("VanillaBooksExpandedMod").PatchAll();
        }
    }
}