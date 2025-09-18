using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier3
{
    public class Zapmaster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 39;
            Item.knockBack = 1f;
            Item.GetGlobalItem<ItemEnergy>().energy = 8;
            Item.useAnimation = 32;
            Item.useTime = 32;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item91;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.shootSpeed = 8f;
            Item.shoot = ProjectileType<ZapMasterLightning>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<Capacitor>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
