﻿using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Accessories.Tier2
{
    public class RustedMechanism : ModItem
    {
        public bool hasShot = false;
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 2;    
            Item.accessory = true;

        }
        //public override void SetStaticDefaults()
        //{
        //    DisplayName.SetDefault("Rusted Mechanism");
        //    Tooltip.SetDefault("When you are hit, you shoot out a bunch of the first ammo you have\nConsumes 3 energy on use");
        //}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            int ammoType = 0;
            int whichSlot = 0;
            for (int i = 0; i < 4; i++)
            {
                if (ammoType == 0)
                {
                    int slot = 54 + i;
                    if (Main.LocalPlayer.inventory[slot].type != 0 && Main.LocalPlayer.inventory[slot].type != ItemID.JungleSpores)
                    {
                        ammoType = Main.LocalPlayer.inventory[slot].shoot;
                        whichSlot = slot;
                    }
                }
            }
            if (player.immune && ammoType != 0)
            {
                if (!hasShot)
                {
                    if (modPlayer.energy > 3)
                    {
                        modPlayer.energy -= 3;
                        float rotation = MathHelper.TwoPi;
                        float numProj = 8;
                        float speed = 4;
                        for (int i = 0; i < numProj; i++)
                        {
                            Vector2 perturbedSpeed = (rotation / numProj * i).ToRotationVector2() * speed;
                            int num1 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ammoType, 10, 2f, 0);
                            Main.projectile[num1].noDropItem = true;
                        }
                        if (Main.LocalPlayer.inventory[whichSlot].consumable)
                        {
                            Main.LocalPlayer.inventory[whichSlot].stack--;
                        }
                    }
                    hasShot = true;
                }
            }
            else
            {
                hasShot = false;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.IronBar, 8);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 10);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
