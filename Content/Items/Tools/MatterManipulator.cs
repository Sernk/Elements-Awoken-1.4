using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.Chaos;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tools
{
    public class MatterManipulator : ModItem
    {
        private int rightCD = 0;
        protected override bool CloneNewInstances => true;
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 12;
            Item.damage = 40;
            Item.knockBack = 6;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = 5;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.pick = 300;
            Item.axe = 65;
            Item.tileBoost += 30;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.UseSound = SoundID.Item23;
            Item.shoot = ProjectileType<Projectiles.Drills.MatterManipulator>();
            Item.shootSpeed = 40f;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (rightCD <= 0)
                {
                    ItemsGlobal modItem = Item.GetGlobalItem<ItemsGlobal>();
                    if (modItem.miningRadius == 0)
                    {
                        modItem.miningRadius = 1;
                        CombatText.NewText(player.getRect(), new Color(0, 150, 191), "3x3", true, false);
                    }
                    else if (modItem.miningRadius == 1)
                    {
                        modItem.miningRadius = 2;
                        CombatText.NewText(player.getRect(), new Color(0, 80, 138), "5x5", true, false);
                    }
                    else if (modItem.miningRadius == 2)
                    {
                        modItem.miningRadius = 0;
                        CombatText.NewText(player.getRect(), new Color(133, 229, 255), "1x1", true, false);
                    }
                    rightCD = 60;
                }
                return false;
            }
           
            return base.CanUseItem(player);
        }
        public override void UpdateInventory(Player player) => rightCD--;
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<DiscordantBar>(), 15);
            recipe.AddIngredient(ItemType<ChaoticFlare>(), 8);
            recipe.AddIngredient(ItemID.LaserDrill, 1);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}