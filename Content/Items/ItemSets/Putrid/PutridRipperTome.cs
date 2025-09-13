using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    public class PutridRipperTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.damage = 60;
            Item.knockBack = 3.25f;
            Item.mana = 10;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item113;
            Item.shoot = ProjectileType<PutridRipper>();
            Item.shootSpeed = 10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}