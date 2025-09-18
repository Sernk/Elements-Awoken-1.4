using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Items.Tech.Weapons.Tier1;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier4
{
    public class Imbalancer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 43;
            Item.knockBack = 1f;
            Item.GetGlobalItem<ItemEnergy>().energy = 8;
            Item.useAnimation = 32;
            Item.useTime = 32;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item61;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 5;
            Item.shootSpeed = 8f;
            Item.shoot = ProjectileType<ImbalancerMine>();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Electrozzitron>(), 1);
            recipe.AddRecipeGroup(EARecipeGroups.AdamantiteBar, 6);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}