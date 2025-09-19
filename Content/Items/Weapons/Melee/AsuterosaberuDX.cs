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
    public class AsuterosaberuDX : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 56;
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
            Item.shoot = ModContent.ProjectileType<AstralTear>();
            Item.shootSpeed = 12f;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 63, 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
            Main.dust[dust].scale *= 2f;
            Main.dust[dust].noGravity = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) => false;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.FindBuffIndex(ModContent.BuffType<AstralTearCooldown>()) == -1)
                {
                    Item.useTime = 25;
                    Item.useAnimation = 25;
                    player.AddBuff(ModContent.BuffType<AstralTearCooldown>(), 240);

                    if (player.direction == 1)
                    {
                        Projectile.NewProjectile(EAU.Play(player), player.Center.X + 80, player.Center.Y - 18, 12f, 4f, ModContent.ProjectileType<AstralTear>(), 120, 5f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item91, player.position);
                    }
                    if (player.direction == -1)
                    {
                        Projectile.NewProjectile(EAU.Play(player), player.Center.X - 80, player.Center.Y - 18, -12f, 4f, ModContent.ProjectileType<AstralTear>(), 120, 5f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item91, player.position);
                    }
                }
                else return false;
            }
            else
            {
                Item.useTime = 15;
                Item.useAnimation = 15;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofSight, 8);
            recipe.AddIngredient(ModContent.ItemType<Infamy>(), 1);
            recipe.AddIngredient(ItemID.RainbowBrick, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}