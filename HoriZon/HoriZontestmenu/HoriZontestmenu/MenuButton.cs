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
    public class MenuButton
    {
        Texture2D memoire;
        Texture2D texture;
        Texture2D activatedTexture;
        Vector2 position;
        Rectangle container;

        MouseEvent mouse = new MouseEvent();


        public MenuButton(Vector2 position, Texture2D texture, Texture2D activatedTexture)
        {
            this.position = position;
            this.texture = texture;

            this.activatedTexture = activatedTexture;
            this.memoire = texture;
         
        }

        public Rectangle getcontainer()
        {
            container = new Rectangle
                ((int)position.X,
                (int)position.Y,
                ((int)texture.Width),
                ((int)texture.Height));
            return container;
        }

        public void DrawButton(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, new Rectangle ((int)position.X,(int)position.Y,210,107), Color.White);
        }

        public Texture2D activ()
        {
            
            texture = activatedTexture;

            return texture;
        }
        public Texture2D desactiv()
        {
            texture = memoire;

            return texture;
        }

    }
}
