using ElementsAwoken.Content.NPCs.Bosses.Azana;
using ElementsAwoken.Content.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Other
{
    public class Tomato : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.damage = 1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = 1;
            Item.useTime = 12;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 0, 0, 5);
            Item.rare = 0;
            Item.shoot = ModContent.ProjectileType<TomatoP>();
            Item.shootSpeed = 4f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Azana>()))
            {
                speed.X *= 2f;
                speed.Y *= 2f;
            }
            return true;
        }
    }
}
