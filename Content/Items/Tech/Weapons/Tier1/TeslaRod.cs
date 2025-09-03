using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier1
{
    public class TeslaRod : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 15;
            Item.knockBack = 1f;
            Item.GetGlobalItem<ItemEnergy>().energy = 2;
            Item.DamageType = DamageClass.Magic;
            Item.useTurn = true;
            Item.autoReuse = false;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<TeslaSpark>();
            Item.shootSpeed = 7f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ElectricArcing"), player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.IronBar, 8);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}