using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class BurnerGenerator : ModItem
    {
        public bool enabled = false;
        public int fuel = 0;
        public int producePowerCooldown = 0;

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
                    Item wood = Main.LocalPlayer.inventory[i];
                    if (wood.type == ItemID.Wood || wood.type == ItemID.RichMahogany || wood.type == ItemID.Ebonwood || wood.type == ItemID.Shadewood || wood.type == ItemID.Pearlwood || wood.type == ItemID.BorealWood || wood.type == ItemID.PalmWood || wood.type == ItemID.DynastyWood || wood.type == ItemID.SpookyWood || wood.type == ItemID.AshWood) 
                    {
                        if (fuel <= 0)
                        {
                            fuel = 900;
                            wood.stack--;
                        }
                    }
                }
                if (fuel > 0)
                {
                    fuel--;
                }
                producePowerCooldown--;
                if (fuel > 0 && producePowerCooldown <= 0)
                {
                    producePowerCooldown = 90;
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
            if (enabled) enabled = false;
            else enabled = true;
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 24);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddRecipeGroup(EARecipeGroups.SilverBar, 3);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}