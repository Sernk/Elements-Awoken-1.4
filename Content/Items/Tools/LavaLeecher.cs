﻿using ElementsAwoken.Content.Items.BossDrops.Volcanox;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tools
{
    public class LavaLeecher : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 12;
            Item.useTime = 5;
            Item.width = 20;
            Item.height = 20;
            Item.autoReuse = true;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.tileBoost += 2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VolcanicStone>(), 8);
            recipe.AddIngredient(ItemID.SuperAbsorbantSponge, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(9) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}