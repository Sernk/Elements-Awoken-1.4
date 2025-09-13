using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Radia
{
    public class RadiantKatana : ModItem
    {
        private int timer = 0;
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 430;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileType<RadiantKatanaStar>();
            Item.shootSpeed = 13f;
        }
        public override bool AltFunctionUse(Player player)
        {
            if (timer > 0) return false;
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) Item.noUseGraphic = true;
            else Item.noUseGraphic = false;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile slash  = Main.projectile[Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ProjectileType<RadiantKatanaSlash>(), damage, knockback, player.whoAmI)];
                slash.spriteDirection = player.direction;
                timer = 90;
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item9,player.position);
                int numberProjectiles1 = Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles1; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(5));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            timer--;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, EAU.PinkFlame);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
