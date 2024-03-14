using HarmonyLib;
using Verse;

namespace VanillaBooksExpanded
{
    [HarmonyPatch(typeof(Book), "GenerateBook")]
    public static class Book_GenerateBook_Patch
    {
        public static void Prefix(Book __instance, ref Pawn author, ref long? fixedDate)
        {
            if (GenRecipe_PostProcessProduct_Patch.curWorker != null)
            {
                author = GenRecipe_PostProcessProduct_Patch.curWorker;
                fixedDate = GenTicks.TicksAbs;
            }
        }
    }
}
