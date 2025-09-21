using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.Projectiles.Minions.SoulSkull;

namespace ElementsAwoken.Content.Items.Weapons.Summon
{
    public class SoulburnerStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.damage = 60;
            Item.mana = 10;
            Item.knockBack = 3;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<SoulSkull>();
            Item.shootSpeed = 7f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.Ectoplasm, 8);
            recipe.AddIngredient(ItemID.Bone, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
