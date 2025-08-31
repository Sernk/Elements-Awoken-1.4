using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.NPCs.Bosses.Infernace;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class InfernaceSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = 3;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ZoneUnderworldHeight;
        }
        public override bool? UseItem(Player player)
        {
            Main.NewText(ModContent.GetInstance<EALocalization>().InfernaceSummon, Color.Orange.R, Color.Orange.G, Color.Orange.B);

            int npcIndex = NPC.NewNPC(EAU.Play(player), (int)player.Center.X, (int)player.Center.Y - 300, ModContent.NPCType<Infernace>(), 0, 0f, 0f, 0f, 0f, 255);
            Main.npc[npcIndex].ai[1] = -300;
            Main.npc[npcIndex].ai[3] = -1;
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex, 0f, 0f, 0f, 0, 0, 0);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ItemID.Obsidian, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}