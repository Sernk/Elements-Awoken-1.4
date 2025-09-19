using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles.Flails;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class LeashedHungry : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 42;
            Item.knockBack = 4;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.channel = true;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<HungryFlailP>();
            Item.shootSpeed = 14f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddIngredient(ModContent.ItemType<DemonicFleshClump>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}