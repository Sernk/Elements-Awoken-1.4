using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI.Tooltips
{
    /// <summary>
    /// Отображает условия выпадения предметов в подсказках, только в recipe browser.
    /// </summary>
    public class ConditionsForRecipeBrowser : GlobalItem
    {
        private static readonly Dictionary<int, List<TooltipLine>> _dropTooltipCache = [];

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if(ModContent.GetInstance<Config>().RecipeBrowser == false) return;
            if (!_dropTooltipCache.TryGetValue(item.type, out var cachedLines))
            {
                cachedLines = [];

                var conditionToNpcNames = new Dictionary<string, List<string>>();

                foreach (var kvp in ContentSamples.NpcsByNetId)
                {
                    int npcNetId = kvp.Key;
                    NPC npcSample = kvp.Value;

                    var rules = Main.ItemDropsDB.GetRulesForNPCID(npcNetId, includeGlobalDrops: true);
                    if (rules == null || rules.Count == 0) continue;

                    foreach (IItemDropRule rule in rules)
                    {
                        var drops = new List<DropRateInfo>();
                        rule.ReportDroprates(drops, new DropRateInfoChainFeed(1f));

                        foreach (var dr in drops)
                        {
                            if (dr.itemId != item.type) continue;

                            float chance = dr.dropRate;
                            string conditionsText = "";

                            if (dr.conditions != null)
                            {
                                foreach (var cond in dr.conditions)
                                {
                                    if (cond.CanShowItemDropInUI())
                                    {
                                        string desc = cond is IProvideItemConditionDescription prov ? prov.GetConditionDescription() : cond.ToString();

                                        if (!string.IsNullOrEmpty(desc))
                                        {
                                            Player player = Main.LocalPlayer;
                                            if (conditionsText.Length > 0) conditionsText += ", ";
                                            conditionsText += desc;
                                        }
                                    }else continue;
                                }
                            }

                            if (string.IsNullOrEmpty(conditionsText)) continue;

                            string conditionLine = $" {conditionsText} ({chance * 100f:0.##}%)";

                            if (!conditionToNpcNames.TryGetValue(conditionLine, out var npcList))
                            {
                                npcList = [];
                                conditionToNpcNames[conditionLine] = npcList;
                            }
                            npcList.Add(npcSample.FullName);
                        }
                    }
                }
                foreach (var kv in conditionToNpcNames)
                {
                    string conditionLine = kv.Key;
                    string text = $"[i:{item.type}] {conditionLine}";
                    cachedLines.Add(new TooltipLine(Mod, $"TooltipNpcDrop_{item.type}_{conditionLine}", text));
                }
                _dropTooltipCache[item.type] = cachedLines;
            }
            tooltips.AddRange(cachedLines);
        }
    }
}