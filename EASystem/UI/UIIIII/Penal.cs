using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ElementsAwoken.EASystem.UI.UIIIII
{
    /// <summary>
    /// The original panel cannot be closed
    /// </summary>
    class Penal : UIState
    {
        public static bool Visible;
        private int uiWidth = 500;
        private int uiHeight = 250;
        public UIImage CloseButton;
        public UIDisplay UIDisplay;
        public override void OnInitialize()
        {
            DraggableUIPanel panel = new();
            panel.SetPadding(0);

            panel.Left.Set(Main.screenWidth / 2 - uiWidth / 2, 0f);
            panel.Top.Set(Main.screenHeight / 2 - uiHeight / 2, 0f);
            panel.Width.Set(uiWidth, 0f);
            panel.Height.Set(uiHeight, 0f);
            panel.BackgroundColor = new Color(73, 94, 171);

            int buttonSize = 44;

            CloseButton = new UIImage(ModContent.Request<Texture2D>("ElementsAwoken/Extra/ButtonClose2x2"));
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

            panel.Append(UIDisplay);
            panel.Append(CloseButton);
            Append(panel);
        }
        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            UISystemSettings.Panel = false;
        }
    }
    public class DraggableUIPanel : UIPanel
    {
        private Vector2 offset;
        private bool dragging;

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            DragStart(evt);
        }
        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            DragEnd(evt);
        }

        private void DragStart(UIMouseEvent evt)
        {
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt)
        {
            Vector2 end = evt.MousePosition;
            Left.Set(end.X - offset.X, 0f);
            Top.Set(end.Y - offset.Y, 0f);
            Recalculate();
            dragging = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (dragging)
            {
                Vector2 mouse = UserInterface.ActiveInstance.MousePosition;
                Left.Set(mouse.X - offset.X, 0f);
                Top.Set(mouse.Y - offset.Y, 0f);
                Recalculate();
            }
        }
    }
}