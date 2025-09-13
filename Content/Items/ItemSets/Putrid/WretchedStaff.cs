using ElementsAwoken.Content.Projectiles.Held.Staffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    public class WretchedStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 84;
            Item.knockBack = 3.75f;
            Item.mana = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item15;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.shoot = ProjectileType<WretchedStaffHeld>();
            Item.shootSpeed = 35f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}