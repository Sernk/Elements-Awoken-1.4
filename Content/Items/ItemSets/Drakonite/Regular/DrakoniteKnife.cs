using ElementsAwoken.Content.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Regular
{
    public class DrakoniteKnife : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.damage = 12;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 10;
            Item.useStyle = 1;
            Item.useTime = 15;
            Item.knockBack = 1.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 30;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<DrakoniteKnifeP>();
            Item.shootSpeed = 11f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(40);
            recipe.AddIngredient(ModContent.ItemType<Drakonite>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}