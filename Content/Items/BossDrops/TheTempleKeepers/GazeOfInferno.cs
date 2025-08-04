using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheTempleKeepers
{
    public class GazeOfInferno : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 96;
            Item.knockBack = 2;
            Item.mana = 6;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.staff[Item.type] = true;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item8;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<InfernoEye>();
            Item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<InfernoEye>()] != 0)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center.X, player.Center.Y - 70, 0f, 0f, ModContent.ProjectileType<InfernoEye>(), damage, knockback, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
            return false;
        }
    }
}
 