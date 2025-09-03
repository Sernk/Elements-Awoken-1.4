using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace ElementsAwoken.EASystem.UI.UIIIII
{
    class UISystemSettings : ModSystem
    {
        public static bool Panel = false;
        public static int VoidTime = 300;
        public static Color VoidColor = Color.White;
        public static bool VoidSay = false;
        public static bool VoidSay1 = false;
        public static bool VoidSay2 = false;
        public static bool VoidSay3 = false;
        public override void ClearWorld()
        {
            Panel = false;
            VoidSay = false;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            if (Panel) tag["panel"] = Panel;
            if (VoidSay) tag["VoidSay"] = VoidSay;
            if (VoidSay1) tag["VoidSay1"] = VoidSay1;
            if (VoidSay2) tag["VoidSay2"] = VoidSay2;
            if (VoidSay3) tag["VoidSay3"] = VoidSay3;
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.WriteFlags(Panel, VoidSay, VoidSay1, VoidSay2, VoidSay3);
        }
        public override void NetReceive(BinaryReader reader)
        {
            reader.ReadFlags(out Panel, out VoidSay, out VoidSay1, out VoidSay2, out VoidSay3);
        }
        public override void LoadWorldData(TagCompound tag)
        {
            Panel = tag.GetBool("panel");
            VoidSay = tag.GetBool("VoidSay");
            VoidSay1 = tag.GetBool("VoidSay1");
            VoidSay2 = tag.GetBool("VoidSay2");
            VoidSay3 = tag.GetBool("VoidSay3");
        }
        private GameTime _lastUpdateUiGameTime;
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("UIIIII: UI", delegate
                {
                    if (_lastUpdateUiGameTime != null && MyInterface?.CurrentState != null)
                    {
                        MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                    }
                    return true;
                }, InterfaceScaleType.UI));
            }
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (index != -1)
            {
                layers.Insert(index, new LegacyGameInterfaceLayer(
                    "YourMod: DrawText",
                    delegate
                    {
                        DrawCustomText(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        private void DrawCustomText(SpriteBatch spriteBatch)
        {
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            string text = player.encounterText;
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            Vector2 textSize = FontAssets.DeathText.Value.MeasureString(text);
            float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;
            Vector2 pos = new Vector2(textPositionLeft, Main.screenHeight / 2 - 200);
            Vector2 size = font.MeasureString(text);
            Vector2 position = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2) - size / 2;
            if (VoidSay)
            {
                Utils.DrawBorderStringFourWay(spriteBatch, font, DrawStringOutlined(spriteBatch, text, pos, VoidColor, 1f), pos.X, pos.Y, Color.White, Color.Black, Vector2.Zero);
            }
        }
        internal static string DrawStringOutlined(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale)
        {
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X - 1, position.Y), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X + 1, position.Y), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X, position.Y - 1), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X, position.Y + 1), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X, position.Y), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            return "";
        }
        //public void DrawEncounterText(SpriteBatch spriteBatch)
        //{
        //    var mod = ModLoader.GetMod("ElementsAwoken");
        //    var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
        //    string text = player.encounterText;
        //    if (player.encounterTextTimer > 0)
        //    {
        //        Vector2 textSize = FontAssets.DeathText.Value.MeasureString(text);
        //        float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;

        //        Vector2 pos = new Vector2(textPositionLeft, Main.screenHeight / 2 - 200);
        //        float rand = player.finalText ? 3.5f : 2f;
        //        pos.X += Main.rand.NextFloat(-rand, rand);
        //        pos.Y += Main.rand.NextFloat(-rand, rand);
        //        Color color = player.finalText ? new Color(player.encounterTextAlpha, 0, 0, player.encounterTextAlpha) : new Color(player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha);
        //        DrawStringOutlined(spriteBatch, text, pos, color, 1f);
        //    }
        //}
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (Panel)
            {
                if (MyInterface?.CurrentState == null)
                {
                    MyInterface?.SetState(MyUI);
                }
            }
            else
            {
                if (MyInterface?.CurrentState != null)
                {
                    MyInterface?.SetState(null);
                }
            }
            MyInterface?.Update(gameTime);
        }

        internal UserInterface MyInterface;
        internal Penal MyUI;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyUI = new Penal();
                MyUI.Activate();
            }
        }
    }
}