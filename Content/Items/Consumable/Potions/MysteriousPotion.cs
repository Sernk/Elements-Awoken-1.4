using ElementsAwoken.Content.Buffs;
using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    public class MysteriousPotion : ModItem
    {
        public int potionNum = 0;
        protected override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(potionNum);
        }
        public override void NetReceive(BinaryReader reader)
        {
            potionNum = reader.ReadInt32();
        }
        public override void SaveData(TagCompound tag)
        {
            potionNum = tag.GetInt("potionNum");
        }
        public override void LoadData(TagCompound tag)
        {
            potionNum = tag.GetInt("potionNum");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.useTurn = true;
            Item.consumable = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = 1;
            //item.potion = true;
            Item.buffType = BuffID.Regeneration;
            Item.buffTime = 0;
            return;
        }
        public override bool OnPickup(Player player)
        {
            if (ModContent.GetInstance<Config>().debugMode) potionNum = Main.rand.Next(10);
            return base.OnPickup(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            var EALocalization = ModContent.GetInstance<EALocalization>();

            string name = MyWorld.mysteriousPotionColours[potionNum];
            string desc = EALocalization.MysteriousPotion;
            if (modPlayer.mysteriousPotionsDrank[potionNum])
            {
                switch (potionNum)
                {
                    case 0:
                        name = EALocalization.MysteriousPotion1;
                        desc = EALocalization.MysteriousPotion2;
                        break;
                    case 1:
                        name = EALocalization.MysteriousPotion3;
                        desc = EALocalization.MysteriousPotion4;
                        break;
                    case 2:
                        name = EALocalization.MysteriousPotion5;
                        desc = EALocalization.MysteriousPotion6;
                        break;
                    case 3:
                        name = EALocalization.MysteriousPotion7;
                        desc = EALocalization.MysteriousPotion8;
                        break;
                    case 4:
                        name = EALocalization.MysteriousPotion9;
                        desc = EALocalization.MysteriousPotion10;
                        break;
                    case 5:
                        name = EALocalization.MysteriousPotion11;
                        desc = EALocalization.MysteriousPotion12;
                        break;
                    case 6:
                        name = EALocalization.MysteriousPotion13;
                        desc = EALocalization.MysteriousPotion14;
                        break;
                    case 7:
                        name = EALocalization.MysteriousPotion15;
                        desc = EALocalization.MysteriousPotion16;
                        break;
                    case 8:
                        name = EALocalization.MysteriousPotion17;
                        desc = EALocalization.MysteriousPotion18;
                        break;
                    case 9:
                        name = EALocalization.MysteriousPotion19;
                        desc = EALocalization.MysteriousPotion20;
                        break;
                }
            }
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.Text = desc;
                    if (ModContent.GetInstance<Config>().debugMode)
                    {
                        line2.Text += "\npotionNum:" + potionNum + "\ncolor:" + MyWorld.mysteriousPotionColours[potionNum];
                    }
                }
                if (line2.Mod == "Terraria" && line2.Name.Contains("ItemName"))
                {
                    line2.Text = name + " " + EALocalization.MysteriousPotion22;
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (potionNum == 0 && player.FindBuffIndex(BuffID.PotionSickness) != -1)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool ConsumeItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            var EALocalization = ModContent.GetInstance<EALocalization>();

            if (potionNum == 0)
            {
                int amount = (int)(player.statLifeMax2 * 0.2f);
                player.statLife += amount;
                player.HealEffect(amount);
                player.AddBuff(BuffID.PotionSickness, player.pStone ? 2700 : 3600);
            }
            else if (potionNum == 1)
            {
                int amount = (int)(player.statLifeMax2 * 0.2f);
                player.statLife -= amount;
                if (player.statLife < 0) player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromLiteral(player.name + " " + EALocalization.MysteriousPotion21)), 1, 1);
                CombatText.NewText(player.getRect(), Color.OrangeRed, -amount + " Health");
            }
            else if (potionNum == 2) player.AddBuff(ModContent.BuffType<RottenHeart>(), 3600);
            else if (potionNum == 3) player.AddBuff(ModContent.BuffType<StrongHeart>(), 3600);
            else if (potionNum == 4) player.AddBuff(ModContent.BuffType<Invincibility>(), 600);
            else if (potionNum == 5) player.AddBuff(ModContent.BuffType<GoldenWeapons>(), 1800);
            else if (potionNum == 6) player.AddBuff(ModContent.BuffType<Glowing>(), 1800);
            else if (potionNum == 7) player.AddBuff(BuffID.Poisoned, 3600);
            else if (potionNum == 8) player.AddBuff(ModContent.BuffType<SuperSlow>(), 1200);
            else if (potionNum == 9) player.AddBuff(ModContent.BuffType<SuperSpeed>(), 600);

            modPlayer.mysteriousPotionsDrank[potionNum] = true;
            if (ModContent.GetInstance<Config>().debugMode) Item.stack++;
            return base.ConsumeItem(player);
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Color color = Color.Red;
            if (MyWorld.mysteriousPotionColours[potionNum] == "Red") color = Color.Red;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Cyan") color = Color.Cyan;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Lime") color = Color.LimeGreen;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Fuschia") color = Color.HotPink;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Pink") color = Color.LightPink;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Brown") color = Color.RosyBrown;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Orange") color = Color.Orange;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Yellow") color = Color.Yellow;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Blue") color = Color.DarkBlue;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Purple") color = Color.Purple;
            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Items/Consumable/Potions/MysteriousPotionContents").Value;
            spriteBatch.Draw(tex, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Color color = Color.Red;
            if (MyWorld.mysteriousPotionColours[potionNum] == "Red") color = Color.Red;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Cyan") color = Color.Cyan;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Lime") color = Color.LimeGreen;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Fuschia") color = Color.DeepPink;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Pink") color = Color.LightPink;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Brown") color = Color.RosyBrown;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Orange") color = Color.Orange;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Yellow") color = Color.LightYellow;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Blue") color = Color.DarkBlue;
            else if (MyWorld.mysteriousPotionColours[potionNum] == "Purple") color = Color.Purple;
            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Items/Consumable/Potions/MysteriousPotionContents").Value;
            spriteBatch.Draw(tex, Item.position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}