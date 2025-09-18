using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier4
{
    public class PlasmaRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 34;
            Item.damage = 30;
            Item.knockBack = 3.75f;
            Item.GetGlobalItem<ItemEnergy>().energy = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 4;
            Item.reuseDelay = 12;
            Item.useAnimation = 6;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
            Item.shoot = 10;
            Item.shootSpeed = 20f;
            Item.useAmmo = 97;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item12, player.position);
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<CoilRound>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 8);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PalladiumBar, 8);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
