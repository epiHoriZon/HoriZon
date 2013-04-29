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
using HoriZon;

namespace HoriZon
{
    class Camera
    {
        public Vector2 position;
        Texture2D texture;
        
        public Camera( Vector2 position, Texture2D texture)       
        {
            this.texture = texture;
            this.position = position;
        }
        public int CameraWidth
        {
            get { return GraphicsDeviceManager.DefaultBackBufferWidth; } 
        }

        public int CameraHeight
        {
            get { return GraphicsDeviceManager.DefaultBackBufferHeight; }
        }

        public void Update(Rectangle position2)
        {
            position.X = 0;
            position.Y = 0;
            position.X = position2.X - CameraWidth / 2;
            position.Y = position2.Y - CameraHeight / 2;
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0; 
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            spriteBatch.Draw(texture, Vector2.Zero, new Rectangle((int)position.X, (int)position.Y, x, y), Color.White);
        }

        public void DrawHeros(SpriteBatch spritebatch, Personnage heros)
        {
            if (heros.position.Y > GraphicsDeviceManager.DefaultBackBufferHeight / 2)
                heros.affichage.Y = GraphicsDeviceManager.DefaultBackBufferHeight / 2;

            if (heros.position.X > GraphicsDeviceManager.DefaultBackBufferWidth / 2)
                heros.affichage.X = GraphicsDeviceManager.DefaultBackBufferWidth / 2;

            heros.Drawperso(spritebatch, 24, 32);
        }

        public void DrawFixe(SpriteBatch spritebatch, Personnage heros)
        {
        }
    }
}
