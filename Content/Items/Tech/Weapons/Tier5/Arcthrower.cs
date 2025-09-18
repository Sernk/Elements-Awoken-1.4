using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Items.Tech.Weapons.Tier3;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier5
{
    public class Arcthrower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 55;
            Item.knockBack = 1f;
            Item.useAnimation = 15;
            Item.useTime = 5;
            Item.reuseDelay = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.shootSpeed = 12f;
            Item.shoot = ProjectileType<ZapMasterLightning>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Item/ElectricArcing"), Item.position);

            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(4));
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (modPlayer.energy >= 5) modPlayer.energy -= 5;
            else return false;
            return base.CanUseItem(player);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 6);
            recipe.AddIngredient(ItemType<Zapmaster>(), 1);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<CopperWire>(), 4);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ItemType<Transistor>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}