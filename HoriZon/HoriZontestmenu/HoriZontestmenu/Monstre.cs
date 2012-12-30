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
   
    class Monstre
    {
        Rectangle container;
        Texture2D skinm;
        public Vector2 position;

        public Vector2 positiondepart;
   


        public Monstre(Texture2D skinm,Vector2 position)
        {
            this.skinm = skinm;
            this.position = position;
            this.positiondepart = position;
        }

        public Rectangle getmonstercontainer()
        {
            container = new Rectangle((int)position.X, (int)position.Y, (int)position.X + skinm.Width, (int)position.Y + skinm.Height);
            return container;
        }

        public void deplacement ()
        {
        
        }

        public void DrawMonstre(SpriteBatch spritebatch)
        {
            spritebatch.Draw(skinm, position, Color.White);
        }

    }
}
