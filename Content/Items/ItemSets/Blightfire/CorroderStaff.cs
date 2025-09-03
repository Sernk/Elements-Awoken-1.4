using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Blightfire
{
    public class CorroderStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 120;
            Item.knockBack = 1.25f;
            Item.mana = 20;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 11;
            Item.UseSound = SoundID.Item113;
            Item.shoot = ModContent.ProjectileType<CorroderMinion>();
            Item.shootSpeed = 10f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Corroder Staff");
            // Tooltip.SetDefault("Summons a corroder to protect you\nEach corroders only takes 0.75 minion slots");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Blightfire>(), 10);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
