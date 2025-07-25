using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.EALayer
{
    public class EaGreatLensLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Wings);
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();
            return modPlayer.greatLensTimer > 0;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MyPlayer modPlayer = drawPlayer.GetModPlayer<MyPlayer>();

            if (modPlayer.greatLensTimer > 0)
            {
                Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Extra/GreatLens").Value;
                int drawX = (int)(drawInfo.Position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.Position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);
                Color color = Lighting.GetColor((int)(drawPlayer.Center.X / 16), (int)(drawPlayer.Center.Y / 16)) * 0.7f;
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.3f, drawPlayer.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                data.shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.ReflectiveDye);
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }
}