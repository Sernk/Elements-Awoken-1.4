using ElementsAwoken.Content.Buffs.Prompts;
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
            bool _AqueousSkyVisual = false;
            bool _PermafrostSkyVisual = false;
            bool _InfernaceSkyVisual = false; 

            bool _VoidEventSkyVisual = false;
            bool _VoidEventDarkSkyVisual = false;
            bool _RadiantRainSkyVisual = false;

            bool _Encounter1SkyVisual = false;
            bool _Encounter2SkyVisual = false;
            bool _Encounter3SkyVisual = false;

            bool _DespairSkyVisual = false;
            bool _BlizzardSkyVisual = false;
            bool _InfernacesWrathSkyVisual = false;

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

            if (Leviathan.useAqueous)
            {
                _AqueousSkyVisual = true;
            }
            if (_AqueousSkyVisual && Leviathan.useAqueousint == 2)
            {
                if (!Filters.Scene[ElementsAwoken.Aqueous].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Aqueous, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Aqueous].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Aqueous);
            }

            #region Event
            if (Leviathan.useVoidEvent)
            {
                _VoidEventSkyVisual = true;
            }
            if (_VoidEventSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.VoidEvent].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.VoidEvent, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.VoidEvent].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.VoidEvent);
            }

            if (Leviathan.useVoidEventDark)
            {
                _VoidEventDarkSkyVisual = true;
            }
            if (_VoidEventDarkSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.VoidEventDark].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.VoidEventDark, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.VoidEventDark].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.VoidEventDark);
            }

            if (Leviathan.useRadRain)
            {
                _RadiantRainSkyVisual = true;
            }
            if (_RadiantRainSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.RadiantRain].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.RadiantRain, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.RadiantRain].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.RadiantRain);
            }
            #endregion
            #region Regaroth
            if (Leviathan.useRegaroth == 1)
            {
                if (!Filters.Scene[ElementsAwoken.Regaroth].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Regaroth, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Regaroth].IsActive() || Leviathan.useRegaroth == 2)
            {
                Filters.Scene.Deactivate(ElementsAwoken.Regaroth);
            }

            if (Leviathan.useRegaroth == 2)
            {
                if (!Filters.Scene[ElementsAwoken.Regaroth2].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Regaroth2, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Regaroth2].IsActive() || Leviathan.useRegaroth == 3)
            {
                Filters.Scene.Deactivate(ElementsAwoken.Regaroth2);
            }

            if (Leviathan.useRegaroth == 3)
            {
                if (!Filters.Scene[ElementsAwoken.RegarothIntense].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.RegarothIntense, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.RegarothIntense].IsActive() || Leviathan.useRegaroth == 4)
            {
                Filters.Scene.Deactivate(ElementsAwoken.RegarothIntense);
            }

            if (Leviathan.useRegaroth == 4)
            {
                if (!Filters.Scene[ElementsAwoken.Regaroth2Intense].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Regaroth2Intense, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Regaroth2Intense].IsActive() || Leviathan.useRegaroth == 0)
            {
                Filters.Scene.Deactivate(ElementsAwoken.Regaroth2Intense);
            }

            if (Leviathan.useRegaroth == 0)
            {
                Filters.Scene.Deactivate(ElementsAwoken.Regaroth);
                Filters.Scene.Deactivate(ElementsAwoken.Regaroth2);
                Filters.Scene.Deactivate(ElementsAwoken.RegarothIntense);
                Filters.Scene.Deactivate(ElementsAwoken.Regaroth2Intense);
            }
            #endregion
            #region Encounter
            if (Leviathan.useEncounter1)
            {
                _Encounter1SkyVisual = true;
            }
            if (_Encounter1SkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Encounter1].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Encounter1, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Encounter1].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Encounter1);
            }

            if (Leviathan.useEncounter2)
            {
                _Encounter2SkyVisual = true;
            }
            if (_Encounter2SkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Encounter2].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Encounter2, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Encounter2].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Encounter2);
            }

            if (Leviathan.useEncounter3)
            {
                _Encounter3SkyVisual = true;
            }
            if (_Encounter3SkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Encounter3].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Encounter3, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Encounter3].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Encounter3);
            }
            #endregion
            #region ??SkyVisual
            if (Leviathan.useDespair)
            {
                _DespairSkyVisual = true;
            }
            if (_DespairSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Despair].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Despair, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Despair].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Despair);
            }

            if (Leviathan.useblizzard)
            {
                _BlizzardSkyVisual = true;
            }
            if (_BlizzardSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.Blizzard].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.Blizzard, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.Blizzard].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.Blizzard);
            }

            if (Leviathan.useInfWrath)
            {
                _InfernacesWrathSkyVisual = true;
            }
            if (_InfernacesWrathSkyVisual)
            {
                if (!Filters.Scene[ElementsAwoken.InfernacesWrath].IsActive())
                {
                    Filters.Scene.Activate(ElementsAwoken.InfernacesWrath, default);
                }
            }
            else if (Filters.Scene[ElementsAwoken.InfernacesWrath].IsActive())
            {
                Filters.Scene.Deactivate(ElementsAwoken.InfernacesWrath);
            }
            #endregion
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
