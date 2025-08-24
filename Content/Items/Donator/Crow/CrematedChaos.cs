using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Crow
{
    public class CrematedChaos : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 16;
            Item.damage = 34;
            Item.mana = 5;
            Item.knockBack = 3.25f;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 8;
            Item.useAnimation = 16;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item34;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 5;
            Item.shoot = ModContent.ProjectileType<PoisonFire>();
            Item.shootSpeed = 5f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, -4);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numProj = 2;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(3));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}