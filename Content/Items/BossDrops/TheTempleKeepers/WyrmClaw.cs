using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheTempleKeepers
{
    public class WyrmClaw : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.damage = 87;
            Item.knockBack = 7f;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item71;
            Item.DamageType = DamageClass.Melee;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<WyrmClawSlash>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float pi = 0.314159274f;
            int numProjectiles = 3;
            Vector2 vector14 = new Vector2(speed.X, speed.Y);
            vector14.Normalize();
            vector14 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
            for (int num123 = 0; num123 < numProjectiles; num123++)
            {
                float num124 = (float)num123 - ((float)numProjectiles - 1f) / 2f;
                Vector2 vector15 = vector14.RotatedBy((double)(pi * num124), default(Vector2));
                if (!flag11)
                {
                    vector15 -= vector14;
                }
                Projectile.NewProjectile(source, vector2.X + vector15.X, vector2.Y + vector15.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
    }
}