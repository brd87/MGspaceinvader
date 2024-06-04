using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace SpaceInvaderPlusPlus.Menus
{
    internal class TitleMenu
    {
        private float Cooldawn;
        private TimeSpan LastTime;
        private SpriteFont TitleFont;
        private SpriteFont SubTitleFont;
        private SpriteFont OptionFont;
        private string TitleContent;
        private string SubTitleContent;
        private List<string> Options;
        private Vector2 TitleOffset;
        private Vector2 SubTitleOffset;
        private int Begin;
        private int BerginSubTitleOffset;
        private int BeginMainOffset;
        private int BeginOptionsOffset;
        private List<Vector2> OptionOffsets;
        private List<Color> OptionColors;
        private int CurrentSelected;

        public TitleMenu(ref General general)
        {
            Cooldawn = 0.2f;
            LastTime = TimeSpan.FromSeconds(0.0f);

            TitleFont = general.CONTENT.Load<SpriteFont>("font/font_title");
            TitleContent = "S.I.P.P";
            TitleOffset = TitleFont.MeasureString(TitleContent) / 2;

            SubTitleFont = general.CONTENT.Load<SpriteFont>("font/font_subtitle");
            SubTitleContent = "Space Invader Plus Plus";
            SubTitleOffset = SubTitleFont.MeasureString(SubTitleContent) / 2;

            OptionFont = general.CONTENT.Load<SpriteFont>("font/font_options");
            Options = new List<string>() { "PLAY", "Scoreboard", "Settings", "Exit" };
            OptionOffsets = new List<Vector2>();
            OptionColors = new List<Color>();
            for (int i = 0; i < Options.Count; i++)
            {
                OptionOffsets.Add(OptionFont.MeasureString(Options[i]) / 2);
                if (i == 0)
                {
                    OptionColors.Add(Color.White);
                    continue;
                }
                OptionColors.Add(Color.Gray);
            }

            Begin = general.HEIGHT / 4;
            BerginSubTitleOffset = Begin + 70;
            BeginMainOffset = BerginSubTitleOffset + 50;
            BeginOptionsOffset = 38;
            

            CurrentSelected = 0;
        }

        public void Update(ref General general, GameTime gameTime)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn))
            {
                if (general.KSTATE.IsKeyDown(Keys.W))
                {
                    LastTime = gameTime.TotalGameTime;
                    OptionColors[CurrentSelected] = Color.Gray;
                    if (CurrentSelected == 0)
                        CurrentSelected = Options.Count - 1;
                    else
                        CurrentSelected--;
                    OptionColors[CurrentSelected] = Color.White;
                }
                else if (general.KSTATE.IsKeyDown(Keys.S))
                {
                    LastTime = gameTime.TotalGameTime;
                    OptionColors[CurrentSelected] = Color.Gray;
                    if (CurrentSelected == Options.Count - 1)
                        CurrentSelected = 0;
                    else
                        CurrentSelected++;
                    OptionColors[CurrentSelected] = Color.White;
                }
            }

            for (int i = 0; i < Options.Count; i++)
                if (i == CurrentSelected && general.KSTATE.IsKeyDown(Keys.Enter))
                    if (general.KSTATE != general.KSTATE_PREV)
                    {
                        general.MENUMODE = i;
                        if (CurrentSelected == 0) general.GAMESTATE = 1;
                        else if(CurrentSelected == 3) general.GAMESTATE = 4;
                    }
        }

        public void Draw(ref General general)
        {
            general.SPRITE_BATCH.DrawString(TitleFont, TitleContent, new Vector2(general.WIDTH / 2, Begin) - TitleOffset, Color.Wheat,
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            general.SPRITE_BATCH.DrawString(SubTitleFont, SubTitleContent, new Vector2(general.WIDTH / 2, BerginSubTitleOffset) - SubTitleOffset, Color.Wheat,
                0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            for (int i = 0; i < Options.Count; i++)
                general.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(general.WIDTH / 2, BeginMainOffset + i * BeginOptionsOffset) - OptionOffsets[i], OptionColors[i],
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }
    }
}
