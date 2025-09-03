using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.Projectiles;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined
{
    public class DragonTalon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 68;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<DragonTalonBall>();
            Item.shootSpeed = 7f;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(EAU.Dragonfire, 200);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2 + Main.rand.Next(0, 1);
            for (int k = 0; k < numberProjectiles; k++)
            {
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                vector2.X = player.position.X;
                vector2.Y = player.Center.Y + Main.rand.Next(-15, 15);
                Projectile.NewProjectile(source, vector2.X, vector2.Y, speed.X, speed.Y, ModContent.ProjectileType<DragonTalonBall>(), damage, Item.knockBack, Main.myPlayer, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RefinedDrakonite>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}