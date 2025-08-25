using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles.Thrown;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.YukkiKun
{
    public class AncientIchorFlagon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.damage = 54;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 7;
            Item.shoot = ModContent.ProjectileType<AncientIchorFlagonP>();
            Item.shootSpeed = 6.5f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SunFragment>(), 8);
            recipe.AddIngredient(ItemID.Ichor, 15);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}