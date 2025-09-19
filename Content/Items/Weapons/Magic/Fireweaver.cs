using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic
{
    public class Fireweaver : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.damage = 36;
            Item.mana = 18;
            Item.knockBack = 5;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item20;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<FireweaverProj>();
            Item.shootSpeed = 9f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.LavaBucket, 2);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
