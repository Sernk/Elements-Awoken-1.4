using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EABiomeVisuals : ModSystem
    {
        public override void PostUpdateEverything()
        {
            var Leviathan = Main.LocalPlayer.GetModPlayer<MyPlayer>().useLeviathan;
            bool _LeviathanSkyVisual = false;
            NPC[] npc = Main.npc;
            foreach (NPC npc2 in npc)
            {
                if (Leviathan)
                {
                    _LeviathanSkyVisual = true;
                    break;
                }
            }
            if (_LeviathanSkyVisual)
            {
                if (!Filters.Scene["ElementsAwoken:VoidLeviathanHead"].IsActive())
                {
                    Filters.Scene.Activate("ElementsAwoken:VoidLeviathanHead", default(Vector2));
                }
            }
            else if (Filters.Scene["ElementsAwoken:VoidLeviathanHead"].IsActive())
            {
                Filters.Scene.Deactivate("ElementsAwoken:VoidLeviathanHead");
            }
        }
    }
    public class EABiomeSky : CustomSky
    {
        private bool isActive;

        private float intensity;

        public override void Update(GameTime gameTime)
        {
            if (isActive && intensity < 0.5f)
            {
                intensity += 0.01f;
            }
            else if (!isActive && intensity > 0f)
            {
                intensity -= 0.01f;
            }
        }

        private bool UpdatepyroIndex()
        {
            var Leviathan = Main.LocalPlayer.GetModPlayer<MyPlayer>().useLeviathan;
            bool _LeviathanSky = Leviathan = true;
            if (_LeviathanSky)
            {
                return true;
            }
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0f && minDepth < 0f)
            {
                spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(0, 50, 100) * intensity);
            }
        }

        public override float GetCloudAlpha()
        {
            return 0f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            isActive = false;
        }

        public override bool IsActive()
        {
            if (!isActive)
            {
                return intensity > 0f;
            }
            return true;
        }
    }
}
