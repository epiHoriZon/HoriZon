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
    class Recompense
    {
        Texture2D texture;
        Texture2D activated_texture;
        public Rectangle affichage;
        public Rectangle container;
        public bool ouvert;

        public Recompense(Texture2D texture, Texture2D activated_texture, Rectangle container)
        {
            this.texture = texture;
            this.activated_texture = activated_texture;
            this.container = container;
            this.affichage = container;
        }


        public void ouverture()
        {
            texture = activated_texture;
            ouvert = true;
        }

        public bool obtention_recompense(Personnage heros)
        {
            return (heros.position.Intersects(container));
        }

        public void DrawRecompense(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, affichage, Color.White);
        }
    }
}
