using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HoriZontestmenu
{
    class KeyboardEvent
    {
        KeyboardState ButtonPressed;

        public KeyboardEvent()
        { 
        
        }

        public bool Is_Back_Pressed()
        {
            ButtonPressed = Keyboard.GetState();
            return (ButtonPressed.IsKeyDown (Keys.Back));
        }
    }
}
