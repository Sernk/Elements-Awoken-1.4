using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class GeothermalGenerator : ModItem
    {
        public int producePowerCooldown = 0;
        public int producePowerCooldownMax = 60;
        public int productionAmount = 1;

        public Tile closest;
        public Vector2 closestPos = new Vector2();

        protected override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 4;
            Item.maxStack = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            float powerPerSec = (float)producePowerCooldownMax / 60f;
            string ppsString = powerPerSec.ToString("n1");
            string G = ModContent.GetInstance<EALocalization>().Generator;
            TooltipLine powerOutput = new TooltipLine(Mod, "Elements Awoken:Tooltip", String.Format(G, productionAmount, ppsString));
            if (Vector2.Distance(closestPos, player.Center) < 160 && (closest.LiquidType == LiquidID.Lava))
            {
                powerOutput = new TooltipLine(Mod, "Elements Awoken:Tooltip", String.Format(G, productionAmount, ppsString));
            }
            else
            {
                powerOutput = new TooltipLine(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().Generator1);
            }
            tooltips.Insert(1, powerOutput);
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            producePowerCooldown--;

            int distance = 10 * 16;
            Point topLeft = ((player.position - new Vector2(distance, distance)) / 16).ToPoint();
            Point bottomRight = ((player.BottomRight + new Vector2(distance, distance)) / 16).ToPoint();

            for (int i = topLeft.X; i <= bottomRight.X; i++)
            {
                for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);
                    if ((t.LiquidType == LiquidID.Lava))
                    {
                        Vector2 tileCenter = new Vector2(i * 16, j * 16);
                        if (closest != null)
                        {
                            if (Vector2.Distance(tileCenter, player.Center) < Vector2.Distance(closestPos, player.Center))
                            {
                                closest = t;
                                closestPos = new Vector2(i * 16, j * 16);
                            }
                        }
                        else
                        {
                            closest = t;
                            closestPos = new Vector2(i * 16, j * 16);
                        }

                    }
                }
            }
            if (Vector2.Distance(closestPos, player.Center) < 160 && (closest.LiquidType == LiquidID.Lava))
            {
                producePowerCooldownMax = (int)MathHelper.Lerp(15, 120, Vector2.Distance(closestPos, player.Center) / 160);
                productionAmount = (int)Math.Round(MathHelper.Lerp(2, 1, Vector2.Distance(closestPos, player.Center) / 160));

                if (producePowerCooldown <= 0)
                {
                    modPlayer.energy += productionAmount;
                    producePowerCooldown = producePowerCooldownMax;
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ModContent.ItemType<MagmaCrystal>(), 4);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 4);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}