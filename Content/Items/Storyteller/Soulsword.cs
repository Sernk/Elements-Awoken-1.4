using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Storyteller
{
    public class Soulsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 25;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 4;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SoulswordSoul>()] < 3)
            {
                Projectile.NewProjectile(EAU.Play(player), target.Center.X, target.Center.Y, 0f, -1.5f, ModContent.ProjectileType<SoulswordSoul>(), hit.Damage, hit.Knockback, Main.myPlayer);
            }
        }
    }
}
