using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.NPCs.Bosses.Aqueous;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class AqueousSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = 6;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ZoneBeach)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().AqueousSummon, Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Aqueous>());
            else NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<Aqueous>(), 0f, 0f, 0, 0, 0);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 5);
            recipe.AddIngredient(ItemID.Ectoplasm, 6);
            recipe.AddIngredient(ItemID.Seashell, 2);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}