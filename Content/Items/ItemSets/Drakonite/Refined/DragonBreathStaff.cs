using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined
{
    public class DragonBreathStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 50;
            Item.knockBack = 1.25f;
            Item.mana = 20;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item113;
            Item.shoot = ModContent.ProjectileType<MiniDragon>();
            Item.shootSpeed = 10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RefinedDrakonite>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
