using ElementsAwoken.Content.NPCs.Bosses.Obsidious;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class ObsidiousSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = 5;
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
            if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<ObsidiousHuman>());
            else NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<ObsidiousHuman>(), 0f, 0f, 0, 0, 0); 
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 15);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 3);
            recipe.AddIngredient(ItemID.SoulofNight, 3);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}