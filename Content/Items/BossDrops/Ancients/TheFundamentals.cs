using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Ancients
{
    public class TheFundamentals : ModItem
    {
        public bool supercharged = false;
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 620;
            Item.knockBack = 18;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 75, 0, 0);
            Item.rare = ModContent.RarityType<Rarity14>();
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<FundementalStrike>();
            Item.shootSpeed = 20f;
        }
        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.noDamageCounter++;
            if (modPlayer.noDamageCounter == 600)
            {
                int numDusts = 50;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (new Vector2((float)player.width / 2f, (float)player.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                    Vector2 velocity = position - player.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, 63, velocity.X * 2f, velocity.Y * 2f, 100, default, 1.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                }
            }
            if (modPlayer.noDamageCounter > 600)
            {
                if (Main.rand.Next(6) == 0)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 63)];
                    dust.position -= player.velocity / 6f;
                    dust.noGravity = true;
                    dust.scale = 1.5f;
                    dust.velocity *= 1.8f;
                }
            }
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.noDamageCounter > 600) damage *= 8;
            else damage *= 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(6));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}