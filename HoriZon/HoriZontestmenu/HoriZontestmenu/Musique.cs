using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace HoriZon
{
    class Musique
    {
        public static void lancer_musique(ref bool lancer, Song musique)
        {
            if (!lancer)
            {
                MediaPlayer.Play(musique);
                lancer = true;
            }
        }
    }
}
