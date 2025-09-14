using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.ToySlime
{  
    public class ToyRobotControlRod : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.damage = 21;
            Item.mana = 10;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ProjectileType<ToyRobot>();
            Item.shootSpeed = 7f; 
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}