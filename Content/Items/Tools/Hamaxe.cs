using ElementsAwoken.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tools
{
    public class Hamaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 60;
            Item.useTime = 6;
            Item.useAnimation = 20;
            Item.useTurn = true;
            Item.axe = 9;
            Item.hammer = 45;
            Item.useStyle = 1;
            Item.knockBack = 4.5f;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FleshClump>(), 8);
            recipe.AddIngredient(ItemID.Bone, 24);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(9) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}