using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaBooksExpanded
{
    [HarmonyPatch(typeof(BookOutcomeDoerGainSkillExp), "OnBookGenerated")]
    public static class BookOutcomeDoerGainSkillExp_OnBookGenerated_Patch
    {
        public static bool Prefix(BookOutcomeDoerGainSkillExp __instance)
        {
            if (GenRecipe_PostProcessProduct_Patch.curWorker != null)
            {
                var extension = GenRecipe_PostProcessProduct_Patch.curRecipe.GetModExtension<RecipeSkillBook>();
                float num = BookUtility.GetSkillExpForQuality(__instance.Quality);
                __instance.values[extension.skill] = num;
                return false;
            }
            return true;
        }
    }
}
