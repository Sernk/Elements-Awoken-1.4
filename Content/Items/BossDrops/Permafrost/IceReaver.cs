using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Permafrost
{
    public class IceReaver : ModItem
    {
        int uses = 0;
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.knockBack = 5;
            Item.damage = 57;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.value = Item.buyPrice(0, 46, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<IceReaverP>();
            Item.shootSpeed = 18f;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 200);
        }
        public override bool CanUseItem(Player player)
        {
            uses++;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (uses % 2 == 0)
            {
                float rotation = MathHelper.ToRadians(8);
                if (player.direction == -1) rotation = -rotation;
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(rotation, -rotation, (float)player.itemAnimation / (float)Item.useAnimation));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}