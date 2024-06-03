using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;

namespace SpaceInvaderPlusPlus.Utilities
{
    internal class TextInputHandler
    {
        private string RegexPattern;
        private GameWindow Window;
        private General GeneralInstance;
        public bool IsTextInputActive { get; set; }


        public TextInputHandler(string regexPattern, ref GameWindow window)
        {
            RegexPattern = regexPattern;
            Window = window;
            IsTextInputActive = false;
        }

        public void SetGeneralInstance(ref General general)
        {
            GeneralInstance = general;
        }

        public void OnTextInput(object sender, TextInputEventArgs e)
        {
            if (e.Character == '\b')
            {
                if (GeneralInstance.PLAYERNAME.Length > 0)
                    GeneralInstance.PLAYERNAME = GeneralInstance.PLAYERNAME.Substring(0, GeneralInstance.PLAYERNAME.Length - 1);
            }
            else if (e.Character == ' ' || IsValid(e.Character))
            {
                if (GeneralInstance.PLAYERNAME.Length <= 20)
                    GeneralInstance.PLAYERNAME += e.Character;
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
