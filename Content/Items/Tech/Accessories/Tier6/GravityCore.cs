using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.Content.Items.Tech.Accessories.Tier6
{
    public class GravityCore : ModItem
    {
        public bool enabled = false;
        public int gravityPercent = 100;

        public bool leftArrow = false;
        public int leftArrowCooldown = 0;
        public bool rightArrow = false;
        public int rightArrowCooldown = 0;

        public int energyCD = 0;
        protected override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 7;    
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine lifeRegen = new(Mod, "Elements Awoken:Tooltip", string.Format(ModContent.GetInstance<EALocalization>().GravityCore, gravityPercent)); 
            tooltips.Insert(1, lifeRegen);

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
            TooltipLine warning = new(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().GravityCore3);
            warning.OverrideColor = Color.Red;
            tooltips.Add(warning);
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (enabled)
            {
                energyCD--;
                leftArrowCooldown--;
                rightArrowCooldown--;
                if (Main.HoverItem.type == Item.type)
                {
                    Keys[] pressedKeys = Main.keyState.GetPressedKeys();

                    for (int j = 0; j < pressedKeys.Length; j++)
                    {
                        Keys key = pressedKeys[j];
                        if (key == Keys.Left)
                        {
                            if (leftArrowCooldown <= 0 && gravityPercent > -100)
                            {
                                gravityPercent--;
                                leftArrowCooldown = 3;
                                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                            }
                        }
                        if (key == Keys.Right)
                        {
                            if (rightArrowCooldown <= 0 && gravityPercent < 300)
                            {
                                gravityPercent++;
                                rightArrowCooldown = 3;
                                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                            }
                        }
                        if (key == Keys.G)
                        {
                            gravityPercent = 100;
                        }
                    }
                }
                if (energyCD <= 0)
                {
                    modPlayer.energy--;
                    energyCD = 60;
                }
                if (modPlayer.energy > 0)
                {
                    player.gravity = 0.4f * (gravityPercent / 100);
                }
            }
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            enabled = !enabled;
            Item.stack++;
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(enabled);
            writer.Write(gravityPercent);
        }
        public override void NetReceive(BinaryReader reader)
        {
            enabled = reader.ReadBoolean();
            gravityPercent = reader.ReadInt32();
        }
        public override void SaveData(TagCompound tag)
        {
            tag["enabled"] = enabled;
            tag["gravity"] = gravityPercent;
        }
        public override void LoadData(TagCompound tag)
        {
            enabled = tag.GetBool("enabled");
            gravityPercent = tag.GetInt("gravity");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofFlight, 18);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Microcontroller>(), 2);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}