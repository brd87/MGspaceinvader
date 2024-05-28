using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace SpaceInvaderPlusPlus.Menus
{
    public class TitleMenu
    {
        public float Cooldawn { get; set; }
        public TimeSpan LastTime { get; set; }
        public SpriteFont TitleFont { get; set; }
        public SpriteFont SubTitleFont { get; set; }
        public SpriteFont OptionFont { get; set; }
        public string TitleContent { get; set; }
        public string SubTitleContent { get; set; }
        public List<string> Options { get; set; }
        public Vector2 TitleOffset { get; set; }
        public Vector2 SubTitleOffset { get; set; }
        public int TopOffset { get; set; }
        public int Begin { get; set; }
        public List<Vector2> OptionOffsets { get; set; }
        public List<Color> OptionColors { get; set; }
        public int CurrentSelected { get; set; }

        public TitleMenu()
        {
            Cooldawn = 0.2f;
            LastTime = TimeSpan.FromSeconds(0.0f);

            TitleFont = Holder.CONTENT.Load<SpriteFont>("font_title");
            TitleContent = "S.I.P.P";
            TitleOffset = TitleFont.MeasureString(TitleContent) / 2;

            SubTitleFont = Holder.CONTENT.Load<SpriteFont>("font_subtitle");
            SubTitleContent = "Space Invader Plus Plus";
            SubTitleOffset = SubTitleFont.MeasureString(SubTitleContent) / 2;

            OptionFont = Holder.CONTENT.Load<SpriteFont>("font_options");
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

            TopOffset = 38;
            Begin = 400;

            CurrentSelected = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - LastTime >= TimeSpan.FromSeconds(Cooldawn))
            {
                if (Holder.KSTATE.IsKeyDown(Keys.W))
                {
                    LastTime = gameTime.TotalGameTime;
                    OptionColors[CurrentSelected] = Color.Gray;
                    if (CurrentSelected == 0)
                        CurrentSelected = Options.Count - 1;
                    else
                        CurrentSelected--;
                    OptionColors[CurrentSelected] = Color.White;
                }
                else if (Holder.KSTATE.IsKeyDown(Keys.S))
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
                if (i == CurrentSelected && Holder.KSTATE.IsKeyDown(Keys.Enter))
                    if (Holder.KSTATE != Holder.KSTATE_PREV)
                    {
                        Holder.MENUMODE = i;
                        if (CurrentSelected == 0) Holder.STARTNEW = true;
                    }
        }

        public void Draw()
        {
            Holder.SPRITE_BATCH.DrawString(TitleFont, TitleContent, new Vector2(Holder.WIDTH / 2, 270) - TitleOffset, Color.Wheat);
            Holder.SPRITE_BATCH.DrawString(SubTitleFont, SubTitleContent, new Vector2(Holder.WIDTH / 2, 340) - SubTitleOffset, Color.Wheat);
            for (int i = 0; i < Options.Count; i++)
                Holder.SPRITE_BATCH.DrawString(OptionFont, Options[i], new Vector2(Holder.WIDTH / 2, Begin + i * TopOffset) - OptionOffsets[i], OptionColors[i]);
        }
    }
}
