using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    public class FrostBattleaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 70;
            Item.knockBack = 6;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;           
            Item.useTime = 40;   
            Item.useAnimation = 40;     
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;  
            Item.shoot = ProjectileType<IceWaveCheck>();
            Item.shootSpeed = 6f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, 0, 0, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}