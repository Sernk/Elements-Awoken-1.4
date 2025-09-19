using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic
{
    public class ImpishWarhorn : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 18;
            Item.damage = 20;
            Item.mana = 20;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = 5;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<ImpishWave>();
            Item.shootSpeed = 24f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(new SoundStyle(EAU.SoundPath("Warhorn")), Item.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 16);
            recipe.AddIngredient(ItemID.Fireblossom, 2);
            recipe.AddIngredient(ModContent.ItemType<ImpEar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<MagmaCrystal>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}