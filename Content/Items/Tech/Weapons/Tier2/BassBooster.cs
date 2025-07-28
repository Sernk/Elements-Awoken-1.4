using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier2
{
    public class BassBooster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 22;
            Item.knockBack = 1f;
            Item.GetGlobalItem<ItemEnergy>().energy = 6;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 5;
            Item.UseSound = Item.UseSound = new SoundStyle("ElementsAwoken/Sounds/Item/BassBoost");
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 1;
            Item.shootSpeed = 6f;
            Item.shoot = ModContent.ProjectileType<BassBoost>();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 8);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 10);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}