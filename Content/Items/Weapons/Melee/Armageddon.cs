using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class Armageddon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;      
            Item.damage = 67;
            Item.knockBack = 5;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 18;
            Item.useTime = 25;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<ArmageddonBlade>();
            Item.shootSpeed = 18f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, Main.myPlayer,Main.rand.Next(3));
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 200);
            target.AddBuff(BuffID.OnFire, 200);
            target.AddBuff(BuffID.Frostburn, 200);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddIngredient(ItemID.CursedFlame, 12);
            recipe.AddIngredient(ItemID.FrostCore, 3);
            recipe.AddIngredient(ItemID.LivingFireBlock, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
