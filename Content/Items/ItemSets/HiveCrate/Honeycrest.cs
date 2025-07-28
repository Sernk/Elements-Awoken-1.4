using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.HiveCrate
{
    public class Honeycrest : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;       
            Item.damage = 25;
            Item.knockBack = 2;
            Item.useTime = 24;
            Item.useAnimation = 12;
            Item.useStyle = 3;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 1;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 6f;
            Item.shoot = ModContent.ProjectileType<Projectiles.HoneycrestStinger>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, player.direction * Item.shootSpeed, 0, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.AddBuff(BuffID.Honey, 300);
        }
    }
}