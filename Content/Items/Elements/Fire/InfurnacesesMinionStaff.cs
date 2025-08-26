using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Fire
{  
    public class InfurnicesesMinionStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 28;
            Item.width = 26;
            Item.damage = 22;
            Item.mana = 10;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<FireElemental>();
            Item.shootSpeed = 7f; 
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.FireEssence, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}