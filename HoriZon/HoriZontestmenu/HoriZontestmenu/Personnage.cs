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
using HoriZon;

namespace HoriZontestmenu
{

    public enum Direction
    {
        Up, Down, Left, Right
    }
    class Personnage
    {
        public List<Equipements> inventaire;
        public List<Equipements> equip;
        public int Points_Vie_Perso;
        int pv_depart;
        Map map;
        public int numero;

        public int attaque;
        int tir;
        int defense;

        public int bonus_attaque;
        int bonus_tir;
        public int bonus_defense;

        public Texture2D skin;
        public Rectangle position;
        public Rectangle affichage;
        public Rectangle positiondepart;
        Rectangle container;
        public Direction direction;

        int Frameline = 1;
        int Framecolumn = 1;

        public int speed = 2;
        int Timer = 0;
        public int AnimationSpeed = 5;

        public Personnage(Texture2D skin, Rectangle position, int pv_depart, int attaque, int tir, int defense, List<Equipements> inventaire, List<Equipements> equip,Map mapmap)
        {
            this.attaque = attaque;
            this.defense = defense;
            this.tir = tir;
            this.inventaire = inventaire;
            this.equip = equip;
            this.Points_Vie_Perso = pv_depart;
            this.pv_depart = pv_depart;

            map = mapmap;

            this.skin = skin;
            this.position = position;
            this.positiondepart = position;

            this.affichage = position;
        }

        public void Inventaire()
        {
            this.bonus_attaque = attaque;
            this.bonus_defense = defense;
            this.bonus_tir = tir;

            if (equip != null)
            {
                for (int i = 0; i < equip.Count; i++)
                {
                    this.bonus_attaque = bonus_attaque + equip[i].bonus_attaque;
                    this.bonus_defense = bonus_defense + equip[i].bonus_defense;
                    this.bonus_tir = bonus_tir + equip[i].bonus_tir;
                }
            }

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
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (map.GetFropPos(position.X, position.Y - speed) == 0)
                    position.Y -= speed;
                else if (map.GetFropPos(position.X, position.Y - speed) == 2)
                    position.Y -= speed / 2;

                direction = Direction.Up;
                Animate(3);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (map.GetFropPos(position.X, position.Y + 32 + 5) == 0 && map.GetFropPos(position.X + speed + 24, position.Y - speed + 32) == 0)
                    position.Y += speed;
                else if (map.GetFropPos(position.X, position.Y + 32) == 2 && map.GetFropPos(position.X + speed + 24, position.Y - speed + 32) == 2)
                    position.Y += speed / 2;

                direction = Direction.Down;
                Animate(3);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (map.GetFropPos(position.X + 5 + 24, position.Y) == 0 && map.GetFropPos(position.X + 24, position.Y - speed + 32) == 0)
                    position.X += speed;
                else if (map.GetFropPos(position.X + speed + 24, position.Y) == 2 && map.GetFropPos(position.X + 24, position.Y - speed + 32) == 2)
                    position.X += speed / 2;

                direction = Direction.Right;
                Animate(3);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (map.GetFropPos(position.X - speed, position.Y) == 0 && map.GetFropPos(position.X, position.Y + 32) == 0)
                    position.X -= speed;
                else if (map.GetFropPos(position.X - speed, position.Y) == 2)
                    position.X -= speed / 2;

                direction = Direction.Left;
                Animate(3);
            }
            else
            {
                Framecolumn = 2;
            }


            switch (direction)
            {
                case Direction.Up: Frameline = 1;
                    break;
                case Direction.Down: Frameline = 3;
                    break;
                case Direction.Left: Frameline = 4;
                    break;
                case Direction.Right: Frameline = 2;
                    break;

            }

        }

        public void animationmonstre(int numero)
        {
            if (numero == 2)
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
            else if (numero == 4)
            {

                switch (direction)
                    {
                     case Direction.Up: Frameline = 4;
                            break;
                      case Direction.Down: Frameline = 1;
                          break;
                     case Direction.Left: Frameline = 2;
                           break;
                     case Direction.Right: Frameline = 3;
                           break;

                    }


            }
	

        }

     

        public Rectangle getplayercontainer()
        {
            container = new Rectangle((int)position.X, (int)position.Y, (int)position.X + skin.Width, (int)position.Y + skin.Height);
            return container;
        }

        public bool EnnemiCollision(Rectangle balle)
        {
            return (container.Intersects(balle));
        }

        public void Drawperso(SpriteBatch spritebatch, int largeur, int hauteur)
        {
            if (Points_Vie_Perso <= ((20 / 100) * pv_depart))
            {
                spritebatch.Draw(skin, affichage, new Rectangle((Framecolumn - 1) * largeur, (Frameline - 1) * hauteur, largeur, hauteur), Color.Red);
                if (Game1.cri_montre_devient_rouge_fait == false)
                {
                    Game1.cri_montre_devient_rouge.Play();
                    Game1.cri_montre_devient_rouge_fait = true;
                }
            }
            else
            {
                spritebatch.Draw(skin, affichage, new Rectangle((Framecolumn - 1) * largeur, (Frameline - 1) * hauteur, largeur, hauteur), Color.White);
            }
        }



    }
}
