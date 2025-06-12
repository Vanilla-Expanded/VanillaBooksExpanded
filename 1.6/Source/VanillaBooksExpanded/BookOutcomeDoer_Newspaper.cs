using RimWorld;
using System;
using Verse;

namespace VanillaBooksExpanded
{
    public class BookOutcomeProperties_Newspaper : BookOutcomeProperties
    {
        public float joyAmountPerTick;

        public int readingTicks;

        public override Type DoerClass => typeof(BookOutcomeDoer_Newspaper);
    }

    public class BookOutcomeDoer_Newspaper : BookOutcomeDoer
    {
        public new BookOutcomeProperties_Newspaper Props => (BookOutcomeProperties_Newspaper)props;

        public override bool DoesProvidesOutcome(Pawn reader)
        {
            return Newspaper.IsRelevant;
        }

        private Newspaper Newspaper =>(Parent as Newspaper);

        public override void OnBookGenerated(Pawn author = null)
        {
            base.Book.SetJoyFactor(0);
            var randValue = Rand.RangeInclusive(60000, 180000);
            Newspaper.expireTime = Find.TickManager.TicksGame + randValue;
            Newspaper.expireTimeAbs = Find.TickManager.TicksAbs + randValue;
        }

        public override void OnReadingTick(Pawn reader, float factor)
        {
            reader.needs.joy.GainJoy(Props.joyAmountPerTick, reader.CurJobDef.joyKind);
        }

        //public override string GetBenefitsString(Pawn reader = null)
        //{
        //
        //}
        //
        //public override List<RulePack> GetTopicRulePacks()
        //{
        //
        //}

        public override void PostExposeData()
        {

        }
    }
}