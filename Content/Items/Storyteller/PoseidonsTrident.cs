using ElementsAwoken.Content.Projectiles.Spears;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Storyteller
{
    public class PoseidonsTrident : ModItem
    {
        public override void SetDefaults()
        {       
            Item.damage = 66;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.useTime = 10;
            Item.knockBack = 8.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 60;
            Item.width = 60;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.shoot = ModContent.ProjectileType<PoseidonsTridentP>();
            Item.shootSpeed = 8f;
            Item.rare = 8;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile proj = Main.projectile[Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, 409, damage, knockback, player.whoAmI, 0.0f, 0.0f)];
            //proj.DamageType = DamageClass.Default;
            proj.DamageType = DamageClass.Melee;
            return true;
        }
    }
}
