using ElementsAwoken.EASystem.UI.UIIIII;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.EASystem.UI
{
    /// <summary>
    /// It's better not to quiz this instead you should <use cref="Penal"> </add>
    /// </summary>
    internal class PromptInfoUI : UIState
    {
        public DragableUIPanel BackPanel;
        public UIHoverImageButton CloseButton;
        public UIDisplay UIDisplay;
        public static bool Visible;
        private int uiWidth = 500;
        private int uiHeight = 250;

        public override void OnInitialize()
        {
            BackPanel = new DragableUIPanel();
            BackPanel.SetPadding(0);

            BackPanel.Left.Set(Main.screenWidth / 2 - uiWidth / 2, 0f);
            BackPanel.Top.Set(Main.screenHeight / 2 - uiHeight / 2, 0f);
            BackPanel.Width.Set(uiWidth, 0f);
            BackPanel.Height.Set(uiHeight, 0f);
            BackPanel.BackgroundColor = new Color(73, 94, 171);

            int buttonSize = 44;

            CloseButton = new UIHoverImageButton(Request<Texture2D>("ElementsAwoken/Extra/ButtonClose2x2"), "Close");
            CloseButton.Width.Set(buttonSize, 0f);
            CloseButton.Height.Set(buttonSize, 0f);
            CloseButton.Left.Set(uiWidth - 10 - buttonSize, 0f);
            CloseButton.Top.Set(uiHeight - 10 - buttonSize, 0f);
            CloseButton.OnLeftClick += CloseButtonClicked;

            UIDisplay = new UIDisplay();
            UIDisplay.Left.Set(0, 0f);
            UIDisplay.Top.Set(0, 0f);
            UIDisplay.Width.Set(uiWidth, 0f);
            UIDisplay.Height.Set(uiHeight, 1f);

            BackPanel.Append(CloseButton);
            BackPanel.Append(UIDisplay);
            Append(BackPanel);
        }
        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            Visible = false;
        }
    }
    public class UIDisplay : UIElement
    {
        public UIDisplay()
        {
            Width.Set(100, 0f);
            Height.Set(40, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();

            var EALocalization = GetInstance<Utilities.EALocalization>();

            float uiX = innerDimensions.X;
            float uiY = innerDimensions.Y;

            string sentence = EALocalization.Prompt;
            string[] words = sentence.Split(new char[] { ' ' });
            IList<string> sentenceParts = new List<string>();
            sentenceParts.Add(string.Empty);

            int lineNum = 0;

            float descX = uiX + 20;
            float descY = uiY + 20;

            foreach (string word in words)
            {
                if (FontAssets.MouseText.Value.MeasureString(sentenceParts[lineNum] + word).X > 480)
                {
                    lineNum++;
                    sentenceParts.Add(string.Empty);
                }
                sentenceParts[lineNum] += word + " ";
            }
            string message = "";
            foreach (string x in sentenceParts)
                message += x + "\n";
            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, message, descX, descY, Color.White, Color.Black, new Vector2(0.3f), 1f);

            //innerDimensions.Width;
            if (Language.ActiveCulture.Name == "ru-RU")
            {
                float promptY = uiY + innerDimensions.Height - 48 - 24;
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/ScorpionBreakout").Value, new Vector2(uiX + 64, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/InfernacesWrath").Value, new Vector2(uiX + 64 * 2, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/DarkenedSkies").Value, new Vector2(uiX + 64 * 3, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/Hypothermia").Value, new Vector2(uiX + 64 * 4, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/StormSurge").Value, new Vector2(uiX + 64 * 5, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/Psychosis").Value, new Vector2(uiX + 64 * 6, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
            else
            {
                float promptY = uiY + innerDimensions.Height - 48 - 32;
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/ScorpionBreakout").Value, new Vector2(uiX + 64, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/InfernacesWrath").Value, new Vector2(uiX + 64 * 2, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/DarkenedSkies").Value, new Vector2(uiX + 64 * 3, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/Hypothermia").Value, new Vector2(uiX + 64 * 4, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/StormSurge").Value, new Vector2(uiX + 64 * 5, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/Buffs/Prompts/Psychosis").Value, new Vector2(uiX + 64 * 6, promptY), null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
        }
    }
}