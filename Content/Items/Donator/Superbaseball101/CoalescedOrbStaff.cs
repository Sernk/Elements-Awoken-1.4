using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Superbaseball101
{
    public class CoalescedOrbStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 50;
            Item.mana = 10;
            Item.knockBack = 1.25f;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 7;
            Item.useAnimation = 25;
            Item.shoot = ModContent.ProjectileType<CoalescedOrb>();
            Item.shootSpeed = 10f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MysticLeaf>(), 2);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddIngredient(ItemID.ShroomiteBar, 10);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.Register();
        }
    }
}