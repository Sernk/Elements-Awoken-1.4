using ElementsAwoken.Content.Buffs.PetBuffs;
using ElementsAwoken.Content.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Pets
{
    public class DynastyWhistle : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.width = 16;
            Item.height = 30;
            Item.damage = 0;
            Item.useStyle = 1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = new SoundStyle("ElementsAwoken/Sounds/Item/Whistle");
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<TurboDoge>();
            Item.buffType = ModContent.BuffType<TurboDogeBuff>();
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
            recipe.AddIngredient(ItemID.DynastyWood, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}