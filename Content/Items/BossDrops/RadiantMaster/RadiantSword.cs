using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.BossDrops.RadiantMaster
{
    public class RadiantSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 470;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTime = 30;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<Rarity13>();
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileType<RadiantStorm>();
            Item.shootSpeed = 4f;
        }
        public override bool AltFunctionUse(Player player)
        {
            if (player.ownedProjectileCounts[ProjectileType<RadiantBlade>()] != 0) return false;
            return true;
        }
        //public override bool OnlyShootOnSwing/* tModPorter Note: Removed. If you returned true, set Item.useTime to a multiple of Item.useAnimation */ => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) Item.noUseGraphic = true;
            else Item.noUseGraphic = false;
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-3,3), Main.rand.NextFloat(-3, 3), ProjectileType<RadiantBlade>(), damage, knockBack, player.whoAmI);
                }
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item9, player.position);
                int numberProjectiles1 = Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles1; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(10));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, (int)(damage * 1.5f), knockBack, player.whoAmI);
                }
            }
            return false;
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
