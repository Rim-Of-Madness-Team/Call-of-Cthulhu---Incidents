using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace ROM_Incidents
{
    internal class IncidentWorker_Crows : IncidentWorker
    {
        public override bool TryExecute(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef crow = PawnKindDef.Named("ROM_PawnKind_Crow");
            IntVec3 intVec, origin;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out origin, map))
            {
                return false;
            }
            if (!RCellFinder.TryFindRandomSpotJustOutsideColony(origin, map, out intVec))
            {
                return false;
            }
            int num = Rand.RangeInclusive(8, 16);
            for (int i = 0; i < num; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10);
                Pawn newThing = PawnGenerator.GeneratePawn(crow, null);
                GenSpawn.Spawn(newThing, loc, map);
            }
            Find.LetterStack.ReceiveLetter("LetterLabelCrowsArrived".Translate(), "CrowsArrived".Translate(), LetterType.BadNonUrgent, new TargetInfo(intVec, map, false), null);
            return true;
        }
    }
}