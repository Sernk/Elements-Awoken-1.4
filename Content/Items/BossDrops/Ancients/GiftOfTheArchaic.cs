using ElementsAwoken.Content.Buffs.Cooldowns;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Ancients
{
    public class GiftOfTheArchaic : ModItem
    {
        private int boundKey = (int)Keys.X;
        private int rebindTimer = 0;
        private int reuseKeyTimer = 0;
        protected override bool CloneNewInstances => true;
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 11;
            Item.accessory = true;
            Item.expert = true;
            Item.rare = ModContent.RarityType<Awakened>();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        { 
            var EL = ModContent.GetInstance<EALocalization>();
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.Text = $"{EL.GiftOfTheArchaic} { (Keys)boundKey} {EL.GiftOfTheArchaic1}";
                }
            }
        }
        private bool ControlKey(Keys key)
        {
            if (key == Keys.Space) return true;
            else if (key == Keys.RightControl) return true;
            else if (string.Concat(key) == Main.cRight) return true;
            else if (string.Concat(key) == Main.cLeft) return true;
            else if (string.Concat(key) == Main.cDown) return true;
            else if (string.Concat(key) == Main.cUp) return true;
            else if (string.Concat(key) == Main.cThrowItem) return true;
            else if (string.Concat(key) == Main.cHeal) return true;
            else if (string.Concat(key) == Main.cMana) return true;
            else if (string.Concat(key) == Main.cBuff) return true;
            else if (string.Concat(key) == Main.cHook) return true;
            else if (string.Concat(key) == Main.cTorch) return true;
            else if (string.Concat(key) == Main.cInv) return true;
            else if (string.Concat(key) == Main.cSmart) return true;
            else if (string.Concat(key) == Main.cMount) return true;
            else if (string.Concat(key) == Main.cFavoriteKey) return true;
            else if (string.Concat(key) == Main.cMapZoomIn) return true;
            else if (string.Concat(key) == Main.cMapZoomOut) return true;
            else if (string.Concat(key) == Main.cMapAlphaUp) return true;
            else if (string.Concat(key) == Main.cMapAlphaDown) return true;
            else if (string.Concat(key) == Main.cMapFull) return true;
            else if (string.Concat(key) == Main.cMapStyle) return true;
            else return false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            rebindTimer--;
            reuseKeyTimer--;
            Keys[] pressedKeys = Main.keyState.GetPressedKeys();

            bool ctrlPressed = false;
            for (int j = 0; j < pressedKeys.Length; j++)
            {
                Keys key = pressedKeys[j];
                if (Main.HoverItem.type == Item.type && (key == Keys.LeftControl || key == Keys.RightControl)) ctrlPressed = true;
            }
            for (int j = 0; j < pressedKeys.Length; j++)
            {
                Keys key = pressedKeys[j];
                if (Main.HoverItem.type == Item.type)
                {
                    if (!ControlKey(key) && ctrlPressed && rebindTimer <= 0)
                    {
                        boundKey = (int)key;
                        rebindTimer = 20;
                    }
                }
                if (key == (Keys)boundKey && reuseKeyTimer <= 0 && rebindTimer <= 0)
                {
                    if (modPlayer.archaicProtectionTimer > 0) modPlayer.archaicProtectionTimer = 0;
                    else if (player.FindBuffIndex(ModContent.BuffType<ArchaicProtectionCD>()) == -1)
                    {
                        modPlayer.archaicProtectionTimer = 1200;
                        player.AddBuff(ModContent.BuffType<ArchaicProtectionCD>(), ModContent.GetInstance<Config>().debugMode ? 60 : 3600); // 3600
                    }
                    reuseKeyTimer = 20;
                }
            }
        }
    }
}