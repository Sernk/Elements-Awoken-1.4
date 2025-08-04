using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheTempleKeepers
{
    public class TheAllSeer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.damage = 80;
            Item.knockBack = 5;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<FireRunes>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(1f, 1f).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(source, player.Center.X + Main.rand.Next(-300, 300), player.Center.Y + Main.rand.Next(-300, 300), perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FireRunes>(), damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}