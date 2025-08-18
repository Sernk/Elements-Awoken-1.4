using ElementsAwoken.Content.Items.BossDrops.TheTempleKeepers;
using ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined;
using ElementsAwoken.Content.NPCs.Bosses.TheGuardian;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    /// <summary>
    /// TheGuardianSpawn
    /// </summary>
    public class GuardianSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.rare = 8;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.maxStack = 9999;
            //item.shoot = ModContent.ProjectileType<TheGuardianSpawn>();
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override bool? UseItem(Player player)
        {
            int npcIndex = NPC.NewNPC(EAU.Play(player), (int)player.Center.X, (int)player.Center.Y - 400, ModContent.NPCType<TheGuardian>(), 0, 0f, 0f, 0f, 0f, 255);
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex, 0f, 0f, 0f, 0, 0, 0);

            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TempleFragment>(), 1);
            recipe.AddIngredient(ModContent.ItemType<WyrmHeart>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RefinedDrakonite>(), 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}