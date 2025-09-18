using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.Projectiles;

namespace ElementsAwoken.Content.Items.Tech.Tools
{
    public class GravityGun : ModItem
    {
        public int energyConsume = 0;

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.useAnimation = 2;
            Item.useTime = 2;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Blank>();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            TooltipLine tip = new TooltipLine(Mod, "Elements Awoken:Energy", EALocalization.ItemEnergy + " " + 3 + " " + EALocalization.ItemEnergy1);
            tooltips.Add(tip);
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gravity Gun MKI");
            // Tooltip.SetDefault("Able to pick up items");
        }
        public override void HoldItem(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            Vector2 mouse = Main.MouseWorld;
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item otherItem = Main.item[i];

                bool canSee = Collision.CanHit(player.Center, 2, 2, otherItem.Center, 2, 2);
                Rectangle grabBox = new Rectangle((int)otherItem.TopLeft.X, (int)otherItem.TopLeft.Y, otherItem.width, otherItem.height);
                if (otherItem.width < 30)
                {
                    grabBox = new Rectangle((int)(otherItem.Center.X - otherItem.width * 3), (int)(otherItem.Center.Y - otherItem.height * 6), otherItem.width * 6, otherItem.height * 6); // making a larger hitbox (3x the size)
                }
                else
                {
                    grabBox = new Rectangle((int)(otherItem.Center.X - otherItem.width * 1.5f), (int)(otherItem.Center.Y - otherItem.height * 1.5f), (int)(otherItem.width * 3f), (int)(otherItem.height * 3f)); // making a larger hitbox (3x the size)
                }
                bool mouseOn = grabBox.Contains((int)mouse.X, (int)mouse.Y);
                if (mouseOn && canSee)
                {
                    Dust.NewDust(otherItem.position, otherItem.width, otherItem.height, 220);

                    if (Main.mouseLeft && modPlayer.energy > 3)
                    {
                        energyConsume--;
                        if (energyConsume <= 0)
                        {
                            modPlayer.energy -= 3;
                            energyConsume = 60;
                        }
                        Vector2 difference = player.Center - otherItem.Center;
                        for (int k = 0; k < 20; k++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(otherItem.Center + difference * Main.rand.NextFloat(), 0, 0, 61)];
                            dust.velocity = Vector2.Zero;
                            dust.noGravity = true;
                        }
                        otherItem.Center = mouse;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofFlight, 16);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 10);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 20);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}