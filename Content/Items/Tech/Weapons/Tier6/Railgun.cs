using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier6
{
    public class Railgun : ModItem
    {
        public float heat = 0; 
        //public override bool CloneNewInstances
        //{
        //    get { return true; }
        //}
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
            //item.GetGlobalItem<ItemEnergy>().energy = 18;
        }
        //public override void SetStaticDefaults()
        //{
        //    DisplayName.SetDefault("Railgun");
        //    Tooltip.SetDefault("Loves worms\nUsing the item charges heat\nGaining too much heat will damage the player and decrease the items effectiveness");
        //}
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
        //public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        //{
        //    Main.PlaySound(SoundID.Item113, (int)player.position.X, (int)player.position.Y); 
        //    Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 70f;
        //    MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
        //    if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))  position += muzzleOffset;           
        //    Projectile.NewProjectile(position.X, position.Y , speedX, speedY, ProjectileType<RailgunBeam>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
        //    if (heat > 1300) player.AddBuff(BuffType<BurningHands>(), 180, false);          
        //    if (heat > 1700)
        //    {
        //        int amount = (int)(player.statLifeMax2 * 0.2f);
        //        player.statLife -= amount;
        //        if (player.statLife < 0) player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " burnt their face off"), 1, 1);
        //    }
        //    heat += 160;
        //    return false;
        //}
        //public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        //{
        //    if (heat > 1300)
        //    {
        //        mult *= 1 - ((float)(heat - 1300) / 500f);
        //        Main.NewText(mult);
        //    }
        //}
        //public override Vector2? HoldoutOffset()
        //{
        //    return new Vector2(-26, -2);
        //}
        //public override void AddRecipes()
        //{
        //    ModRecipe recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemType<Coilgun>(), 1);
        //    recipe.AddIngredient(ItemType<GoldWire>(), 10);
        //    recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
        //    recipe.AddIngredient(ItemType<Microcontroller>(), 1);
        //    recipe.AddTile(TileID.MythrilAnvil);
        //    recipe.SetResult(this);
        //    recipe.AddRecipe();
        //}
    }
}
