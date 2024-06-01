using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Menus
{
    public class ScoreboardMenu
    {
        private float Cooldawn;
        private TimeSpan LastTime;
        private SpriteFont ReturnFont;
        private SpriteFont PlayerRecordFont;
        private string ReturnContent;
        private Vector2 ReturnOffset;
        private int LeftOffset;
        private int RightOffset;
        private int TopOffset;
        private int Begin;
        private Color ReturnColor;
        private List<Color> PlayerRecordColors;
        private int CurrentSelected;

        public ScoreboardMenu()
        {
            Cooldawn = 0.2f;
            LastTime = TimeSpan.FromSeconds(0.0f);

            ReturnFont = Holder.CONTENT.Load<SpriteFont>("font/font_options");
            ReturnContent = "Return";
            ReturnOffset = ReturnFont.MeasureString(ReturnContent) / 2;
            ReturnColor = Color.White;

            PlayerRecordFont = Holder.CONTENT.Load<SpriteFont>("font/font_records");
            PlayerRecordColors = new List<Color>();
            for (int i = 0; i < Holder.TOP_PLAYERS.Players.Count; i++)
                PlayerRecordColors.Add(Color.Salmon);

            LeftOffset = 250;
            RightOffset = 200;
            TopOffset = 18;
            Begin = 350;

            CurrentSelected = -1;
        }

        public void Update(GameTime gameTime)
        {
            if (Holder.TOP_PLAYERS.Players.Count > PlayerRecordColors.Count)
                PlayerRecordColors.Add(Color.Salmon);

            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn))
            {
                if (Holder.KSTATE.IsKeyDown(Keys.W))
                {
                    LastTime = gameTime.TotalGameTime;
                    if (CurrentSelected == -1) ReturnColor = Color.Gray;
                    else PlayerRecordColors[CurrentSelected] = Color.Salmon;

                    if (CurrentSelected == -1) CurrentSelected = PlayerRecordColors.Count - 1;
                    else CurrentSelected--;

                    if (CurrentSelected == -1) ReturnColor = Color.White;
                    else PlayerRecordColors[CurrentSelected] = Color.Red;
                }
                else if (Holder.KSTATE.IsKeyDown(Keys.S))
                {
                    LastTime = gameTime.TotalGameTime;
                    if (CurrentSelected == -1) ReturnColor = Color.Gray;
                    else PlayerRecordColors[CurrentSelected] = Color.Salmon;

                    if (CurrentSelected == PlayerRecordColors.Count - 1) CurrentSelected = -1;
                    else CurrentSelected++;

                    if (CurrentSelected == -1) ReturnColor = Color.White;
                    else PlayerRecordColors[CurrentSelected] = Color.Red;
                }
            }

            if (CurrentSelected == -1 && Holder.KSTATE.IsKeyDown(Keys.Enter))
                if (Holder.KSTATE != Holder.KSTATE_PREV) Holder.MENUMODE = 0;
        }

        public void Draw()
        {
            Holder.SPRITE_BATCH.DrawString(ReturnFont, ReturnContent, new Vector2(Holder.WIDTH / 2, 300) - ReturnOffset, ReturnColor);
            if (Holder.TOP_PLAYERS.Players.Count == PlayerRecordColors.Count)
                for (int i = 0; i < Holder.TOP_PLAYERS.Players.Count; i++)
                {
                    Holder.SPRITE_BATCH.DrawString(PlayerRecordFont, $"{i + 1}.", new Vector2(Holder.WIDTH / 2 - LeftOffset, Begin + i * TopOffset), PlayerRecordColors[i]);
                    Holder.SPRITE_BATCH.DrawString(PlayerRecordFont, Holder.TOP_PLAYERS.Players[i].PlayerName, new Vector2(Holder.WIDTH / 2 - LeftOffset + 50, Begin + i * TopOffset), PlayerRecordColors[i]);
                    Holder.SPRITE_BATCH.DrawString(PlayerRecordFont, Holder.TOP_PLAYERS.Players[i].Score.ToString(), new Vector2(Holder.WIDTH / 2 + RightOffset, Begin + i * TopOffset), PlayerRecordColors[i]);
                }
        }
    }
}
