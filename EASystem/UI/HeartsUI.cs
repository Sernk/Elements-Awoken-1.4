using ElementsAwoken.EASystem.Global;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ElementsAwoken.EASystem.UI
{
    public class HeartsUI : ModResourceOverlay
    {
        private Dictionary<string, Asset<Texture2D>> vanillaAssetCache = new();
        public string baseFolder = "ElementsAwoken/Extra/";

        public string LifeTexturePath()
        {
            string folder = $"{baseFolder}HP";
            HeartsPlayers modPlayer = Main.LocalPlayer.GetModPlayer<HeartsPlayers>();
            var modPlayer2 = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            if (modPlayer.chaosHeartLife > 0)
            {
                return folder + "Heart4";
            }
            if (modPlayer.voidHeartLife > 0)
            {
                if (modPlayer2.voidCompressor)
                {
                    return folder + "Heart3Alt";
                }
                return folder + "Heart3";
            }
            if (modPlayer.shieldLife > 0)
            {
                return folder + "ShieldHeart";
            }
            return string.Empty;
        }

        public override void PostDrawResource(ResourceOverlayDrawContext context)
        {
            Asset<Texture2D> asset = context.texture;
            string fancyFolder = "Images/UI/PlayerResourceSets/FancyClassic/";
            string barsFolder = "Images/UI/PlayerResourceSets/HorizontalBars/";

            if (LifeTexturePath() == string.Empty)
                return;

            if (asset == TextureAssets.Heart || asset == TextureAssets.Heart2 || CompareAssets(asset, fancyFolder + "Heart_Fill") || CompareAssets(asset, fancyFolder + "Heart_Fill_B"))
            {
                context.texture = ModContent.Request<Texture2D>(LifeTexturePath() + "Heart");
                context.Draw();
            }

            else if (CompareAssets(asset, barsFolder + "HP_Fill") || CompareAssets(asset, barsFolder + "HP_Fill_Honey"))
            {
                context.texture = ModContent.Request<Texture2D>(LifeTexturePath() + "Bar");
                context.Draw();
            }

        }

        public override void PostDrawResourceDisplay(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife, Color textColor, bool drawText)
        {
            HeartsPlayers modPlayer = Main.LocalPlayer.GetModPlayer<HeartsPlayers>();
            var player = Main.LocalPlayer;

            if (!drawingLife || player.ghost)
                return;

            int totalHearts = snapshot.AmountOfLifeHearts;
            float lifePerHeart = snapshot.LifePerSegment;
            int currentLife = snapshot.Life;
            int maxLife = snapshot.LifeMax;

            Vector2 basePosition = displaySet.NameKey switch
            {
                "Default" => new Vector2(Main.screenWidth - 289, 43),
                "New" => new Vector2(Main.screenWidth - 281, 30),
                "NewWithText" => new Vector2(Main.screenWidth - 281, 36),
                "HorizontalBarsWithText" => new Vector2(Main.screenWidth - 60, 28),
                "HorizontalBarsWithFullText" => new Vector2(Main.screenWidth - 60, 26),
                "HorizontalBars" => new Vector2(Main.screenWidth - 60, 24),
                _ => Vector2.Zero
            };

            // === Отображение Sanity ===
            var awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (!MyWorld.awakenedMode || player.ghost || awakenedPlayer.sanityMax <= 0)
            {
                return;
            }

            Texture2D sanityTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/InsanityUIIcon").Value;
            Texture2D arrowTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/SanityArrow").Value;
            DynamicSpriteFont font = FontAssets.MouseText.Value;

            float sanityPerEye = 30f;
            int maxEyes = (int)Math.Ceiling(awakenedPlayer.sanityMax / sanityPerEye);
            int currentSanity = awakenedPlayer.sanity;

            Vector2 sanityBasePos = basePosition - new Vector2(160, 12);
            Vector2 eyeSize = new Vector2(sanityTex.Width, sanityTex.Height);
            float eyeSpacing = 26f;

            for (int i = 0; i < maxEyes; i++)
            {
                bool isPartial = currentSanity > i * sanityPerEye && currentSanity < (i + 1) * sanityPerEye;
                float fill = Math.Clamp((currentSanity - i * sanityPerEye) / sanityPerEye, 0f, 1f);

                float scale = isPartial ? fill / 4f + 0.75f : 1f;
                if (isPartial)
                    scale += Main.cursorScale - 1f;

                int intensity = (int)(30f + 225f * fill);
                int alpha = (int)(intensity * 0.9f);
                intensity = Math.Clamp(intensity, 30, 255);

                int xOffset = (i % 5) * (int)eyeSpacing;
                int yOffset = (i / 5) * ((int)eyeSpacing + 4);
                Vector2 pos = sanityBasePos + new Vector2(xOffset, yOffset);

                // Санити глитчи
                if (awakenedPlayer.sanity < awakenedPlayer.sanityMax * 0.4f && awakenedPlayer.sanityGlitchFrame != 0)
                {
                    int amount = awakenedPlayer.sanity < awakenedPlayer.sanityMax * 0.1f ? 4 : awakenedPlayer.sanity < awakenedPlayer.sanityMax * 0.2f ? 3 : awakenedPlayer.sanity < awakenedPlayer.sanityMax * 0.3f ? 2 : 1;         
                    pos += new Vector2(Main.rand.Next(-amount, amount), Main.rand.Next(-amount, amount));
                }

                Vector2 origin = eyeSize / 2f;
                Main.spriteBatch.Draw(sanityTex, pos + origin, null, new Color(intensity, intensity, intensity, alpha), 0f, origin, scale, SpriteEffects.None, 0f);                   
            }
            var EALocalization = ModContent.GetInstance<EALocalization>();
            // === Текстовое значение ===
            string sanityText = $"{EALocalization.Sanity}: {awakenedPlayer.sanity}/{awakenedPlayer.sanityMax}";
            Vector2 textSize = font.MeasureString(sanityText);
            Vector2 textPos = sanityBasePos + new Vector2(maxEyes * eyeSpacing / 2 - textSize.X / 2f, -20);
            Main.spriteBatch.DrawString(font, sanityText, textPos, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor));

            // === Tooltip при наведении ===
            Rectangle sanityBounds = new((int)sanityBasePos.X, (int)sanityBasePos.Y, (int)(eyeSpacing * 5), (int)(eyeSize.Y * 2));
            if (!Main.mouseText && sanityBounds.Contains(Main.MouseScreen.ToPoint()))
            {
                string mouseText = $"{awakenedPlayer.sanity}/{awakenedPlayer.sanityMax}";
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, mouseText, new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One);
            }

            // === Индикатор восстановления ===
            if (awakenedPlayer.sanityRegen != 0)
            {
                int arrowFrame = awakenedPlayer.sanityArrowFrame;
                int arrowHeight = 26;
                Rectangle source = new Rectangle(0, arrowHeight * arrowFrame, arrowTex.Width, arrowHeight);
                SpriteEffects flip = awakenedPlayer.sanityRegen < 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;

                Vector2 arrowPos = textPos + new Vector2(textSize.X + 8, arrowHeight / 2);
                Main.spriteBatch.Draw(arrowTex, arrowPos, source, Color.White, 0f, new Vector2(arrowTex.Width / 2, arrowHeight / 2), 1f, flip, 0f);
            }
            // === Отображение Energy ===
            var energyPlayer = player.GetModPlayer<PlayerEnergy>();
            if (player.ghost || energyPlayer.maxEnergy <= 0)
                return;

            Texture2D energyTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/EnergyUIIcon").Value;

            int totalOrbs = 10;
            float energyPerOrb = energyPlayer.maxEnergy / (float)totalOrbs;
            Vector2 energyBasePos = basePosition - new Vector2(400, 12); // смещаем ещё левее от Sanity
            Vector2 orbSize = new Vector2(energyTex.Width, energyTex.Height);
            float orbSpacing = 26f;

            for (int i = 0; i < totalOrbs; i++)
            {
                bool isPartial = energyPlayer.energy > i * energyPerOrb && energyPlayer.energy < (i + 1) * energyPerOrb;
                float fill = Math.Clamp((energyPlayer.energy - i * energyPerOrb) / energyPerOrb, 0f, 1f);

                float scale = isPartial ? fill / 4f + 0.75f : 1f;
                if (isPartial)
                    scale += Main.cursorScale - 1f;

                int intensity = (int)(30f + 225f * fill);
                intensity = Math.Clamp(intensity, 30, 255);
                int alpha = (int)(intensity * 0.9f);

                int xOffset = (i % 5) * (int)orbSpacing;
                int yOffset = (i / 5) * ((int)orbSpacing + 4);
                Vector2 pos = energyBasePos + new Vector2(xOffset, yOffset);
                Vector2 origin = orbSize / 2f;

                Main.spriteBatch.Draw(
                    energyTex,
                    pos + origin,
                    null,
                    new Color(intensity, intensity, intensity, alpha),
                    0f,
                    origin,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }

            // === Текстовое значение Energy ===
            string energyText = $"{EALocalization.Energy}: {energyPlayer.energy}/{energyPlayer.maxEnergy}";
            Vector2 energyTextSize = font.MeasureString(energyText);
            Vector2 energyTextPos = energyBasePos + new Vector2(totalOrbs * orbSpacing / 2 - energyTextSize.X / 2f, -20);

            Main.spriteBatch.DrawString(font, energyText, energyTextPos, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor));

            // === Tooltip при наведении ===
            Rectangle energyBounds = new((int)energyBasePos.X, (int)energyBasePos.Y, (int)(orbSpacing * 5), (int)(orbSize.Y * 2));
            if (!Main.mouseText && energyBounds.Contains(Main.MouseScreen.ToPoint()))
            {
                string tooltip = $"{energyPlayer.energy}/{energyPlayer.maxEnergy}";
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, tooltip, new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
            }

        }

        private bool CompareAssets(Asset<Texture2D> currentAsset, string compareAssetPath)
        {
            if (!vanillaAssetCache.TryGetValue(compareAssetPath, out var asset))
                asset = vanillaAssetCache[compareAssetPath] = Main.Assets.Request<Texture2D>(compareAssetPath);

            return currentAsset == asset;
        }
    }
}
