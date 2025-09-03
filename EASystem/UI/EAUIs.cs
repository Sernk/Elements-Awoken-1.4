using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace ElementsAwoken.EASystem.UI
{
    public class EAUIs : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            ElementsAwoken mod = ModContent.GetInstance<ElementsAwoken>();
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();
            var encounter = ElementsAwoken.encounter;
            var screenText = ElementsAwoken.screenText;
            var screenTextTimer = ElementsAwoken.screenTextTimer;
            var screenTextAlpha = ElementsAwoken.screenTextAlpha;
            var screenTextDuration = ElementsAwoken.screenTextDuration;
            var screenTextPos = ElementsAwoken.screenTextPos;
            var screenTextScale = ElementsAwoken.screenTextScale;

            if (!Main.gameMenu)
            {
                if (MyWorld.credits)
                {
                    layers.Clear();

                    var creditsState = new LegacyGameInterfaceLayer("ElementsAwoken: Credits",
                        delegate
                        {
                            mod.Credits();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(0, creditsState); // because all layers are deleted, 0 is the next one
                }
                else
                {
                    if (!player.ghost)
                    {
                        // computer
                        if (modPlayer.inComputer)
                        {
                            var computerLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                            var computerState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                                delegate
                                {
                                    mod.DrawComputer(Main.spriteBatch);
                                    return true;
                                },
                                InterfaceScaleType.UI);
                            layers.Insert(computerLayer, computerState);
                        }
                    }
                    // encounter text
                    if (encounter != 0)
                    {
                        var encounterLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
                        var encounterState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                            delegate
                            {
                                mod.DrawEncounterText(Main.spriteBatch);
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(encounterLayer, encounterState);
                    }
                    //Voidblood glow
                    if (modPlayer.voidBlood)
                    {
                        var heartLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                        var heartState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                            delegate
                            {
                                ElementsAwoken.DrawVoidBloodGlow();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(heartLayer, heartState);
                    }




                    //                // hearts & mana
                    //                //if (!calamityEnabled)
                    //                //{
                    //                //    var heartLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                    //                //    var heartState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                    //                //        delegate
                    //                //        {
                    //                //            ElementsAwoken.DrawHearts();
                    //                //            return true;
                    //                //        },
                    //                //        InterfaceScaleType.UI);
                    //                //    layers.Insert(heartLayer, heartState);

                    //                //    // to stop hearts being underneath
                    //                //    if (modPlayer.voidHeartsUsed == 10)
                    //                //    {
                    //                //        Main.heart2Texture = GetTexture("Extra/Blank");
                    //                //    }
                    //                //    else
                    //                //    {
                    //                //        Main.heart2Texture = GetTexture("Extra/Heart2");
                    //                //    }
                    //                //    if (modPlayer.lunarStarsUsed == 1)
                    //                //    {
                    //                //        Main.manaTexture = GetTexture("Extra/Mana2");
                    //                //    }
                    //                //    else
                    //                //    {
                    //                //        Main.manaTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Mana");
                    //                //    }
                    //                //}
                    #region energy & insanity UI

                    var BarLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                    var BarState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                        delegate
                        {
                            if (ModContent.GetInstance<Config>().resourceBars)
                            {
                                ElementsAwoken.DrawEnergyBar();
                                ElementsAwoken.DrawInsanityBar();
                            }
                            else
                            {
                                ElementsAwoken.DrawEnergyUI();
                                ElementsAwoken.DrawInsanityUI();
                            }
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(BarLayer, BarState);

                    var insanityOverlayLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                    var insanityOverlayState = new LegacyGameInterfaceLayer("ElementsAwoken: Interface Logic 1",
                        delegate
                        {
                            ElementsAwoken.DrawInsanityOverlay();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(insanityOverlayLayer, insanityOverlayState);

                    #endregion

                    // sanity book
                    if (awakenedPlayer.openSanityBook)
                    {
                        var bookLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                        var bookState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                            delegate
                            {
                                mod.DrawSanityBook();
                                return true;
                            },
                            InterfaceScaleType.UI);
                        layers.Insert(bookLayer, bookState);
                    }
                    #region info accessories
                    var infoLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                    var infoState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                        delegate
                        {
                            mod.DrawInfoAccs();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(infoLayer, infoState);
                    #endregion
                    //if (player.HeldItem.type == ModContent.ItemType<Railgun>())
                    //{
                    //    var heatBarLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                    //    var heatBarState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                    //        delegate
                    //        {
                    //            ElementsAwoken.DrawHeatBar(player.HeldItem);
                    //            return true;
                    //        },
                    //        InterfaceScaleType.UI);
                    //    layers.Insert(heatBarLayer, heatBarState);
                    //}



                    // rain texture
                    if (encounter == 3)
                    {
                        TextureAssets.Rain = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Rain3");
                    }
                    else if (MyWorld.radiantRain)
                    {
                        TextureAssets.Rain = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Rain4");
                    }
                    else
                    {
                        TextureAssets.Rain = ModContent.Request<Texture2D>("Terraria/Images" + Path.DirectorySeparatorChar.ToString() + "Rain");
                    }
                    // infernace clouds
                    if (MyWorld.firePrompt > ElementsAwoken.bossPromptDelay)
                    {
                        for (int cloud = 0; cloud < 22; cloud++)
                        {
                            TextureAssets.Cloud[cloud] = ModContent.Request<Texture2D>(string.Concat(new object[] { "Terraria/Images", Path.DirectorySeparatorChar.ToString(), "Cloud_", cloud }));
                        }
                    }
                    else
                    {
                        ElementsAwoken.ResetCloudTexture();
                    }
                }

                var textLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var textState = new LegacyGameInterfaceLayer("ElementsAwoken: Text",
                    delegate
                    {
                        for (int i = 0; i < screenText.Length - 1; i++)
                        {
                            if (screenText[i] != "" && screenText[i] != null)
                            {
                                screenTextTimer[i]++;
                                if (screenTextTimer[i] < screenTextDuration[i] / 8) screenTextAlpha[i] += 1f / ((float)screenTextDuration[i] / 8f);// 1/4 of the time fading 
                                else if (screenTextTimer[i] > screenTextDuration[i] - (screenTextDuration[i] / 8)) screenTextAlpha[i] -= 1f / ((float)screenTextDuration[i] / 8f);
                                else screenTextAlpha[i] = 1;
                                if (screenTextTimer[i] > screenTextDuration[i]) screenText[i] = "";

                                ElementsAwoken.DrawStringOutlined(Main.spriteBatch, screenText[i], screenTextPos[i], Color.White * screenTextAlpha[i], screenTextScale[i]);
                            }
                        }
                        return true;

                    },
                    InterfaceScaleType.UI);
                if (!MyWorld.credits) layers.Insert(textLayer, textState);
                else layers.Insert(1, textState); // in credits all layers r deleted

                if (modPlayer.screenTransition)
                {
                    var transLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 4"));
                    var transState = new LegacyGameInterfaceLayer("ElementsAwoken: Transitions",
                        delegate
                        {
                            ElementsAwoken.BlackScreenTrans();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    if (!MyWorld.credits) layers.Insert(transLayer, transState);
                    else layers.Insert(1, transState); // in credits all layers r deleted
                }
            }
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ElementsAwoken: Alchemist UI",
                    delegate
                    {
                        mod.AlchemistUserInterface.Draw(Main.spriteBatch, new GameTime());
                        mod.VoidTimerChangerUI.Draw(Main.spriteBatch, new GameTime());
                        if (PromptInfoUI.Visible) mod.PromptInfoUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            ElementsAwoken mod = ModContent.GetInstance<ElementsAwoken>();
            mod.AlchemistUserInterface?.Update(gameTime);
            mod.VoidTimerChangerUI?.Update(gameTime);
            if (PromptInfoUI.Visible)
            {
                if (mod.PromptInfoUserInterface?.CurrentState == null)
                {
                    mod.PromptInfoUserInterface?.SetState(mod.PromptUI);
                }
            }
            else
            {
                if (mod.PromptInfoUserInterface?.CurrentState == null)
                {
                    mod.PromptInfoUserInterface?.SetState(null);
                }
            }
        }
    }
}