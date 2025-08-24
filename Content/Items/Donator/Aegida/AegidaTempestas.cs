using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Weapons.Ranged;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Donator.Aegida
{
    public class AegidaTempestas : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 55;
            Item.knockBack = 3.5f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useAnimation = 6;
            Item.useTime = 6;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item41;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 11;
            Item.shootSpeed = 12f;
            Item.shoot = 10;
            Item.useAmmo = 97;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 70f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            int numberProjectiles = Main.rand.Next(1, 4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(3));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<TheEqualizer>(), 1);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 20);
            recipe.AddIngredient(ItemID.FragmentVortex, 12);
            recipe.AddIngredient(ItemID.Sapphire, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 50)
                return false;
            return true;
        }
    }
}