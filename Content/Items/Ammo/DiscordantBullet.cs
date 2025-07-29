using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.Ammo
{
    public class DiscordantBullet : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.damage = 32;
            Item.knockBack = 1.5f;
            Item.consumable = true;
            Item.DamageType = DamageClass.Ranged;
            Item.maxStack = 9999;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.shoot = ModContent.ProjectileType<Projectiles.Bullets.DiscordantBulletP>();
            Item.shootSpeed = 24f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaotron Bullet");
            // Tooltip.SetDefault("The chaos causes them to teleport near living organisms");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.MusketBall, 50);
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 1);
            recipe.AddTile(ModContent.TileType<ChaoticCrucible>());
            recipe.Register();
        }
    }
}
