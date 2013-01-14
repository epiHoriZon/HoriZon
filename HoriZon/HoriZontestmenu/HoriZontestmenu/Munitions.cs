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

namespace HoriZon
{
    class Munitions
    {
        public Vector2 position;
        Rectangle container;
        Texture2D texture;
        public Munitions(Vector2 position, Texture2D texture)       
        {
            this.position = position;
            this.texture = texture;

        }

        public Rectangle getContainer()
        {
            container = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            return container;
        }

     




        public void DrawMunitions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Orange);


        }
    }
}
