using ElementsAwoken.Content.Items.Chaos;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Crow
{
    public class SWRL : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 16;
            Item.damage = 140;
            Item.knockBack = 3.25f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item61;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.shootSpeed = 19f;
            Item.shoot = ProjectileID.RocketI;
            Item.useAmmo = AmmoID.Rocket;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, -4);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .5f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == ProjectileID.RocketI)
            {
                type = ModContent.ProjectileType<SWRLRocket>();
            }
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(8));
    
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChaoticFlare>(), 10);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 4);
            recipe.AddIngredient(ItemID.SDMG, 1);
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
