using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Regular
{
    public class DragonFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;       
            Item.damage = 16;
            Item.mana = 5;
            Item.knockBack = 2;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item20;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<DrakoniteFireball>();
            Item.shootSpeed = 8f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Drakonite>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}