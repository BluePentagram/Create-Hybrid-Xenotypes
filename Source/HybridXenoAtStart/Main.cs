using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace HybridXenoTypesAtPawnCreation
{
    [StaticConstructorOnStartup]
    public static class HybridXenosAtStart
    {
        static HybridXenosAtStart()
        {
            Harmony harmony = new Harmony("BP_HybridXenoAtStart");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static void ApplyGenes(Pawn pawn, List<GeneDef> genes, bool Xenogene, string XenoName, string HybridXenoName)
        {
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
                pawn.genes.xenotypeName = XenoName + "-" + HybridXenoName;
            }
        }
    }
}
