using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Items.Tech.Weapons.Tier2;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier6
{
    public class Railgun : ModItem
    {
        public float heat = 0;
        protected override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;        
            Item.damage = 270;
            Item.knockBack = 3.5f;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 8;
            Item.shootSpeed = 5f;
            Item.shoot = 10;
            Item.useAmmo = 97;
            Item.GetGlobalItem<ItemEnergy>().energy = 18;
        }
        public override void UpdateInventory(Player player)
        {
            if (heat > 1800) heat = 1800;
            if (heat > 0)
            {
                heat--;
                if (player.wet) heat--;
                if (player.ZoneSnow) heat--;
            }
            if (heat < 0) heat = 0;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            SoundEngine.PlaySound(SoundID.Item113, player.position);
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 70f;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) position += muzzleOffset;
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ProjectileType<RailgunBeam>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            if (heat > 1300) player.AddBuff(BuffType<BurningHands>(), 180, false);
            if (heat > 1700)
            {
                int amount = (int)(player.statLifeMax2 * 0.2f);
                player.statLife -= amount;
                if (player.statLife < 0) player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromLiteral(player.name + " " + EALocalization.Railgun)), 1, 1);
            }
            heat += 160;
            return false;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            float mult = damage.Flat;
            if (heat > 1300)
            {
                mult *= 1 - ((float)(heat - 1300) / 500f);
                Main.NewText(mult);
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-26, -2);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Coilgun>(), 1);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ItemType<Microcontroller>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
