using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles.Turrets;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier2
{
    public class RifleSentrySummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 15;
            Item.knockBack = 2f;
            Item.GetGlobalItem<ItemEnergy>().energy = 8;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 2;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<RifleSentry>();
        }
        public override bool CanUseItem(Player player)
        {
            // if mouse on tile 
            Point mousePoint = Main.MouseWorld.ToTileCoordinates();
            if (Main.tile[mousePoint.X, mousePoint.Y].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[mousePoint.X, mousePoint.Y].TileType] && !Main.tileSolidTop[(int)Main.tile[mousePoint.X, mousePoint.Y].TileType] && Main.tile[mousePoint.X, mousePoint.Y].TileType != TileID.Rope)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int num154 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
            int num155 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
            }
            Projectile.NewProjectile(source, (float)Main.mouseX + Main.screenPosition.X, (float)(num155 * 16 - 24), 0f, 15f, type, damage, knockback, Main.myPlayer, 0f, 0f);
            player.UpdateMaxTurrets();
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.EvilBar, 2);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}