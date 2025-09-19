using ElementsAwoken.Content.Buffs.Cooldowns;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class SolusKatana : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 62;
            Item.DamageType = DamageClass.Melee;
            Item.width = 70;
            Item.height = 70;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item15;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SolusKunai>();
            Item.shootSpeed = 12f;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.FindBuffIndex(ModContent.BuffType<SolusKunaiCooldown>()) == -1)
                {
                    Item.useTime = 25;
                    Item.useAnimation = 25;
                    player.AddBuff(ModContent.BuffType<SolusKunaiCooldown>(), 120);

                    if (player.direction == 1)
                    {
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 12f, 4f, ModContent.ProjectileType<SolusKunai>(), 120, 5f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item91, player.position);
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 11f, 6f, ModContent.ProjectileType<SolusKunai>(), 120, 5f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item91, player.position);
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 10f, 8f, ModContent.ProjectileType<SolusKunai>(), 120, 5f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item91, player.position);
                    }
                    if (player.direction == -1)
                    {
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, -12f, 4f, ModContent.ProjectileType<SolusKunai>(), 120, 5f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item91, player.position);
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, -11f, 6f, ModContent.ProjectileType<SolusKunai>(), 120, 5f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item91, player.position);
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, -10f, 8f, ModContent.ProjectileType<SolusKunai>(), 120, 5f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item91, player.position);
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Item.useTime = 15;
                Item.useAnimation = 15;
            }
            return base.CanUseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofFright, 8);
            recipe.AddIngredient(ItemID.Katana, 1);
            recipe.AddIngredient(ItemID.LivingFireBlock, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
