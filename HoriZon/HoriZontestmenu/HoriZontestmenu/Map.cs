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


namespace HoriZon
{
    class Map
    {
        StreamReader file;
        Texture2D tile_0;
        Texture2D tile_1;



        public Map(Texture2D tile_0, Texture2D tile_1)
        {

            this.tile_0 = tile_0;
            this.tile_1 = tile_1;
        }

        public void generate(SpriteBatch spriteBatch)
        {
            file = new StreamReader("map.txt");

            string line = file.ReadLine();

            while (line != null)
            {
                for (int j = 0; j < 15; j++)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '0')
                        {
                            spriteBatch.Draw(tile_0, new Vector2(32 * i, 32 * j), Color.White);
                        }
                        else if (line[i] == '1')
                        {
                            spriteBatch.Draw(tile_1, new Vector2(32 * i, 32 * j), Color.White);
                        }
                        Console.WriteLine(line.Length);
                    }
                    line = file.ReadLine();
                }

            }
            file.Close();
        }



    }
}
