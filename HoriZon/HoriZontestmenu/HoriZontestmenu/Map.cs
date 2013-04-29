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
using System.IO;
using HoriZontestmenu;


namespace HoriZon
{
    class Map
    {
        string filename;
        StreamReader file;

        Texture2D tile_0;
        Texture2D tile_1;
        Texture2D water;
        private short[,] tailMap;
        Rectangle affichage;

        public Map(Texture2D tile_0, Texture2D tile_1, Texture2D water, string filename)
        {
            this.filename = filename;
            this.tile_0 = tile_0;
            this.tile_1 = tile_1;
            this.water = water;
            tailMap = new short[100, 30];
        }

        public void draw(SpriteBatch s, Personnage heros, Camera camera)
        {
            for (int y = 0; y < tailMap.GetLength(1); y++)
            {
                for (int x = 0; x < tailMap.GetLength(0); x++)
                {
                    affichage.X = 32 * x;
                    affichage.Y = 32 * y;
                    if (heros.position.X > camera.CameraWidth / 2)
                        affichage.X = camera.CameraWidth / 2 - heros.position.X + 32 * x;
                    if (heros.position.Y > camera.CameraHeight / 2)
                        affichage.Y = camera.CameraHeight / 2 - heros.position.Y + 32 * y; 
                    switch (tailMap[x, y])
                    {
                        case 0:
                            s.Draw(tile_0, new Rectangle(affichage.X, affichage.Y, 32, 32), Color.White);
                            break;
                        case 1:
                            s.Draw(tile_1, new Rectangle(affichage.X, affichage.Y, 32, 32), Color.White);
                            break;
                        case 2:
                            s.Draw(water, new Rectangle(affichage.X, affichage.Y, 32, 32), Color.White);
                            break;
                    }
                }
            }
        }

        public void init()
        {
            file = new StreamReader(filename);

            string line = file.ReadLine();
            short a;

            while (line != null)
            {
                for (int y = 0; y < 30; y++)
                {
                    for (int x = 0; x < line.Length; x++)
                    {
                        if (short.TryParse(line[x].ToString(), out a))
                            tailMap[x, y] = a;
                        else
                        {
                            //MOTHERFUCKER
                        }
                    }
                    line = file.ReadLine();
                }

            }
            file.Close();
        }

        public short Get(int x, int y)
        {
            return tailMap[x, y];
        }

        public short GetFropPos(int x, int y)
        {
            return tailMap[(x / 32), (y / 32)];
        }


    }
}
