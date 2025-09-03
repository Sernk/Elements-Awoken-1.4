using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined
{
    public class DrakoniteDrill : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 12;
            Item.damage = 40;
            Item.knockBack = 6;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.pick = 200;
            Item.tileBoost++;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item23;
            Item.shoot = ModContent.ProjectileType<Projectiles.Drills.DrakoniteDrill>();
            Item.shootSpeed = 40f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RefinedDrakonite>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}