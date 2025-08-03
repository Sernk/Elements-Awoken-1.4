using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.ScourgeFighter
{
    public class ScourgeSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 32;
            Item.knockBack = 2;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<BountyP>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float rotation = MathHelper.TwoPi;
            float numProj = 6;
            float speed = 12f;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = (rotation / numProj * i).ToRotationVector2() * speed;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
            }
            return false;
        }
    }
}