using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Puff
{
    public class PuffTorch : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.holdStyle = 1;
            Item.noWet = true;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.Puff.PuffTorch>();
            Item.flame = true;
            Item.value = 50;
            ItemID.Sets.Torches[Type] = true;
        }
        public override void HoldItem(Player player)
        {
            if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0) Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, DustID.Enchanted_Pink);
            Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
            if (!Item.wet) Lighting.AddLight(position, 1f, 1f, 1f);
        }
        public override void PostUpdate() { if (!Item.wet) Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 1f, 1f, 1f); }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(3);
            recipe.AddIngredient(ItemID.Wood, 1);
            recipe.AddIngredient(ModContent.ItemType<Puffball>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}