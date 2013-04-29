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
        public KeyboardState ButtonPressed;
       

        public KeyboardEvent()
        { 
        
        }

        public bool Is_Back_Pressed()
        {
            
            ButtonPressed = Keyboard.GetState();
            return (ButtonPressed.IsKeyDown (Keys.Back));
        }

        public bool Is_A_Pressed()
        {
            
            ButtonPressed = Keyboard.GetState();
            return (ButtonPressed.IsKeyDown(Keys.A));
        }

        public bool Is_E_Pressed()
        { 
            ButtonPressed = Keyboard.GetState();
            return (ButtonPressed.IsKeyDown(Keys.E));
        }
        public bool Is_R_Pressed()
        {

            ButtonPressed = Keyboard.GetState();
            return (ButtonPressed.IsKeyDown(Keys.R));
        }

        public bool Is_Space_Pressed()
        {

            ButtonPressed = Keyboard.GetState();
            return (ButtonPressed.IsKeyDown(Keys.Space));
        }

        public bool Is_I_Pressed()
        {

            ButtonPressed = Keyboard.GetState();
            return (ButtonPressed.IsKeyDown(Keys.I));
        }

         public bool Is_M_Pressed()
        {
            
            ButtonPressed = Keyboard.GetState();
            return (ButtonPressed.IsKeyDown (Keys.M));
        }
    }
}
