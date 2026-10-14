using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace HybridXenoAtStart
{
    [StaticConstructorOnStartup]
    public static class HybridXenosAtStart
    {
        static HybridXenosAtStart()
        {
            Harmony harmony = new("BP_HybridXenoAtStart");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static void ApplyGenes(Pawn pawn, List<GeneDef> genes, bool Xenogene )
        {
            var oldname = pawn.genes.XenotypeLabelCap;
            foreach (GeneDef geneDef in genes)
            {
                if (Xenogene && !pawn.genes.HasXenogene(geneDef)) 
                { 
                    pawn.genes.AddGene(geneDef, Xenogene);
                }
                if (!Xenogene && !pawn.genes.HasEndogene(geneDef))
                {
                    pawn.genes.AddGene(geneDef, Xenogene);
                }
            }
            if (pawn.genes.hybrid == false)
            {
                pawn.genes.hybrid = true;
                pawn.genes.xenotypeName = "BPHybridPrefix".Translate() + oldname + "BPHybridSuffix".Translate();
            }
        }
    }
}
