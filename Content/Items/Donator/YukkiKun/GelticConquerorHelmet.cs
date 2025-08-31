using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.YukkiKun
{
    [AutoloadEquip(EquipType.Head)]
    public class GelticConquerorHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 1;
            Item.defense = 2;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GelticConquerorBreastplate>() && legs.type == ModContent.ItemType<GelticConquerorLeggings>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowEOCShield = true;
            player.armorEffectDrawOutlinesForbidden = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ModContent.GetInstance<EALocalization>().GelticConquerorSetBonus;
            Point playerTopLeft = (player.TopLeft / 16).ToPoint();
            Point playerBottomRight = (player.BottomRight / 16).ToPoint();
            bool touchingCobweb = false;
            for (int k = playerTopLeft.X; k <= playerBottomRight.X; k++)
            {
                for (int l = playerTopLeft.Y; l <= playerBottomRight.Y; l++)
                {
                    Tile t = Framing.GetTileSafely(k, l);
                    if (t.TileType == TileID.Cobweb)
                    {
                        touchingCobweb = true;
                    }
                }
            }
            if (!touchingCobweb && !player.controlDown && !player.mount.Active)
            {
                if (player.velocity.Y <= player.gravity && player.oldVelocity.Y != 0 && player.oldVelocity.Y > 5) // > 5 so he doesnt bounce when jumping, only when falling
                {
                    player.velocity.Y = player.oldVelocity.Y * -0.98f;
                    player.velocity.X *= 1.2f;
                }
            }
            player.oldVelocity.Y = player.velocity.Y; // setting what the old velocity is after so it is the OLD velocity and not current

            player.jumpSpeedBoost += 2.0f;
            player.GetModPlayer<MyPlayer>().gelticConqueror = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}