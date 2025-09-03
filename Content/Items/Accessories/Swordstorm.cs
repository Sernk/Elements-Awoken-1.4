using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EARecipeSystem;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class Swordstorm : ModItem
    {
        public float shootTimer = 60f;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = 10000;
            Item.rare = 1;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (shootTimer > 0f)
            {
                shootTimer -= 1f;
            }
            bool hardMode = Main.hardMode;
            bool expertMode = Main.expertMode;
            if (player.immune && shootTimer == 0f)
            {   
                SoundEngine.PlaySound(SoundID.Item8, player.position);
                float spread = 45f * 0.0174f;
                double startAngle = Math.Atan2(player.velocity.X, player.velocity.Y) - spread / 2;
                double deltaAngle = spread / 8f;
                double offsetAngle;
                int hardMult = hardMode ? 30 : 0;
                int expertMult = hardMode ? 10 : 0;
                int newDamage = 15 + expertMult + hardMult;
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var s = player.GetSource_FromThis();
                        offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                        Projectile.NewProjectile(s, player.Center.X, player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<Sword>(), newDamage, 1.25f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(s, player.Center.X, player.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<Sword>(), newDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    }
                }
                shootTimer = 60f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddRecipeGroup(EARecipeGroups.SilverSword);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}