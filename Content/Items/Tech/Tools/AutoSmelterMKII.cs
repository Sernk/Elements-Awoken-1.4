using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Tools
{
    public class AutoSmelterMKII : ModItem
    {
        public bool enabled = false;
        public int smeltCooldown = 0;

        protected override bool CloneNewInstances { get { return true; } }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 1;
            Item.maxStack = 1;
        }
        public override void SetStaticDefaults()
        {
            EAU.SetSoul(Type);
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tip = new TooltipLine(Mod, "Elements Awoken:Tooltip", "");
            if (enabled)
            {
                tip = new TooltipLine(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().GravityCore1);
                tip.OverrideColor = Color.Green;
            }
            else
            {
                tip = new(Mod, "Elements Awoken:Tooltip", ModContent.GetInstance<EALocalization>().GravityCore2);
                tip.OverrideColor = Color.Red;
            }
            tooltips.Insert(1, tip);
        }
        public override void UpdateInventory(Player player)
        {
            int energyConsumed = 2;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            smeltCooldown--;

            if (!enabled || smeltCooldown > 0) return;
                
            for (int i = 0; i < 50; i++)
            {
                Item item = Main.LocalPlayer.inventory[i];
                if (EAList.Tier1.TryGetValue(item.type, out var data) && item.stack >= data.need || EAList.Tier2.TryGetValue(item.type, out data) && item.stack >= data.need)
                {
                    smeltCooldown = 90;
                    modPlayer.energy -= energyConsumed;

                    item.stack -= data.need;
                    QuickItem(player, data.bar);
                    return;
                }
            }
            if (smeltCooldown <= 0)
            {
                Item hellOre = null, obsidian = null;
                for (int i = 0; i < 50; i++)
                {
                    Item other = Main.LocalPlayer.inventory[i];
                    if (other.type == ItemID.Hellstone) hellOre = other;
                    if (other.type == ItemID.Obsidian) obsidian = other;
                }

                if (hellOre?.stack >= 3 && obsidian?.stack >= 1)
                {
                    smeltCooldown = 90;
                    modPlayer.energy -= energyConsumed;

                    hellOre.stack -= 3;
                    obsidian.stack--;
                    QuickItem(player, ItemID.HellstoneBar);
                }
            }
            if (smeltCooldown <= 0)
            {
                Item voidOre = null, ashes = null;
                for (int i = 0; i < 50; i++)
                {
                    Item other = Main.LocalPlayer.inventory[i];
                    if (other.type == ItemType<VoiditeOre>()) voidOre = other;
                    if (other.type == ItemType<VoidAshes>()) ashes = other;
                }
                if (voidOre?.stack >= 4 && ashes?.stack >= 1)
                {
                    smeltCooldown = 90;
                    modPlayer.energy -= energyConsumed;

                    voidOre.stack -= 4;
                    ashes.stack--;
                    QuickItem(player, ItemType<VoiditeBar>());
                }
            }
        }
        static void QuickItem(Player source, int ID) => source.QuickSpawnItem(source.GetSource_FromThis(), ID);
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            if (enabled) enabled = false;
            else enabled = true;
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<AutoSmelter>(), 1);
            recipe.AddIngredient(ItemID.AdamantiteForge, 1);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 2);
            recipe.AddIngredient(ItemType<GoldWire>(), 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<AutoSmelter>(), 1);
            recipe.AddIngredient(ItemID.TitaniumForge, 1);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 2);
            recipe.AddIngredient(ItemType<GoldWire>(), 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}