using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Storyteller
{
    public class MasterSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 60;
            Item.knockBack = 2;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<MasterSwordWave>();
            Item.shootSpeed = 16f;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (modPlayer.masterSwordCharge > 0)
            {
                if (player.altFunctionUse != 2)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 135);
                        Main.dust[dust].noGravity = true;
                    }
                }
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (player.altFunctionUse == 2)
            {
                if (modPlayer.masterSwordCharge >= 50)
                {
                    return false;

                }
                else
                {
                    return true;
                }
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (player.altFunctionUse == 2)
            {
                Item.noMelee = true;
                Item.channel = true;
                Item.useTime = 4;
                Item.useAnimation = 20;
                Item.UseSound = SoundID.Item13;
                Item.useStyle = 4;

                modPlayer.masterSwordCharge++;


                if (modPlayer.masterSwordCharge <= 5)
                {
                    modPlayer.masterSwordCountdown = 900;
                }

                Color color = Color.LightCyan;
                if (modPlayer.masterSwordCharge == 50)
                {
                    color = Color.Magenta;
                }
                if (modPlayer.masterSwordCharge % 10 == 0) // if it is divisible by 10
                {
                    CombatText.NewText(player.getRect(), color, modPlayer.masterSwordCharge, true, false);
                }

                for (int l = 0; l < 30; l++)
                {
                    int dust = Dust.NewDust(new Vector2(player.Center.X - 100, player.Center.Y), 200, 8, 135, 0f, 0f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                }
            }
            else
            {
                Item.noMelee = false;
                Item.channel = false;
                Item.useTime = 16;
                Item.useAnimation = 16;
                Item.UseSound = SoundID.Item1;
                Item.useStyle = 1;

                if (modPlayer.masterSwordCharge > 30)
                {
                    Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            damage *= 1 + modPlayer.masterSwordCharge / 50;
        }
        public override void HoldItem(Player player)
        {
            if (player.altFunctionUse == 2)  player.itemRotation -= 0.785f  * player.direction;
        }
    }
}
