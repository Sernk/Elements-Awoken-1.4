using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class BiofuelBurner : ModItem
    {
        public bool enabled = false;
        public int fuel = 0;
        public int producePowerCooldown = 0;
        public int producePowerCooldownMax = 60;
        public int powerLevel = 0;
        public string fuelItemType = "";
        protected override bool CloneNewInstances { get { return true; } }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 6;
            Item.maxStack = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (enabled)
            {
                if (powerLevel != 0)
                {
                    float powerPerSec = (float)producePowerCooldownMax / 60f;
                    string ppsString = powerPerSec.ToString("n1");
                    TooltipLine fuelQuality = new TooltipLine(Mod, "Elements Awoken:Tooltip", string.Format(ModContent.GetInstance<EALocalization>().BiofuelBurner, powerLevel, ppsString));
                    tooltips.Insert(1, fuelQuality);
                }
                TooltipLine fuelType = new TooltipLine(Mod, "Elements Awoken:Tooltip", string.Format(ModContent.GetInstance<EALocalization>().BiofuelBurner1, fuelItemType));
                tooltips.Insert(1, fuelType);

                TooltipLine fuelRemaining = new TooltipLine(Mod, "Elements Awoken:Tooltip", string.Format(ModContent.GetInstance<EALocalization>().BiofuelBurner2, fuel));
                tooltips.Insert(1, fuelRemaining);
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
            if (enabled && modPlayer.energy < modPlayer.maxEnergy)
            {
                for (int i = 0; i < 50; i++)
                {
                    Item plant = Main.LocalPlayer.inventory[i];
                    if (fuel <= 0)
                    {
                        // flowers
                        if (plant.type == ItemID.Blinkroot) ConsumePlant(plant, 900, plant.Name, 1, 60);
                        if (plant.type == ItemID.Daybloom) ConsumePlant(plant, 600, plant.Name, 1, 60);
                        if (plant.type == ItemID.Deathweed) ConsumePlant(plant, 900, plant.Name, 1, 60);
                        if (plant.type == ItemID.Fireblossom) ConsumePlant(plant, 600, plant.Name, 2, 60);
                        if (plant.type == ItemID.Moonglow) ConsumePlant(plant, 1500, plant.Name, 1, 120);
                        if (plant.type == ItemID.Shiverthorn) ConsumePlant(plant, 900, plant.Name, 1, 60);
                        if (plant.type == ItemID.Waterleaf) ConsumePlant(plant, 900, plant.Name, 1, 60);
                        if (plant.type == ItemID.Mushroom) ConsumePlant(plant, 900, plant.Name, 1, 60);
                        if (plant.type == ItemID.GlowingMushroom) ConsumePlant(plant, 200, plant.Name, 1, 90);
                        if (plant.type == ItemID.VileMushroom) ConsumePlant(plant, 1200, plant.Name, 1, 60);
                        if (plant.type == ItemID.ViciousMushroom) ConsumePlant(plant, 1200, plant.Name, 1, 60);
                        if (plant.type == ItemID.Cactus) ConsumePlant(plant, 200, plant.Name, 1, 90);
                    }
                }
            }
            if (fuel <= 0)
            {
                fuelItemType = ModContent.GetInstance<EALocalization>().BiofuelBurner3;
                powerLevel = 0;
            }
            if (fuel > 0)
            {
                fuel--;
            }
            producePowerCooldown--;
            if (fuel > 0 && producePowerCooldown <= 0)
            {
                producePowerCooldown = producePowerCooldownMax;
                modPlayer.energy += 1;
            }
        }   
        private void ConsumePlant(Item plant, int fuelAmount, string fuelName, int quality, int powerCooldown)
        {
            fuel = fuelAmount;
            fuelItemType = fuelName;
            powerLevel = quality;
            producePowerCooldownMax = powerCooldown;
            plant.stack--;
        }
        public override bool CanRightClick() => true;
        public override void RightClick(Player player)
        {
            if (enabled) enabled = false;
            else enabled = true;
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddIngredient(ModContent.ItemType<MysticLeaf>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 3);
            recipe.AddIngredient(ModContent.GetInstance<Transistor>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}