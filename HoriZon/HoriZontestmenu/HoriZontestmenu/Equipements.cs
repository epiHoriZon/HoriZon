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

namespace HoriZon
{

    public enum emplacement
{
        Tete,Torse,Jambe
}
    class Equipements
    {
        emplacement place;
        int bonus_attaque;
        int bonus_tir;
        int bonus_defense;


        Equipements(emplacement place, int bonus_attaque, int bonus_tir, int bonus_defense)
        {
            this.place = place;
            this.bonus_attaque = bonus_attaque;
            this.bonus_defense = bonus_defense;
            this.bonus_tir = bonus_tir;
        }



    }
}
