using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace VanillaBooksExpanded
{
    [HarmonyPatch(typeof(JobDriver_Reading), "ReadBook")]
    public static class JobDriver_Reading_ReadBook_Patch
    {
        public static void Prefix(ref int duration, JobDriver_Reading __instance)
        {
            var compBook = __instance.Book.GetComp<CompBook>();
            var doer = compBook.GetDoer<BookOutcomeDoer_Newspaper>();
            if (doer != null)
            {
                duration = doer.Props.readingTicks;
            }
        }

        public static void Postfix(Toil __result, JobDriver_Reading __instance)
        {
            __result.AddFinishAction(delegate
            {
                TryGainLibraryRoomThought(__instance.pawn);
            });
        }

        public static void TryGainLibraryRoomThought(Pawn pawn)
        {
            if (DefDatabase<HistoryEventDef>.GetNamedSilentFail("VME_ReadBook") != null)
            {
                Find.HistoryEventsManager.RecordEvent(new HistoryEvent(DefDatabase<HistoryEventDef>.GetNamedSilentFail("VME_ReadBook"), pawn.Named(HistoryEventArgsNames.Doer)), true);
            }

            Room room = pawn.GetRoom();
            if (room != null && room.Role == VBE_DefOf.VBE_Library)
            {
                int scoreStageIndex = RoomStatDefOf.Impressiveness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Impressiveness));
                if (pawn.needs.mood != null && VBE_DefOf.VBE_JoyActivityInImpressiveLibraryRoom.stages[scoreStageIndex] != null)
                {
                    pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtMaker.MakeThought(VBE_DefOf.VBE_JoyActivityInImpressiveLibraryRoom, scoreStageIndex));
                }
            }
        }
    }
}