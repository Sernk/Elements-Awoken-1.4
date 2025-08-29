using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class Windfall : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 16;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.25f;
            Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<WindfallFire>();
            Item.shootSpeed = 6f;
            Item.useAmmo = AmmoID.Gel;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .75f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}