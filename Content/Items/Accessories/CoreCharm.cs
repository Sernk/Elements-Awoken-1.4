using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class CoreCharm : ModItem
    {
        public int shootTimer = 120;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 4;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(7, 4));
            Const.SetSoul(Type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            shootTimer--;
            float maxDistance = 500f;
            if (player.whoAmI == Main.myPlayer)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.CanBeChasedBy(this) && Vector2.Distance(player.Center, nPC.Center) <= maxDistance && Collision.CanHit(player.Center, player.width, player.height, nPC.Center, nPC.width,nPC.height))
                    {
                        if (shootTimer <= 0)
                        {
                            int numberProjectiles = 2;
                            float Speed = 12f;
                            float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                                Projectile.NewProjectile(Const.Players(player), player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FireSkull>(), 20, 0f, Main.myPlayer, 0f, 0f);
                            }
                            shootTimer = 120;
                            return;
                        }
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagmaCrystal>(), 8);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.Bone, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}