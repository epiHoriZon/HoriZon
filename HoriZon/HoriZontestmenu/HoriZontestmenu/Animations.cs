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
using HoriZontestmenu;

namespace HoriZon
{
    class Animations
    {
        Texture2D texture;
        Rectangle container;


        int Timer = 0;
        int AnimationSpeed = 7;

        int Framecolumn = 2;
       public  int Frameline = 1;


        public Animations(Texture2D texture, Rectangle container)
        {
            this.texture = texture;
            this.container = container;
        }

        public void Animate(int nbcolonnes)
        {
              Timer++;
            if (Timer >= AnimationSpeed)
            {
                Timer = 0;
                Framecolumn++;
                if (Framecolumn > nbcolonnes)
                {
                    Framecolumn = 1;
                }
            }

        }

        public void DrawAnimate(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, container, new Rectangle((Framecolumn - 1) * 72, (Frameline - 1) * 72, 72, 72), Color.White);

        }

    }
}
