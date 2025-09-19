using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Tomes
{
    public class IceSpikeTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 18;
            Item.knockBack = 2;
            Item.mana = 6;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item42;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = false;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 1;
            Item.shoot = ProjectileType<Icicle>();
            Item.shootSpeed = 9f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}