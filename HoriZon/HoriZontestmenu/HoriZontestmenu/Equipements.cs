using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
