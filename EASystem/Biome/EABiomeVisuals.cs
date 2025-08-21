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
            var Leviathan = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            bool _AncientsSkyVisual = false;
            bool _AzanaSkyVisual = false;
            bool _LeviathanSkyVisual = false;
            bool _VolcanoxSkyVisual = false;
            bool _TheGuardianFlySkyVisual = false;
            bool _InfernaceSkyVisual = false;
            bool _PermafrostSkyVisual = false;

            if (Leviathan.useLeviathan)
            {
                _LeviathanSkyVisual = true;
            }
            if (_LeviathanSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.VoidLeviathanHead].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.VoidLeviathanHead, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.VoidLeviathanHead].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.VoidLeviathanHead);
            }

            if (Leviathan.useInfernace)
            {
                _InfernaceSkyVisual = true;
            }
            if (_InfernaceSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Infernace].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Infernace, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Infernace].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Infernace);
            }

            if (Leviathan.usePermafrost)
            {
                _PermafrostSkyVisual = true;
            }
            if (_PermafrostSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Permafrost].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Permafrost, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Permafrost].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Permafrost);
            }

            if (Leviathan.useGuardian)
            {
                _TheGuardianFlySkyVisual = true;
            }
            if (_TheGuardianFlySkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.TheGuardianFly].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.TheGuardianFly, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.TheGuardianFly].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.TheGuardianFly);
            }


            if (Leviathan.useVolcanox)
            {
                _VolcanoxSkyVisual = true;
            }
            if (_VolcanoxSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Volcanox].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Volcanox, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Volcanox].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Volcanox);
            }

            if (Leviathan.useAzana)
            {
                _AzanaSkyVisual = true;
            }
            if (_AzanaSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Azana].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Azana, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Azana].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Azana);
            }

            if (Leviathan.useAncients)
            {
                _AncientsSkyVisual = true;
            }
            if (_AncientsSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Ancients].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Ancients, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Ancients].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Ancients);
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
