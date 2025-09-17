using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class LifeforceBurner : ModItem
    {
        public bool enabled = false;
        public int lifeRegenAmount = 0;
        public int lifeRegenTimer = 0;

        public bool leftArrow = false;
        public int leftArrowCooldown = 0;
        public bool rightArrow = false;
        public int rightArrowCooldown = 0;

        protected override bool CloneNewInstances {  get { return true; }}
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 1;
            Item.maxStack = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine lifeRegen = new TooltipLine(Mod, "Elements Awoken:Tooltip", string.Format(ModContent.GetInstance<EALocalization>().LifeforceBurner, lifeRegenAmount));
            tooltips.Insert(1, lifeRegen);

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
                if (lifeRegenAmount > 0)
                {
                    player.lifeRegenCount = 0;

                    lifeRegenTimer += lifeRegenAmount;

                    if (lifeRegenTimer >= 120)
                    {
                        lifeRegenTimer -= 120;
                        modPlayer.energy++;
                        player.statLife -= 2;
                        if (player.statLife <= 0)
                        {
                            player.statLife = 0;
                            player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromKey(player.name + " " + ModContent.GetInstance<EALocalization>().LifeforceBurner1)), 1.0, 0, false);
                        }
                    }
                }
            }
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
                        if (leftArrowCooldown <= 0 && lifeRegenAmount > 0)
                        {
                            lifeRegenAmount--;
                            leftArrowCooldown = 3;
                            SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                        }
                    }
                    if (key == Keys.Right)
                    {
                        if (rightArrowCooldown <= 0 && lifeRegenAmount < 50)
                        {
                            lifeRegenAmount++;
                            rightArrowCooldown = 3;
                            SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                        }
                    }
                }
            }
            if (!player.active || player.dead)
            {
                lifeRegenAmount = 0; // reset when dead in case player cant turn it off in time
            }
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            if (enabled)enabled = false;
            else enabled = true;
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeCrystal, 4);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 6);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
