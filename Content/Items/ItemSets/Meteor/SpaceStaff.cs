using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Meteor
{
    public class SpaceStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 14;
            Item.mana = 9;
            Item.knockBack = 5;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item42;
            Item.shoot = ModContent.ProjectileType<MeteoricBolt>();
            Item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(2,4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(18));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            Main.NewText("", Main.DiscoColor);
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            if (player.armor[0].type == ItemID.MeteorHelmet && player.armor[1].type == ItemID.MeteorSuit && player.armor[2].type == ItemID.MeteorLeggings) mult = 0;
        }
    }
}