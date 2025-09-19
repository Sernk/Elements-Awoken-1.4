using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class ManaBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Magic;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 18;
            Item.useTurn = true;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override bool? UseItem(Player player)
        {
            int rand = Main.rand.Next(5, 15);
            player.statMana -= rand;

            int newDamage = 5 + rand;
            Item.damage = newDamage;
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddRecipeGroup(EARecipeGroups.SilverSword);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}