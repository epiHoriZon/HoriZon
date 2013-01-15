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
    public class MenuButton
    {
        Texture2D memoire;
        Texture2D texture;
        Texture2D activatedTexture;

        Texture2D anglais_texture;
        Texture2D anglais_activated_texture;

        public bool anglais_on;
        Vector2 position;
        Rectangle container;

        MouseEvent mouse = new MouseEvent();


        public MenuButton(Vector2 position, Texture2D texture, Texture2D activatedTexture,Texture2D anglais_texture,Texture2D anglais_activated_texture)
        {
            this.position = position;
            this.texture = texture;
            this.anglais_texture = anglais_texture;
            this.activatedTexture = activatedTexture;
            this.anglais_activated_texture = anglais_activated_texture;
            if (!anglais_on)
            {
                this.memoire = texture;
            }
            else
            {
                this.memoire = anglais_texture;
            }
            
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
            if (!anglais_on)
            {
                texture = activatedTexture;
            }
            else
            {
                texture = anglais_activated_texture;
            }
            return texture;
        }
        public Texture2D desactiv()
        {
            if (!anglais_on)
            {
                texture = memoire;
            }
            else
            {
                texture = anglais_texture;
            }
           

            return texture;
        }


    }
}
