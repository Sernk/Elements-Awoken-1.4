using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic
{
    public class Warhorn : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 18;
            Item.damage = 35;
            Item.mana = 20;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = 5;
            Item.UseSound = new SoundStyle(EAU.SoundPath("Warhorn"));
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<WarhornP>();
            Item.shootSpeed = 24f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, player.Center) < 100)
                {
                    Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity -= toTarget * 10f;
                }
            }
            Projectile.NewProjectile(source, position.X, position.Y, 0f, 0f, ModContent.ProjectileType<WarhornP>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -10);
        }
    }
}