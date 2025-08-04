using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Spears;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Volcanox
{
    public class EmberBurst : ModItem
    {
        public override void SetDefaults()
        {       
            Item.width = 66;
            Item.damage = 210;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 19;
            Item.useStyle = 5;
            Item.useTime = 19;
            Item.knockBack = 8.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 66;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 11;
            Item.shoot = ModContent.ProjectileType<EmberBurstP>();
            Item.shootSpeed = 11f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X / 2, speed.Y / 2, ModContent.ProjectileType<FirelashFlames>(), damage / 2, knockback, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if(Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
            }
        }
    }
}