using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Arrows;
using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class ExtinctionBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 64;
            Item.knockBack = 5;
            Item.damage = 300;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.shoot = 10;
            Item.shootSpeed = 30f;
            Item.useAmmo = 40;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            float speedX = velocity.X;
            float speedY = velocity.Y;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.altFunctionUse != 2)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ModContent.ProjectileType<ExtinctionArrow>();
                }
                int numProj = 3;
                for (int i = 0; i < numProj; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            else
            {
                Projectile.NewProjectile(source, position.X, position.Y - 100, 0f, 0f, ModContent.ProjectileType<VoidPortal>(), damage, knockBack, player.whoAmI);
                modPlayer.voidPortalCooldown = 1800;
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.altFunctionUse == 2)
            {
                if (modPlayer.voidPortalCooldown > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public override void HoldItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.voidPortalCooldown <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    int num5 = Dust.NewDust(player.position, player.width, player.height, EAU.PinkFlame, 0f, 0f, 200, default(Color), 0.5f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].velocity *= 0.75f;
                    Main.dust[num5].fadeIn = 1.3f;
                    Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector.Normalize();
                    vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                    Main.dust[num5].velocity = vector;
                    vector.Normalize();
                    vector *= 34f;
                    Main.dust[num5].position = player.Center - vector;
                }
            }
        }
    }
}