using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Floral
{
    public class PetalBattleaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;    
            Item.damage = 45;
            Item.crit = 4;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 12;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.scale *= 1.3f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Petal>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}