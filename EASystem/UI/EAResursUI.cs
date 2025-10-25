using ElementsAwoken.Content.Items.BossDrops.TheGuardian;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using static ElementsAwoken.EASystem.Loot.InMultipleConditionByMode;

namespace ElementsAwoken.EASystem.UI
{
    public class EAResursUI : ModResourceOverlay
    {
        private readonly Dictionary<string, Asset<Texture2D>> vanillaAssetCache = [];
        public string baseFolder = "ElementsAwoken/Extra/";

        public enum ResourceType { Life, Mana }
        private string GetTexturePath(ResourceType type, bool isMini)
        {
            HeartsPlayers modPlayer = Main.LocalPlayer.GetModPlayer<HeartsPlayers>();
            MyPlayer modPlayer2 = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            string folder = baseFolder;

            if (type == ResourceType.Life) folder += isMini ? "MiniHP" : "HP";
            else folder += isMini ? "MiniMP" : "MP";

            if (type == ResourceType.Life)
            {
                if (modPlayer.emptyVesselHeartLife > 0 || modPlayer.EmptyVesselVisual) return folder + "Heart4";
                if (modPlayer.chaosHeartLife > 0 || modPlayer.ChaosHeartVisual) return folder + "Heart3";
                if (modPlayer.CompressorVisual) return folder + "Heart3Alt";
                if (modPlayer.shieldLife > 0) return folder + "ShieldHeart";
            }
            else { if (modPlayer2.lunarStarsUsed > 0 || modPlayer.ManaBonus) return folder + "Mana2"; }
            return string.Empty;
        }
        public string LifeTexturePath() => GetTexturePath(ResourceType.Life, false);
        public string ManaTexturePath() => GetTexturePath(ResourceType.Mana, false);
        public string MiniLifeTexturePath() => GetTexturePath(ResourceType.Life, true);
        public string MiniManaTexturePath() => GetTexturePath(ResourceType.Mana, true);
        public override void PostDrawResource(ResourceOverlayDrawContext context)
        {
            Asset<Texture2D> asset = context.texture;
            string fancyFolder = "Images/UI/PlayerResourceSets/FancyClassic/";
            string barsFolder = "Images/UI/PlayerResourceSets/HorizontalBars/";

            if (LifeTexturePath() == string.Empty) return;

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
            if (ManaTexturePath() != string.Empty)
            {
                if (asset == TextureAssets.Mana || CompareAssets(asset, fancyFolder + "Star_Fill"))
                {
                    context.texture = ModContent.Request<Texture2D>(ManaTexturePath() + "Star");
                    context.Draw();
                }
                else if (CompareAssets(asset, barsFolder + "MP_Fill"))
                {
                    context.texture = ModContent.Request<Texture2D>(ManaTexturePath() + "Bar");
                    context.Draw();
                }
            }
            if (CompareAssets(asset, barsFolder + "HP_Panel_Right"))
            {
                Texture2D tex = ModContent.Request<Texture2D>(MiniLifeTexturePath() + "HeartMini").Value;
                Vector2 pos = context.position;
                pos += new Vector2(20.25f, 11.5f);
                Main.spriteBatch.Draw(tex, pos, Color.White);
            }
            if (CompareAssets(asset, barsFolder + "MP_Panel_Right"))
            {
                Texture2D tex = ModContent.Request<Texture2D>(MiniManaTexturePath() + "ManaMini").Value;
                Vector2 pos = context.position;
                pos += new Vector2(20.30f, 4f);
                Main.spriteBatch.Draw(tex, pos, Color.White);
            }
        }
        public override void PostDrawResourceDisplay(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife, Color textColor, bool drawText)
        {
            Player player = Main.LocalPlayer;
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();
            PlayerEnergy energyPlayer = player.GetModPlayer<PlayerEnergy>();
            EALocalization EALocalization = ModContent.GetInstance<EALocalization>();

            DynamicSpriteFont font = FontAssets.MouseText.Value;

            if (player.ghost || !drawingLife) return;

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
            if (MyWorld.awakenedMode && ModContent.GetInstance<Config>().resourceBars == false)
            {
                Texture2D sanityTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/InsanityUIIcon").Value;
                Texture2D arrowTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/SanityArrow").Value;
              
                float sanityPerEye = 30f;
                int maxEyes = (int)Math.Ceiling(awakenedPlayer.sanityMax / sanityPerEye);
                int currentSanity = awakenedPlayer.sanity;

                Vector2 sanityBasePos;
                if (displaySet.NameKey == "HorizontalBars" || displaySet.NameKey == "HorizontalBarsWithFullText" || displaySet.NameKey == "HorizontalBarsWithText") sanityBasePos = basePosition - new Vector2(377, 6);
                else sanityBasePos = basePosition - new Vector2(160, 12);
                Vector2 eyeSize = new(sanityTex.Width, sanityTex.Height);
                float eyeSpacing = 26f;

                for (int i = 0; i < maxEyes; i++)
                {
                    bool isPartial = currentSanity > i * sanityPerEye && currentSanity < (i + 1) * sanityPerEye;
                    float fill = Math.Clamp((currentSanity - i * sanityPerEye) / sanityPerEye, 0f, 1f);

                    float scale = isPartial ? fill / 4f + 0.75f : 1f;
                    if (isPartial) scale += Main.cursorScale - 1f;

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
            }
            if (MyWorld.awakenedMode && ModContent.GetInstance<Config>().resourceBars)
            {
                Texture2D backgroundTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/InsanityUI").Value;
                Texture2D barTex = awakenedPlayer.sanity >= awakenedPlayer.sanityMax * 0.4f ? ModContent.Request<Texture2D>("ElementsAwoken/Extra/InsanityBar").Value : ModContent.Request<Texture2D>("ElementsAwoken/Extra/InsanityBarDistorted").Value;
                Texture2D arrowTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/SanityArrow").Value;

                Vector2 sanityBasePos;
                if (displaySet.NameKey == "HorizontalBars" || displaySet.NameKey == "HorizontalBarsWithFullText" || displaySet.NameKey == "HorizontalBarsWithText") sanityBasePos = basePosition - new Vector2(377, 6);
                else sanityBasePos = basePosition - new Vector2(160, 12);
                   
                Vector2 origin = new(backgroundTex.Width / 2f, backgroundTex.Height / 2f);
                Vector2 drawPos = sanityBasePos + new Vector2(backgroundTex.Width / 2f, backgroundTex.Height / 2f);

                Main.spriteBatch.Draw(backgroundTex, drawPos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

                float fillPercent = Math.Clamp((float)awakenedPlayer.sanity / awakenedPlayer.sanityMax, 0f, 1f);
                int barWidth = (int)(barTex.Width * fillPercent);

                if (awakenedPlayer.sanity >= awakenedPlayer.sanityMax * 0.4f)
                {
                    Rectangle dest = new Rectangle((int)drawPos.X - barTex.Width / 2 + 11, (int)drawPos.Y, barWidth, barTex.Height);
                    Rectangle src = new Rectangle(0, 0, barWidth, barTex.Height);
                    Main.spriteBatch.Draw(barTex, dest, src, Color.White, 0f, new Vector2(0, barTex.Height / 2f), SpriteEffects.None, 0f);
                }
                else
                {
                    int barHeight = 28;
                    Rectangle src = new Rectangle(0, barHeight * awakenedPlayer.sanityGlitchFrame, barWidth, barHeight);
                    Rectangle dest = new Rectangle((int)drawPos.X - barTex.Width / 2 + 11, (int)drawPos.Y, barWidth, barHeight);
                    Main.spriteBatch.Draw(barTex, dest, src, Color.White, 0f, new Vector2(0, barHeight / 2f), SpriteEffects.None, 0f);
                }

                string sanityText = $"{EALocalization.Sanity}: {awakenedPlayer.sanity}/{awakenedPlayer.sanityMax}";
                Vector2 textSize = font.MeasureString(sanityText);
                Vector2 textPos = drawPos - new Vector2(textSize.X / 2f, backgroundTex.Height / 2f + 18f);
                Main.spriteBatch.DrawString(font, sanityText, textPos, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor));

                if (awakenedPlayer.sanityRegen != 0)
                {
                    int arrowFrame = awakenedPlayer.sanityArrowFrame;
                    int arrowHeight = 26;
                    Rectangle source = new Rectangle(0, arrowHeight * arrowFrame, arrowTex.Width, arrowHeight);
                    SpriteEffects flip = awakenedPlayer.sanityRegen < 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;

                    Vector2 arrowPos = drawPos + new Vector2(backgroundTex.Width / 2f + 12, 0f);
                    Main.spriteBatch.Draw(arrowTex, arrowPos, source, Color.White, 0f, new Vector2(arrowTex.Width / 2f, arrowHeight / 2f), 1f, flip, 0f);
                }
            }
            // === Отображение Energy ===
            if (energyPlayer.maxEnergy >= 1 && ModContent.GetInstance<Config>().resourceBars == false)
            {
                Texture2D energyTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/EnergyUIIcon").Value;

                int totalOrbs = 10;
                float energyPerOrb = energyPlayer.maxEnergy / (float)totalOrbs;
                Vector2 energyBasePos;
                Vector2 orbSize = new(energyTex.Width, energyTex.Height);
                float orbSpacing = 26f;

                if (displaySet.NameKey == "HorizontalBars" || displaySet.NameKey == "HorizontalBarsWithFullText" || displaySet.NameKey == "HorizontalBarsWithText") energyBasePos = basePosition - new Vector2(640, 7);
                else energyBasePos = basePosition - new Vector2(400, 12); // смещения левее от Sanity

                for (int i = 0; i < totalOrbs; i++)
                {
                    bool isPartial = energyPlayer.energy > i * energyPerOrb && energyPlayer.energy < (i + 1) * energyPerOrb;
                    float fill = Math.Clamp((energyPlayer.energy - i * energyPerOrb) / energyPerOrb, 0f, 1f);

                    float scale = isPartial ? fill / 4f + 0.75f : 1f;
                    if (isPartial) scale += Main.cursorScale - 1f;

                    int intensity = (int)(30f + 225f * fill);
                    intensity = Math.Clamp(intensity, 30, 255);
                    int alpha = (int)(intensity * 0.9f);

                    int xOffset = (i % 5) * (int)orbSpacing;
                    int yOffset = (i / 5) * ((int)orbSpacing + 4);
                    Vector2 pos = energyBasePos + new Vector2(xOffset, yOffset);
                    Vector2 origin = orbSize / 2f;

                    Main.spriteBatch.Draw(energyTex, pos + origin, null, new Color(intensity, intensity, intensity, alpha), 0f, origin, scale, SpriteEffects.None, 0f);
                }

                // === Текстовое значение Energy ===
                string energyText = $"{EALocalization.Energy}: {energyPlayer.energy}/{energyPlayer.maxEnergy}";
                Vector2 energyTextSize = font.MeasureString(energyText);
                int a; int b;

                if (Language.ActiveCulture.Name == "ru-RU") { a = 55; b = 20; }
                else { a = 70;  b = 20;}

                Vector2 energyTextPos = energyBasePos + new Vector2((totalOrbs * orbSpacing / 2 - energyTextSize.X / 2f) - a, - b);

                Main.spriteBatch.DrawString(font, energyText, energyTextPos, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor));

                // === Tooltip при наведении ===
                Rectangle energyBounds = new((int)energyBasePos.X, (int)energyBasePos.Y, (int)(orbSpacing * 5), (int)(orbSize.Y * 2));
                if (!Main.mouseText && energyBounds.Contains(Main.MouseScreen.ToPoint()))
                {
                    string tooltip = $"{energyPlayer.energy}/{energyPlayer.maxEnergy}";
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, tooltip, new Vector2(Main.mouseX + 17, Main.mouseY + 17), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                }
            }
            if (energyPlayer.maxEnergy >= 1 && ModContent.GetInstance<Config>().resourceBars)
            {
                Texture2D energyBack = ModContent.Request<Texture2D>("ElementsAwoken/Extra/EnergyUI").Value;
                Texture2D energyFill = ModContent.Request<Texture2D>("ElementsAwoken/Extra/EnergyBar").Value;

                float fillPercent = energyPlayer.energy / (float)energyPlayer.maxEnergy;
                fillPercent = Math.Clamp(fillPercent, 0f, 1f);

                Vector2 energyBasePos;
                if (displaySet.NameKey == "HorizontalBars" || displaySet.NameKey == "HorizontalBarsWithFullText" || displaySet.NameKey == "HorizontalBarsWithText") energyBasePos = basePosition - new Vector2(640, 7);
                else energyBasePos = basePosition - new Vector2(400, 12);

                Rectangle backRect = new Rectangle((int)energyBasePos.X, (int)energyBasePos.Y, energyBack.Width, energyBack.Height);
                Rectangle fillRect = new Rectangle(backRect.X, backRect.Y, (int)(energyFill.Width * fillPercent), energyFill.Height);

                Main.spriteBatch.Draw(energyBack, backRect, Color.White);
                Main.spriteBatch.Draw(energyFill, new Vector2(backRect.X + 24, backRect.Y), new Rectangle(0, 0, fillRect.Width, fillRect.Height), Color.White);

                string energyText = $"{EALocalization.Energy}: {energyPlayer.energy}/{energyPlayer.maxEnergy}";
                Vector2 textSize = font.MeasureString(energyText);
                Vector2 textPos = energyBasePos + new Vector2((energyBack.Width - textSize.X) / 2f, -20f);

                Main.spriteBatch.DrawString(font, energyText, textPos, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor));
            }
        }
        private bool CompareAssets(Asset<Texture2D> currentAsset, string compareAssetPath)
        {
            if (!vanillaAssetCache.TryGetValue(compareAssetPath, out var asset)) asset = vanillaAssetCache[compareAssetPath] = Main.Assets.Request<Texture2D>(compareAssetPath);
            return currentAsset == asset;
        }
    }
}