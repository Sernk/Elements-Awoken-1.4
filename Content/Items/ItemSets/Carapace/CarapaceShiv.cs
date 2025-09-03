using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Carapace
{
    public class CarapaceShiv : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.damage = 14;
            Item.knockBack = 1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.rare = 0;
        }
        public override bool AltFunctionUse(Player player)
        {
            if (player.velocity.Y == 0)
            {
                return true;
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 toMouse = Main.MouseWorld - player.Center;
                toMouse.Normalize();
                toMouse *= 10f;
                if ((toMouse.X > 0 && player.velocity.X < 5) || (toMouse.X < 0 && player.velocity.X > -5)) player.velocity += new Vector2(toMouse.X, toMouse.Y);
            }
            else { }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}