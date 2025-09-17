using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class WindGenerator : ModItem
    {
        public bool enabled = true;
        public int producePowerCooldown = 0;
        public int producePowerCooldownMax = 60;

        protected override bool CloneNewInstances { get { return true; } }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 1;
            Item.maxStack = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (enabled)
            {
                int windMPH = (int)(Main.windSpeedCurrent * 100f);
                string windspeed = "";
                if (windMPH < 0)
                {
                    windspeed += Language.GetTextValue("GameUI.WestWind", Math.Abs(windMPH));
                }
                else if (windMPH > 0)
                {
                    windspeed += Language.GetTextValue("GameUI.EastWind", windMPH);
                }

                TooltipLine windSpd = new TooltipLine(Mod, "Elements Awoken:Tooltip", string.Format(ModContent.GetInstance<EALocalization>().WindGenerator, windspeed));
                tooltips.Insert(1, windSpd);
                float powerPerSec = (float)producePowerCooldownMax / 60f;
                string ppsString = powerPerSec.ToString("n1");
                TooltipLine powerCooldown = new TooltipLine(Mod, "Elements Awoken:Tooltip", string.Format(ModContent.GetInstance<EALocalization>().WindGenerator, ppsString));
                tooltips.Insert(1, powerCooldown);
            }
            TooltipLine tip;
            if (enabled)
            {
                tip = new TooltipLine(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().GravityCore1);
                tip.OverrideColor = Color.Green;
            }
            else
            {
                tip = new TooltipLine(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().GravityCore2);
                tip.OverrideColor = Color.Red;
            }
            tooltips.Insert(1, tip);
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (enabled)
            {
                // maximum wind is: 50mph?
                producePowerCooldownMax = (int)MathHelper.Lerp(300, 20, (Main.windSpeedCurrent * 100) / 50); 
                producePowerCooldown--;
                if (producePowerCooldown <= 0)
                {
                    producePowerCooldown = producePowerCooldownMax;
                    modPlayer.energy += 1;
                }
            }
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            if (enabled)
            {
                enabled = false;
            }
            else
            {
                enabled = true;
            }
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.CobaltBar, 12);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 12);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 3);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}