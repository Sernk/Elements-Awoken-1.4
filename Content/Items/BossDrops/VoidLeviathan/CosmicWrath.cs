using ElementsAwoken.Content.Projectiles.Thrown;
using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class CosmicWrath : ModItem
    {
        public int killallDelay = 0;
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.damage = 240;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.consumable = false;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.reuseDelay = 6;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.shoot = ModContent.ProjectileType<CosmicWrathP>();
            Item.shootSpeed = 24f;
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
                if (killallDelay > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.type == ModContent.ProjectileType<CosmicWrathP>())
                    {
                        proj.Kill();
                    }
                }
                killallDelay = 20;
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void HoldItem(Player player)
        {
            killallDelay--;
        }
    }
}