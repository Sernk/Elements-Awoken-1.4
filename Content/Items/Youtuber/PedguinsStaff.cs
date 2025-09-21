using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Youtuber
{
    public class PedguinsStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 15;
            Item.mana = 10;
            Item.knockBack = 1.25f;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 2;
            Item.shoot = ModContent.ProjectileType<CorruptPenguin>();
            Item.shootSpeed = 10f;
            Item.GetGlobalItem<EATooltip>().youtuber = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PenguinFeather>(), 4);
            recipe.AddIngredient(ItemID.BorealWood, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}