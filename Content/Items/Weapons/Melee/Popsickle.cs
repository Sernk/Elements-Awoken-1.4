using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class Popsickle : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 78;
            Item.DamageType = DamageClass.Melee;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 20;
            Item.useTurn = true;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 7;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 8;
            Item.UseSound = SoundID.Item71;
            Item.shoot = ModContent.ProjectileType<BubbleRed>();
            Item.shootSpeed = 24f;
            Item.autoReuse = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2 + Main.rand.Next(2); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                switch (Main.rand.Next(4))
                {
                    case 0: type = ModContent.ProjectileType<BubbleBlue>(); break;
                    case 1: type = ModContent.ProjectileType<BubbleRed>(); break;
                    case 2: type = ModContent.ProjectileType<BubblePurple>(); break;
                    case 3: type = ModContent.ProjectileType<BubbleGreen>(); break;
                    default: break;
                }
                float rand = Main.rand.Next(2, 4);
                rand = rand * 4;
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X / rand, perturbedSpeed.Y / rand, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(7) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 63, 0, 0, 200, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
                Main.dust[dust].scale *= 1f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BubbleGun, 1);
            recipe.AddIngredient(ItemID.DeathSickle, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
