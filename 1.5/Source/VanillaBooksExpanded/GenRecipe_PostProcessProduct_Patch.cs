using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaBooksExpanded
{
    [HarmonyPatch(typeof(GenRecipe), "PostProcessProduct")]
    public static class GenRecipe_PostProcessProduct_Patch
    {
        public static Pawn curWorker;
        public static RecipeDef curRecipe;
        public static void Prefix(Thing product, RecipeDef recipeDef, Pawn worker)
        {
            if (product is Book && recipeDef.HasModExtension<RecipeSkillBook>())
            {
                curWorker = worker;
                curRecipe = recipeDef;
            }
        }
        public static void Postfix()
        {
            curWorker = null;
            curRecipe = null;
        }
    }
}
