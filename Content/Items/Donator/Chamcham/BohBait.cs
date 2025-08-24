using ElementsAwoken.Content.Buffs.PetBuffs;
using ElementsAwoken.Content.Projectiles.Pets;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Chamcham
{
    public class BohBait : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.damage = 0;
            Item.useStyle = 1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item2;
            Item.rare = 2;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<ChamchamRat>();
            Item.buffType = ModContent.BuffType<ChamchamRatBuff>();
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Feather, 5);
            recipe.AddIngredient(ItemID.FishingSeaweed, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}