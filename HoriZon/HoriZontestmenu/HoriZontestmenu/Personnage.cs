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

    public enum Direction
    {
        Up, Down, Left, Right
    }
    class Personnage
    {
        public int Points_Vie_Perso;
        int pv_depart;

      


        public Texture2D skin;
        public Rectangle position;
        public Rectangle positiondepart;
        Rectangle container;
        public Direction direction;


        int Frameline = 1;
        int Framecolumn = 1;

        int speed = 2;
        int Timer = 0;
        int AnimationSpeed = 5;

        public Personnage(Texture2D skin, Rectangle position, int Points_Vie_Perso)
        {
            this.skin = skin;
            this.position = position;
            this.positiondepart = position;
            this.Points_Vie_Perso = Points_Vie_Perso;
            this.pv_depart = Points_Vie_Perso;
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
        public void deplacement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && position.Y != 0)
            {
                position.Y -= speed;
                direction = Direction.Up;

                Animate(5);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && position.Y <= 480 - position.Height)
            {
                position.Y += speed;
                direction = Direction.Down;

                Animate(5);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && position.X != 0)
            {
                position.X -= speed;
                direction = Direction.Left;
                Animate(5);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && position.X <= 800 - position.Width)
            {
                position.X += speed;
                direction = Direction.Right;
                Animate(5);
            }
            else
            {
                Framecolumn = 1;
            }


            switch (direction)
            {
                case Direction.Up: Frameline = 5;
                    break;
                case Direction.Down: Frameline = 1;
                    break;
                case Direction.Left: Frameline = 7;
                    break;
                case Direction.Right: Frameline = 3;
                    break;

            }

        }

        public void animationmonstre()
        {
            switch (direction)
            {
                case Direction.Up: Frameline = 1;
                    break;
                case Direction.Down: Frameline = 3;
                    break;
                case Direction.Left: Frameline = 2;
                    break;
                case Direction.Right: Frameline = 4;
                    break;

            }


        }

        public Rectangle getplayercontainer()
        {
            container = new Rectangle((int)position.X, (int)position.Y, (int)position.X + skin.Width, (int)position.Y + skin.Height);
            return container;
        }



        public void Drawperso(SpriteBatch spritebatch, int largeur, int hauteur)
        {
            if (Points_Vie_Perso <= (20 / 100) * pv_depart)
            {
                spritebatch.Draw(skin, position, new Rectangle((Framecolumn - 1) * largeur, (Frameline - 1) * hauteur, largeur, hauteur), Color.Red);
            }
            else
            {
                spritebatch.Draw(skin, position, new Rectangle((Framecolumn - 1) * largeur, (Frameline - 1) * hauteur, largeur, hauteur), Color.White);
            }
        }



    }
}
