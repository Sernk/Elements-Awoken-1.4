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
    public class SolarGeneratorI : ModItem
    {
        public int productionAmount = 1;
        public int producePowerCooldown = 0;
        public int producePowerCooldownMax = 60;

        protected override bool CloneNewInstances { get { return true; } }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 3;
            Item.maxStack = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            float powerPerSec = (float)producePowerCooldownMax / 60f;
            string ppsString = powerPerSec.ToString("n1");
            string G = ModContent.GetInstance<EALocalization>().Generator;
            string GL = String.Format(G, productionAmount, ppsString);
            TooltipLine powerOutput = new(Mod, "Elements Awoken:Tooltip", GL);
            if (Main.dayTime)
            {
                powerOutput = new TooltipLine(Mod, "Elements Awoken:Tooltip", GL);
            }
            else if (!Main.dayTime || modPlayer.energy >= modPlayer.maxEnergy)
            {
                powerOutput = new TooltipLine(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().Generator1);
            }
            tooltips.Insert(1, powerOutput);
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (modPlayer.energy < modPlayer.maxEnergy && Main.dayTime)
            {
                producePowerCooldownMax = (int)MathHelper.Lerp(180, 450, MathHelper.Distance((float)Main.time, 27000) / 27000);
                productionAmount = (int)Math.Round(MathHelper.Lerp(2, 1, MathHelper.Distance((float)Main.time, 27000) / 27000));

                producePowerCooldown--;
                if (producePowerCooldown <= 0)
                {
                    producePowerCooldown = producePowerCooldownMax;
                    modPlayer.energy += productionAmount;
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.HellstoneBar, 4);
            recipe.AddIngredient(ItemID.Glass, 18);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 8);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}