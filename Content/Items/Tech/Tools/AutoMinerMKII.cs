using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Tools
{
    public class AutoMinerMKII : ModItem
    {
        public bool enabled = false;
        public int digCooldown = 0;

        public Vector2 justDugOre = new Vector2();

        protected override bool CloneNewInstances { get { return true; } }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 4;
            Item.maxStack = 1;
            Item.pick = 165;
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
            int energyConsumed = 4;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (enabled && modPlayer.energy > energyConsumed)
            {
                digCooldown--;
                if (digCooldown >= 55)
                {
                    for (int k = 0; k < Main.maxItems; k++)
                    {
                        Item other = Main.item[k];
                        if (other.getRect().Intersects(new Rectangle((int)justDugOre.X, (int)justDugOre.Y, 16, 16)))
                        {
                            other.Center = player.Center;
                        }

                    }
                }
                if (digCooldown <= 0)
                {
                    int distance = 10 * 16;
                    Point topLeft = ((player.position - new Vector2(distance, distance)) / 16).ToPoint();
                    Point bottomRight = ((player.BottomRight + new Vector2(distance, distance)) / 16).ToPoint();
                    
                    for (int i = topLeft.X; i <= bottomRight.X; i++)
                    {
                        for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                        {
                            Point tilePos = new Point(i, j);
                            Tile t = Framing.GetTileSafely(i, j);
                            bool canMineModOre = false;
                            ModTile modTile = TileLoader.GetTile(t.TileType);
                            if (modTile != null)
                            {
                                if (modTile.GetType().Name.Contains("Ore"))
                                {
                                    canMineModOre = true;
                                }
                            }
                            if ((TileID.Sets.Ore[t.TileType] == true || canMineModOre) && GlobalTiles.GetTileMinPick(t.TileType) <= Item.pick)
                            {
                                WorldGen.KillTile(tilePos.X, tilePos.Y);
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, (float)tilePos.X, (float)tilePos.Y, 0f, 0, 0, 0);
                                justDugOre = new Vector2(i * 16, j * 16);

                                Vector2 difference = player.Center - new Vector2(i * 16, j * 16);
                                for (int k = 0; k < 40; k++)
                                {
                                    Dust dust = Main.dust[Dust.NewDust(new Vector2(i * 16, j * 16) + difference * Main.rand.NextFloat(), 0, 0, 61)];
                                    dust.velocity = Vector2.Zero;
                                    dust.noGravity = true;
                                    dust.color = Color.Turquoise;
                                }

                                digCooldown = 60;
                                modPlayer.energy -= energyConsumed;
                                return;
                            }
                        }
                    }
                }
            }
        }
        public override bool CanRightClick() => true;
        public override void RightClick(Player player)
        {
            if (enabled) enabled = false;
            else enabled = true;
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AutoMiner>(), 1);
            recipe.AddIngredient(ItemID.MythrilPickaxe, 1);
            recipe.AddRecipeGroup(EARecipeGroups.AdamantiteBar, 8);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 6);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AutoMiner>(), 1);
            recipe.AddIngredient(ItemID.OrichalcumPickaxe, 1);
            recipe.AddRecipeGroup(EARecipeGroups.AdamantiteBar, 8);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 6);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}