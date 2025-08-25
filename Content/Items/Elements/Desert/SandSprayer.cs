using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    public class SandSprayer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 16;           
            Item.damage = 8;
            Item.knockBack = 3.25f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item34;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.shoot = ProjectileType<SandSpray>();
            Item.shootSpeed = 4.5f;
            Item.useAmmo = AmmoID.Sand;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .8f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<DesertEssence>(), 4);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 25);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 10);
            recipe.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}
