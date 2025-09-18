using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Flower
{
    public abstract class FlowerClass : ModItem
    {
        public abstract int Damage { get; }
        public abstract int ProjType { get; }
        public abstract int Materials { get; }
        public virtual float ShotSpeed => 6f;
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = Damage;
            Item.knockBack = 2;
            Item.mana = 5;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item8;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 0, 50, 0);
            Item.rare = 1;
            Item.shoot = ProjType;
            Item.shootSpeed = ShotSpeed;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Materials, 2);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}
