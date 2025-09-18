using ElementsAwoken.Content.Items.Tools;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.EASystem.UI
{
	internal class DimensionalManipulatorUI : UIState
	{
        internal UIFloatRangedDataValue timeToSet;
        public override void OnInitialize() 
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            timeToSet = new UIFloatRangedDataValue("timeToSet:", modPlayer.voidTimeChangeTime, 0, 1);
        }
        public override void Update(GameTime gameTime)
        {
			base.Update(gameTime);
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            if (Main.LocalPlayer.HeldItem.type != ItemType<DimensionalManipulator>()) GetInstance<ElementsAwoken>().VoidTimerChangerUI.SetState(null);
		}     
		protected override void DrawSelf(SpriteBatch spriteBatch) 
        {
            Main.NewText("f");
		}
	}
}