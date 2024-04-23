using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace VanillaBooksExpanded
{
    [HarmonyPatch(typeof(GenRecipe), "MakeRecipeProducts")]
    public static class GenRecipe_MakeRecipeProducts_Patch
    {
        public static Pawn curWorker;
        public static RecipeDef curRecipe;
        [HarmonyPriority(int.MaxValue)]
        public static void Prefix(RecipeDef recipeDef, Pawn worker)
        {
            if (recipeDef.HasModExtension<RecipeSkillBook>())
            {
                curWorker = worker;
                curRecipe = recipeDef;
            }
        }

        [HarmonyPriority(int.MinValue)]
        public static IEnumerable<Thing> Postfix(IEnumerable<Thing> __result)
        {
            foreach (Thing thing in __result)
            {
                yield return thing;
            }
            curWorker = null;
            curRecipe = null;
        }
    }
}
