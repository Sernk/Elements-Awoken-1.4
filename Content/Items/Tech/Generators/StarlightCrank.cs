using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class StarlightCrank : ModItem
    {
        public int crankCooldown = 0;
        protected override bool CloneNewInstances { get { return true; } }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 3;
            Item.maxStack = 1;
        }
        public override void UpdateInventory(Player player)
        {
            crankCooldown--;
        }
        public override bool CanRightClick() => true;
        public override void RightClick(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (crankCooldown <= 0 && modPlayer.energy < modPlayer.maxEnergy)
            {
                SoundEngine.PlaySound(new SoundStyle("ElementsAwokenSounds/Item/HandCrank"), Item.position);
                modPlayer.energy += 3;
                crankCooldown = 20;
            }
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HandCrank>(), 1);
            recipe.AddIngredient(ItemID.SunplateBlock, 16);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 8);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}