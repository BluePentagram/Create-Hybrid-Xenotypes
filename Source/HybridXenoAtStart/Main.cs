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
            Log.Message("Hybrid Pawns loaded successfully! Nothing Here!!!");

            // *Uncomment for Harmony*
            Harmony harmony = new Harmony("BP_HybridXenoAtStart");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static void ApplyGenes(Pawn pawn, List<GeneDef> genes, bool Xenogene, string XenoName, string HybridXenoName)
        {
            foreach (GeneDef geneitem in genes)
            {
                pawn.genes.AddGene(geneitem, Xenogene);
                if (pawn.genes.hybrid == false)
                {
                    pawn.genes.hybrid = true;
                    pawn.genes.xenotypeName = XenoName + "-" + HybridXenoName;
                }

            }
        }
    }
}
