using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    public class TaintedPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 60;
            Item.damage = 37;
            Item.pick = 215;
            Item.knockBack = 6f;
            Item.useTime = 5;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 46,0,0,150);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
