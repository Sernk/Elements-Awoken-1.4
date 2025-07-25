using ElementsAwoken.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.EASystem;

namespace ElementsAwoken.Content.Items.Other
{
    public class DeathMirror : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.UseSound = SoundID.Item6;
            Item.useStyle = 4;
            Item.useTurn = true;
            Item.consumable = false;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 6;
            return;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hell's Reflection");
            // Tooltip.SetDefault("Teleports the player to the last death postition\nMust teleport back within 30 seconds\nHas a 5 minute cooldown");
        }
        public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.hellsReflectionCD > 0)
            {
                Main.NewText("The reflection is still too dim... " + (int)modPlayer.hellsReflectionCD / 60 + " seconds remain", Color.DarkOrange);
                return false;
            }
            return player.lastDeathTime > DateTime.MinValue && modPlayer.hellsReflectionTimer > 0;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.Teleport(new Vector2(player.lastDeathPostion.X, player.lastDeathPostion.Y - player.height));
            modPlayer.hellsReflectionCD = 18000;
            return true;
        }
        public override void HoldItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.hellsReflectionTimer > 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    int num5 = Dust.NewDust(player.position, player.width, player.height, 60, 0f, 0f, 200, default(Color), 0.5f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].velocity *= 0.75f;
                    Main.dust[num5].fadeIn = 1.3f;
                    Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector.Normalize();
                    vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                    Main.dust[num5].velocity = vector;
                    vector.Normalize();
                    vector *= 34f;
                    Main.dust[num5].position = player.Center - vector;
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicMirror, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 4);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(ModContent.ItemType<MysticLeaf>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
