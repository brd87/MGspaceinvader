using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;

namespace SpaceInvaderPlusPlus.Utilities
{
    public class TextInputHandler
    {
        private string RegexPattern { get; set; }
        private GameWindow Window { get; set; }
        public bool IsTextInputActive { get; set; }


        public TextInputHandler(string regexPattern, GameWindow window)
        {
            RegexPattern = regexPattern;
            Window = window;
            IsTextInputActive = false;
        }

        public void OnTextInput(object sender, TextInputEventArgs e)
        {
            if (e.Character == '\b')
            {
                if (Holder.PLAYERNAME.Length > 0)
                    Holder.PLAYERNAME = Holder.PLAYERNAME.Substring(0, Holder.PLAYERNAME.Length - 1);
            }
            else if (e.Character == ' ' || IsValid(e.Character))
            {
                if (Holder.PLAYERNAME.Length <= 20)
                    Holder.PLAYERNAME += e.Character;
            }
        }

        private bool IsValid(char character)
        {
            return Regex.IsMatch(character.ToString(), RegexPattern);
        }

        public void EnableTextInput()
        {
            Window.TextInput += OnTextInput;
            IsTextInputActive = true;
        }

        public void DisableTextInput()
        {
            Window.TextInput -= OnTextInput;
            IsTextInputActive = false;
        }
    }
}
