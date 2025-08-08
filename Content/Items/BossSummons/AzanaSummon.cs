using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.Content.Items.Chaos;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.NPCs.Bosses.Azana;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class AzanaSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.useAnimation = 45;
            Item.maxStack = 9999;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override bool CanUseItem(Player player)
        {
            return
            !NPC.AnyNPCs(ModContent.NPCType<AzanaEye>()) &&!NPC.AnyNPCs(ModContent.NPCType<Azana>());           
        }
        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AzanaEye>());
            else NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<AzanaEye>(), 0f, 0f, 0, 0, 0);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChaoticFlare>(), 30);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 45);
            recipe.AddIngredient(ModContent.ItemType<VoidAshes>(), 12);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}