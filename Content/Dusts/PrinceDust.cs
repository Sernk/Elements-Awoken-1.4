﻿using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Dusts
{
	public class PrinceDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = true;
            dust.scale = 1.2f;
            dust.noGravity = true;
            dust.alpha = 30;
            dust.velocity /= 2f;
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X;
            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.15f, 0.05f, 0.15f);
            dust.scale -= 0.03f;
            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}