using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic
{
    public class SansHarp : ModItem
    {
        private readonly float[] pitches = [0.0f, 0.0f, 0.8f, 0.4f, 0.3f, 0.2f, 0.1f, 0.0f, 0.1f, 0.2f];
        private readonly int[] useTimes = [8, 8, 16, 18, 14, 14, 12, 8, 8, 16];

        protected override bool CloneNewInstances => true;
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 130;
            Item.mana = 9;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.holdStyle = 3;
            Item.useStyle = 5;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 8;
            Item.shoot = ModContent.ProjectileType<SansNote>();
            Item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            float pitch = pitches[modPlayer.sansNote];
            SoundEngine.PlaySound(SoundID.Item26.WithPitchOffset(pitch), player.position);
            modPlayer.sansUseCD= useTimes[modPlayer.sansNote];
            Projectile proj = Main.projectile[Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f)];
            modPlayer.sansNote++;
            if (modPlayer.sansNote >= 10) modPlayer.sansNote = 0;
                return false;
        }
        public override void HoldItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (player.whoAmI == Main.myPlayer) modPlayer.sansUseCD--;
        }
        public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (modPlayer.sansUseCD > 0) return false;
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicalHarp, 1);
            recipe.AddIngredient(ItemID.Bone, 32);
            recipe.AddIngredient(ModContent.ItemType<RoyalScale>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}