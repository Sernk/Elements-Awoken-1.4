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
    public class GelticBurner : ModItem
    {
        public bool enabled = false;
        public int fuel = 0;
        public int producePowerCooldown = 0;

        protected override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 2;
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
                tip = new(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().GravityCore1);
                tip.OverrideColor = Color.Green;
            }
            else
            {
                tip = new(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().GravityCore2);
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
                    Item other = Main.LocalPlayer.inventory[i];
                    if (other.type == ItemID.Gel)
                    {
                        if (fuel <= 0)
                        {
                            fuel = 900;
                            other.stack--;
                        }
                    }
                    if (other.type == ItemID.PinkGel)
                    {
                        if (fuel <= 0)
                        {
                            fuel = 1800;
                            other.stack--;
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
                    producePowerCooldown = 30;
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
            recipe.AddIngredient(ItemID.Gel, 12);
            recipe.AddIngredient(ItemID.PinkGel, 6);
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 4);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}