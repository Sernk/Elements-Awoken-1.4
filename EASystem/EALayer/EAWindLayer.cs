using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.EALayer
{
    public class EAWindLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.IceBarrier); // Wings || IceBarrier
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();
            return drawInfo.drawPlayer.active && !drawInfo.drawPlayer.dead;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModContent.GetInstance<ElementsAwoken>();
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();

            if (drawInfo.drawPlayer.wings == EquipLoader.GetEquipSlot(mod, "SkylineWhirlwind", EquipType.Wings))
            {
                Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Extra/SkylineWhirlwind").Value;
                int drawX = (int)(drawInfo.Position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.Position.Y + drawPlayer.height / 2f + 89 - Main.screenPosition.Y);
                Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16)) * modPlayer.skylineAlpha;
                Rectangle rect = new Rectangle(0, (texture.Height / 4) * modPlayer.skylineFrame, texture.Width, texture.Height / 4);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), rect, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.3f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }
}