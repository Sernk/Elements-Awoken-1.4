using ElementsAwoken.Content.Projectiles.Whips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class LightsAffliction : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 11;
            Item.damage = 186;
            Item.knockBack = 4f;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.useStyle = 5;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item116;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<LightsAfflictionP>();
            Item.shootSpeed = 15f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            for (int i = 0; i < 2; ++i)
            {
                float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
                Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, ai3);
            }
            return false;
        }
    }
}