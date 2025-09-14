using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.Loot;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Loot.EAIDRC;
using static ElementsAwoken.EASystem.Loot.InMultipleConditionByMode;
using static ElementsAwoken.EAUtilities.EAList;

namespace ElementsAwoken.EASystem.Global;

public class DropNPCs : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public override void OnKill(NPC npc)
    {
        Player player = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];
        #region Essence Drops in OnKill
        if (NPC.downedBoss1) { if (player.ZoneDesert && !player.ZoneBeach && !npc.SpawnedFromStatue) {int chance = 15; if (Main.rand.Next(chance) == 0) { if (Main.rand.Next(chance) == 0) { Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DesertEssence>(), 1); } } } }
        if (NPC.downedBoss3) { if (player.ZoneUnderworldHeight && !npc.SpawnedFromStatue) {int chance = 15; if (Main.rand.Next(chance) == 0) { Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FireEssence>(), 1); } } }
        if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) { if (player.ZoneSkyHeight && !npc.SpawnedFromStatue) {int chance = 15; if (Main.rand.Next(chance) == 0) { Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SkyEssence>(), 1); } } }
        if (NPC.downedPlantBoss) { if (player.ZoneSnow && !npc.SpawnedFromStatue) {int chance = 18;{ if (Main.rand.Next(chance) == 0) { Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrostEssence>(), 1); } } } }
        if (NPC.downedFishron) { if (player.ZoneBeach && !npc.SpawnedFromStatue) { int chance = 18; if (Main.rand.Next(chance) == 0) { Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<WaterEssence>(), 1); } } }
        #endregion
    }
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        #region Essence Drops in ModifyNPCLoot
        IItemDropRuleCondition[] conds;

        LeadingConditionRule Awakened;
        LeadingConditionRule Expert;
        LeadingConditionRule Normal;
        
        if (DeserNPC.Contains(npc.type))
        {
            conds = [new BIDRC(BIDRC.BossType.EyeOfCthulhu, true), new BiomeConditions(BiomeConditions.BiomeID.Desert), new NPCSpawnedFromStatue()];
            Awakened = new(new InMultipleConditionByMode(Mode.Awakened, conds));
            Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
            Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
            Awakened.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DesertEssence>(), 9)); npcLoot.Add(Awakened);
            Expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DesertEssence>(), 11)); npcLoot.Add(Expert);
            Normal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DesertEssence>(), 12)); npcLoot.Add(Normal);
        }
        if (HellNPC.Contains(npc.type))
        {
            conds = [new BIDRC(BIDRC.BossType.Skeletron, true), new BiomeConditions(BiomeConditions.BiomeID.Underworld), new NPCSpawnedFromStatue()];
            Awakened = new(new InMultipleConditionByMode(Mode.All, conds));
            Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
            Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
            Awakened.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FireEssence>(), 10)); npcLoot.Add(Awakened);
            Expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FireEssence>(), 12)); npcLoot.Add(Expert);
            Normal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FireEssence>(), 13)); npcLoot.Add(Normal);
        }
        if (SkyNPC.Contains(npc.type))
        {
            conds = [new BiomeConditions(BiomeConditions.BiomeID.Sky), new NPCSpawnedFromStatue(), new BIDRC(BIDRC.BossType.Destroyer, true), new BIDRC(BIDRC.BossType.Twins, true), new BIDRC(BIDRC.BossType.SkeletronPrime, true)];
            Awakened = new(new InMultipleConditionByMode(Mode.Awakened, conds));
            Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
            Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
            Awakened.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SkyEssence>(), 6)); npcLoot.Add(Awakened);
            Expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SkyEssence>(), 8)); npcLoot.Add(Expert);
            Normal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SkyEssence>(), 10)); npcLoot.Add(Normal);
        }
        if (FrostNPC.Contains(npc.type))
        {
            conds = [new BiomeConditions(BiomeConditions.BiomeID.Frost), new NPCSpawnedFromStatue(), new BIDRC(BIDRC.BossType.Plantera, true)];
            Awakened = new(new InMultipleConditionByMode(Mode.Awakened, conds));
            Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
            Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
            Awakened.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FrostEssence>(), 10)); npcLoot.Add(Awakened);
            Expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FrostEssence>(), 14)); npcLoot.Add(Expert);
            Normal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FrostEssence>(), 16)); npcLoot.Add(Normal);
        }
        if (FrostNPC_2.Contains(npc.type))
        {
            conds = [new NPCSpawnedFromStatue(), new BIDRC(BIDRC.BossType.Plantera, true)];
            Awakened = new(new InMultipleConditionByMode(Mode.Awakened, conds));
            Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
            Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
            Awakened.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FrostEssence>(), 10)); npcLoot.Add(Awakened);
            Expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FrostEssence>(), 14)); npcLoot.Add(Expert);
            Normal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FrostEssence>(), 16)); npcLoot.Add(Normal);
        }
        if (BeachNPC.Contains(npc.type))
        {
            conds = [new BiomeConditions(BiomeConditions.BiomeID.InBeach), new BIDRC(BIDRC.BossType.DukeFishron, true)];
            Awakened = new(new InMultipleConditionByMode(Mode.Awakened, conds));
            Expert = new(new InMultipleConditionByMode(Mode.Expert, conds));
            Normal = new(new InMultipleConditionByMode(Mode.Normal, conds));
            Awakened.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WaterEssence>(), 12)); npcLoot.Add(Awakened);
            Expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WaterEssence>(), 15)); npcLoot.Add(Expert);
            Normal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WaterEssence>(), 16)); npcLoot.Add(Normal);
        }
        #endregion
    }
}