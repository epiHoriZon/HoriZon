﻿using System;
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
    public class MouseEvent
    {
        MouseState ButtonPressed;
        
         Rectangle mousedetection;
        public MouseEvent()
        {
   
        }
        public bool UpdateMouse()
        {
            ButtonPressed = Mouse.GetState();
            if (ButtonPressed.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Rectangle getmousecontainer()
        {
            ButtonPressed = Mouse.GetState();
            mousedetection = new Rectangle((int)ButtonPressed.X, (int)ButtonPressed.Y, (int)1, (int)1);
            return mousedetection;
        }


       
    }
}
