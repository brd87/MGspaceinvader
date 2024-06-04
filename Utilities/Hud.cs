using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaderPlusPlus.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderPlusPlus.Utilities
{
    internal class Hud
    {
        private SpriteFont HudFont;
        private SpriteFont HudFontAux;
        private Vector2 HealthOffset;
        private Vector2 ShieldOffset;
        private Vector2 AmmoOffset;
        private Vector2 UltOffset;
        private Vector2 LoadingOffset;
        private Vector2 TravelOffset;
        private String Heath;
        private String Shield;
        private String Ammo;
        private String Ult;
        private String Loading;
        private String Travel;

        public Hud(ref General general) 
        {
            HudFont = general.CONTENT.Load<SpriteFont>("font/font_hudmain");
            HudFontAux = general.CONTENT.Load<SpriteFont>("font/font_hudaux");
            Heath = "HEALTH: ";
            Shield = "SHIELDS: ";
            Ammo = "AMMO: ";
            Ult = "POWER WEAPON READY";
            Loading = "LOADING...";
            Travel = "Travel: ";

            HealthOffset = HudFont.MeasureString(Heath + "100%") / 2;
            ShieldOffset = HudFont.MeasureString(Shield + "100%") / 2;
            AmmoOffset = HudFont.MeasureString(Ammo + "1000/1000") / 2;
            UltOffset = HudFontAux.MeasureString(Ult) / 2;
            LoadingOffset = HudFont.MeasureString(Loading) / 2;
            TravelOffset = HudFont.MeasureString(Travel + "100000") / 2;
        }

        public void DrawHUD(ref General general, ref Player player, ref Weapon weapon)
        {
            general.SPRITE_BATCH.DrawString(HudFont, Shield + player.Shields + "%", new Vector2((general.WIDTH / 4), 30) - ShieldOffset, Color.White, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(HudFont, Heath + player.Health + "%", new Vector2(general.WIDTH / 2, 30) - HealthOffset, Color.White, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(HudFont, Ammo + weapon.Ammunition + "|" +weapon.MaxAmmunition, new Vector2((general.WIDTH / 4 ) * 3, 30) - AmmoOffset, Color.White, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            if (player.UltAbility)
                general.SPRITE_BATCH.DrawString(HudFontAux, Ult, new Vector2((general.WIDTH / 4), 70) - UltOffset, Color.LightSkyBlue, 
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            if (!weapon.Loaded && weapon.Ammunition > 0)
                general.SPRITE_BATCH.DrawString(HudFontAux, Loading, new Vector2((general.WIDTH / 4) * 3, 70) - LoadingOffset, Color.IndianRed, 
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(HudFont, Travel + general.SCORE_TRAVEL, new Vector2(general.WIDTH / 10, general.HEIGHT - 50) - TravelOffset, Color.White, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(HudFontAux, $"+DMG: {general.SCORE_DMG}", new Vector2(general.WIDTH / 10, general.HEIGHT - 80) - TravelOffset, Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(HudFontAux, $"-DMG: {general.SCORE_DMGPLAYER}", new Vector2(general.WIDTH / 10, general.HEIGHT - 100) - TravelOffset, Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(HudFontAux, $"AMEF: {general.SCORE_AMMOWASTE}", new Vector2(general.WIDTH / 10, general.HEIGHT - 120) - TravelOffset, Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(HudFontAux, $"PICK: {general.SCORE_PICKUPS}", new Vector2(general.WIDTH / 10, general.HEIGHT - 140) - TravelOffset, Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(HudFontAux, $"Bonuses:", new Vector2(general.WIDTH / 10, general.HEIGHT - 160) - TravelOffset, Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            
        }
    }
}
