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
        private SpriteFont _hudFont;
        private SpriteFont _hudFontAux;
        public Hud(ref General general) 
        {
            _hudFont = general.CONTENT.Load<SpriteFont>("font/font_hudmain");
            _hudFontAux = general.CONTENT.Load<SpriteFont>("font/font_hudaux");
        }

        public void DrawHUD(ref General general, ref Player player, ref Weapon weapon)
        {
            general.SPRITE_BATCH.DrawString(_hudFont, $"HEALTH: {player.Health}%", new Vector2(20, 20), Color.White, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(_hudFont, $"SHIELDS: {player.Shields}%", new Vector2(360, 20), Color.White, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(_hudFont, $"AMMO {weapon.Ammunition}|{weapon.MaxAmmunition}", new Vector2(720, 20), Color.White, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            if (player.UltAbility)
                general.SPRITE_BATCH.DrawString(_hudFontAux, $"POWER WEAPON READY", new Vector2(380, 50), Color.LightSkyBlue, 
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            if (!weapon.Loaded && weapon.Ammunition > 0)
                general.SPRITE_BATCH.DrawString(_hudFontAux, $"LOADING...", new Vector2(720, 50), Color.IndianRed, 
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(_hudFont, $"Travel: {general.SCORE_TRAVEL}", new Vector2(400, 850), Color.White, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);

            general.SPRITE_BATCH.DrawString(_hudFontAux, $"Bonuses:", new Vector2(20, 782), Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(_hudFontAux, $"+DMG: {general.SCORE_DMG}", new Vector2(20, 800), Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(_hudFontAux, $"-DMG: {general.SCORE_DMGPLAYER}", new Vector2(20, 818), Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(_hudFontAux, $"AMEF: {general.SCORE_AMMOWASTE}", new Vector2(20, 836), Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(_hudFontAux, $"PICK: {general.SCORE_PICKUPS}", new Vector2(20, 854), Color.IndianRed, 
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }
    }
}
