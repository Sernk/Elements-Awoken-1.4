using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class SixShooter : ModItem
    {
        public int shotNum = 0;

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 28;
            Item.damage = 55;
            Item.knockBack = 5f;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item41;
            Item.shootSpeed = 12f;
            Item.shoot = 10;
            Item.useAmmo = 97;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            shotNum++;

            if (shotNum == 5) Item.reuseDelay = 40;
            else Item.reuseDelay = 0;
            if (shotNum == 6) SoundEngine.PlaySound(new SoundStyle(EAU.SoundPath("RevolverReload")), Item.position);
            if (shotNum > 6) shotNum = 1;

            int projDamage = shotNum == 6 ? damage * 4 : damage;
            CombatText.NewText(player.getRect(), shotNum == 6 ? Color.Purple : Color.LightPink, shotNum, false, false);
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, projDamage, knockback, player.whoAmI);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Revolver, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 8);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}