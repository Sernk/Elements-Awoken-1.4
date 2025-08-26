using ElementsAwoken.Content.Items.Essence;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.Projectiles;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    public class BonfirePummel : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 22;
            Item.knockBack = 2;
            Item.mana = 16;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item42;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<FireBall>();
            Item.shootSpeed = 11f;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Torch);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}