using ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined;
using ElementsAwoken.Content.NPCs.Bosses.TheTempleKeepers;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class AncientDragonSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = 2;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime;
        }
        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<TheEye>());
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AncientWyrmHead>());
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<TheEye>(), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<AncientWyrmHead>(), 0f, 0f, 0, 0, 0);
            }
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentNebula, 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 1);
            recipe.AddIngredient(ItemID.FragmentStardust, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 1);
            recipe.AddIngredient(ModContent.ItemType<RefinedDrakonite>(), 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}