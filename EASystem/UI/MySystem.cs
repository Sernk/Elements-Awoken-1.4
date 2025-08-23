using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI
{
    public class MySystem : ModSystem
    {
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            string text = "";
            DynamicSpriteFont font = FontAssets.DeathText.Value; ;

            Vector2 position = new Vector2(
                Main.screenWidth / 2,
                Main.screenHeight / 2 - 50 
            );

            Vector2 origin = font.MeasureString(text) / 2f;

            spriteBatch.DrawString(font, text,position, Color.White, 0f,origin,1f,SpriteEffects.None,0f);
        }
    }
}
