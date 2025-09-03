using ElementsAwoken.Content.Items.Accessories;
using ElementsAwoken.Content.Items.Elements.Elemental;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Developer
{
    public class Neovirtuo : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().developer = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.neovirtuoBonus = true;
            //asterox effects
            player.noKnockback = true;
            player.statDefense += 12;
            player.GetArmorPenetration(DamageClass.Generic) += 5;
            player.lifeRegen += 3;
            player.moveSpeed *= 1.2f;
            player.panic = true;
            if (!hideVisual)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<NeovirtuoShieldBase>()] < 1)
                {
                    Projectile.NewProjectile(EAU.Play(player), player.position.X, player.position.Y, 0f, 0f, ModContent.ProjectileType<NeovirtuoShieldBase>(), 50, 10f, player.whoAmI, 0.0f, 0.0f);
                }
            }

            //unity effects
            player.statDefense += 5;
            if (player.statLife <= (player.statLifeMax2 * 0.9f) && player.statLife >= (player.statLifeMax2 * 0.9f))
            {
                player.endurance += 0.04f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife >= (player.statLifeMax2 * 0.7f))
            {
                player.endurance += 0.08f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.7f) && player.statLife >= (player.statLifeMax2 * 0.6f))
            {
                player.endurance += 0.16f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife >= (player.statLifeMax2 * 0.5f))
            {
                player.endurance += 0.20f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.5f) && player.statLife >= (player.statLifeMax2 * 0.4f))
            {
                player.endurance += 0.24f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife >= (player.statLifeMax2 * 0.3f))
            {
                player.endurance += 0.28f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.3f) && player.statLife >= (player.statLifeMax2 * 0.2f))
            {
                player.endurance += 0.32f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.2f) && player.statLife >= (player.statLifeMax2 * 0.1f))
            {
                player.endurance += 0.36f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.1f))
            {
                player.endurance += 0.40f;
            }

            player.statManaMax2 += 100;
            player.GetDamage(DamageClass.Magic) *= 1.15f;
            player.moveSpeed *= 1.16f;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[46] = true;
            player.buffImmune[44] = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<Unity>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Asterox>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
