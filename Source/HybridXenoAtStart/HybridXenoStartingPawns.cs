using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace HybridXenoTypesAtPawnCreation
{
    [HarmonyPatch(typeof(StartingPawnUtility), "DrawPortraitArea")]
    public static class HybridXenoStartingPawns
    {
        [HarmonyPostfix]
        public static void HybridButton(Rect rect, int pawnIndex)

        {
            if (!ModsConfig.BiotechActive)
            {
                return;
            }
            List<Pawn> StartingAndOptionalPawns = Find.GameInitData.startingAndOptionalPawns;
            Pawn pawn = StartingAndOptionalPawns[pawnIndex];
            if (pawn.genes.Xenotype == XenotypeDefOf.Baseliner)
            {
                return;
            }
            //float BottomButSize = 250f; //Page.BottomButSize
            Vector2 BottomButSize = new Vector2(150f, 38f); // Copyed from Page
            Text.Font = GameFont.Small;
            float x = (rect.width);
            float y = rect.y + rect.height - 38f;

            if (Widgets.ButtonText(new Rect(x, y, BottomButSize.x, BottomButSize.y), "Create Hybrid"))
            {
                string XenoName = pawn.genes.XenotypeLabel;
                List<FloatMenuOption> list = new List<FloatMenuOption>{};
                foreach (XenotypeDef item in DefDatabase<XenotypeDef>.AllDefs.OrderBy((XenotypeDef xenotypeDef) => 0f - xenotypeDef.displayPriority))
                {
                    XenotypeDef xenoType = item;

                    if (xenoType != XenotypeDefOf.Baseliner)
                    {
                        list.Add(new FloatMenuOption(xenoType.LabelCap, delegate
                        {
                            HybridXenosAtStart.ApplyGenes(pawn, xenoType.AllGenes, !xenoType.inheritable, XenoName, xenoType.LabelCap);
                        }, xenoType.Icon, XenotypeDef.IconColor, MenuOptionPriority.Default, delegate (Rect r)
                        {
                            TooltipHandler.TipRegion(r, xenoType.descriptionShort ?? xenoType.description);
                        }, null, 24f, (Rect r) => Widgets.InfoCardButton(r.x, r.y + 3f, xenoType) ? true : false, null, playSelectionSound: true, 0, HorizontalJustification.Left, extraPartRightJustified: true));
                    }
                }
                foreach (CustomXenotype customXenotype in CharacterCardUtility.CustomXenotypesForReading)
                {
                    CustomXenotype customInner = customXenotype;
                    list.Add(new FloatMenuOption(customInner.name.CapitalizeFirst() + " (" + "Custom".Translate() + ")", delegate
                    {
                        HybridXenosAtStart.ApplyGenes(pawn, customInner.genes, !customInner.inheritable, XenoName, customInner.fileName);
                    }, customInner.IconDef.Icon, XenotypeDef.IconColor, MenuOptionPriority.Default, delegate (Rect r)
                    {
                    }, null, 0, extraPartRightJustified: true));
                }
                Find.WindowStack.Add(new FloatMenu(list));
            }
        }
    }
}