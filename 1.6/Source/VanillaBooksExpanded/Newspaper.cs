using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Grammar;

namespace VanillaBooksExpanded
{
    public class Newspaper : Book
    {
        public int expireTime = 0;
        public int expireTimeAbs = 0;
        public bool IsRelevant => GenDate.DaysPassedAt(this.expireTime) >= GenDate.DaysPassed;

        public override void Tick()
        {
            base.Tick();
            if (Find.TickManager.TicksGame % 60 == 0)
            {
                if (!IsRelevant && this.Map != null)
                {
                    FilthMaker.TryMakeFilth(this.Position, this.Map, VBE_DefOf.VBE_Filth_Newspaper);
                    this.Destroy(DestroyMode.Vanish);
                }
            }
        }

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString() + "\n");
            Vector2 vector = Find.WorldGrid.LongLatOf(this.Map.Tile);
            stringBuilder.Append("VBE.NewspaperRelevantUntil".Translate() + GenDate.DateReadoutStringAt((long)this.expireTimeAbs, vector));
            return stringBuilder.ToString().TrimEndNewlines();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.expireTime, "expireTime", 0);
            Scribe_Values.Look<int>(ref this.expireTimeAbs, "expireTimeAbs", 0);
        }
    }
}

