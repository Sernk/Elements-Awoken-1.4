using ElementsAwoken.Content.Projectiles.Whips;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Lantard
{
    [AutoloadEquip(EquipType.Neck)]
    public class DarknersScarf : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 11;
            Item.damage = 40;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 1;
            Item.useStyle = 5;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.vanity = true;
            Item.accessory = true;
            Item.shoot = ModContent.ProjectileType<DarknersScarfP>();
            Item.shootSpeed = 15f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 25);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, ai3);
            return false;
        }
    }
}