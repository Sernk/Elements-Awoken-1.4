using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using System;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.EAUtilities;
using ElementsAwoken.EASystem.EAPlayer;

namespace ElementsAwoken.Content.Items.Artifacts
{
    public class ElementalArcanum : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().artifact = true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(7, 4));
            EAU.SetSoul(Type);
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && EAList.Artifact.Contains(player.armor[i].type))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.pickSpeed -= 0.3f;
            player.endurance += 0.05f;
            player.jumpSpeedBoost += 2.0f;
            modPlayer.eaMagmaStone = true;
            player.fireWalk = true;
            modPlayer.lightningCloud = true;
            if (hideVisual)
            {
                modPlayer.lightningCloudHidden = true;
            }
            player.GetArmorPenetration(DamageClass.Generic) += 5;
            player.moveSpeed += 0.50f;
            modPlayer.frozenGauntlet = true;
            player.longInvince = true;
            player.starCloakItem = Item;
            player.noKnockback = true;
            player.magicCuffs = true;
            player.manaMagnet = true;
            player.manaFlower = true;
            player.arcticDivingGear = true;
            if (player.wet)
            {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.9f, 0.2f, 0.6f);
            }
            player.pStone = true;
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.5f))
            {
                player.endurance += 0.15f;
            }
            player.GetDamage(DamageClass.Melee) *= 1.35f;
            player.GetDamage(DamageClass.Ranged) *= 1.35f;
            player.GetDamage(DamageClass.Magic) *= 1.40f;
            player.GetDamage(DamageClass.Summon) *= 1.35f;
            player.GetCritChance(DamageClass.Melee) += 10;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
            player.statManaMax2 += 150;
            player.lifeRegen += 2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DiscordantSkull>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosFlameFlask>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GreatThunderTotem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FrozenGauntlet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EtherealShell>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Nanocore>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}