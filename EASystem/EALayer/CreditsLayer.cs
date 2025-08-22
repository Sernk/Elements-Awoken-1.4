using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.EALayer
{
    public class CreditsLayer : ModPlayer
    {
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            for (int i = 0; i < drawInfo.DrawDataCache.Count; i++)
            {
                DrawData data = drawInfo.DrawDataCache[i];
                data.sourceRect = Rectangle.Empty; 
                drawInfo.DrawDataCache[i] = data;
            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (MyWorld.credits)
            {
                drawInfo.mountOffSet = 0f;
                drawInfo.drawPlayer.invis = true;
                drawInfo.colorArmorBody = Color.Transparent;
                drawInfo.colorArmorHead = Color.Transparent;
                drawInfo.colorArmorLegs = Color.Transparent;
                drawInfo.colorBodySkin = Color.Transparent;
                drawInfo.colorEyeWhites = Color.Transparent;
                drawInfo.colorEyes = Color.Transparent;
                drawInfo.colorHair = Color.Transparent;
                drawInfo.headGlowColor = Color.Transparent;
                drawInfo.headGlowColor = Color.Transparent;
            }
        }
        public override void PostUpdate()
        {
            if (MyWorld.credits)
            {
                Player.immuneAlpha = 0;
                Player.immuneTime = 999999999;          
                Player.immune = true;         
                Player.noFallDmg = true;        
                Player.noItems = true;
                Main.mapStyle = 0;
                Main.mapFullscreen = false;
            }
            else if (Player.immuneTime > 100)
            {
                Player.immuneTime = 1;
                Player.immune = false;
                Player.noFallDmg = false;
                Player.noItems = false;
            }
            else
            {

            }
        }
    }
}