using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles
{
    public class StatMirror : ModTile
	{
        public override void Load()
        {
            _ = this.GetLocalization("Stat.Melee").Value;
            _ = this.GetLocalization("Stat.Magic").Value;
            _ = this.GetLocalization("Stat.Ranged").Value;
            _ = this.GetLocalization("Stat.Thrown").Value;
            _ = this.GetLocalization("Stat.Summoner").Value;
            _ = this.GetLocalization("Stat.MaxMinions").Value;
            _ = this.GetLocalization("Stat.SwingSpeed").Value;
            _ = this.GetLocalization("Stat.MovementSpeed").Value;
            _ = this.GetLocalization("Stat.DamageReduction").Value;
            _ = this.GetLocalization("Stat.MAXHP").Value;
            _ = this.GetLocalization("Stat.MAXMP").Value;
            _ = this.GetLocalization("Stat.HPRegen").Value;
        }
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleWrapLimit = 2; //not really necessary but allows me to add more subtypes of chairs below the example chair texture
            TileObjectData.newTile.StyleMultiplier = 2; //same as above
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1); //facing right will use the second texture style
            Main.tileLighted[Type] = true;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(66, 241, 244));
            EAU.DSCursor(Type);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.0f;
            g = 0.0f;
            b = 0.3f;
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];

            float meleeDamage = player.GetDamage(DamageClass.Melee).Additive;
            float magicDamage = player.GetDamage(DamageClass.Magic).Additive;
            float rangerDamage = player.GetDamage(DamageClass.Ranged).Additive;
            float throwingDamage = player.GetDamage(DamageClass.Throwing).Additive;
            float summonDamage = player.GetDamage(DamageClass.Summon).Additive;

            player.cursorItemIconText = string.Format(this.GetLocalization("Stat.Melee").Value, (int)(meleeDamage * 100), (player.GetCritChance(DamageClass.Melee) - 4)) +
                 "\n" + string.Format(this.GetLocalization("Stat.Magic").Value, (int)(magicDamage * 100), (player.GetCritChance(DamageClass.Magic) - 4)) +
                 "\n" + string.Format(this.GetLocalization("Stat.Ranged").Value, (int)(rangerDamage * 100), (player.GetCritChance(DamageClass.Ranged) - 4)) +
                 "\n" + string.Format(this.GetLocalization("Stat.Thrown").Value, (int)(throwingDamage * 100), (player.GetCritChance(DamageClass.Throwing) - 4)) +
                 "\n" + string.Format(this.GetLocalization("Stat.Summoner").Value, (int)(summonDamage * 100)) +
                 "\n" + string.Format(this.GetLocalization("Stat.MaxMinions").Value, (player.maxMinions - 1), (player.maxTurrets - 1)) +
                 "\n" + string.Format(this.GetLocalization("Stat.SwingSpeed").Value, (int)(100 - (player.GetAttackSpeed(DamageClass.Melee) * 100))) + 
                 "\n" + string.Format(this.GetLocalization("Stat.MovementSpeed").Value, (int)((player.moveSpeed * 100) - 100)) +
                 "\n" + string.Format(this.GetLocalization("Stat.DamageReduction").Value, (int)(player.endurance * 100)) +
                 "\n" + string.Format(this.GetLocalization("Stat.MAXHP").Value, (int)(player.statLifeMax)) +
                 "\n" + string.Format(this.GetLocalization("Stat.MAXMP").Value, (int)(player.statManaMax)) +
                 "\n" + string.Format(this.GetLocalization("Stat.HPRegen").Value, (int)(player.lifeRegen));

            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
        }
    }
}