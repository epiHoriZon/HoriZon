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


    class Equipements
    {
        public string place;
        public int bonus_attaque;
        public int bonus_tir;
        public int bonus_defense;
        public Rectangle emplacementInventaire;
        public Rectangle emplacementEquip;
        public Texture2D item;

        public Equipements(string place, Texture2D item, int bonus_attaque, int bonus_tir, int bonus_defense)
        {
            this.place = place;
            this.item = item;
            this.bonus_attaque = bonus_attaque;
            this.bonus_defense = bonus_defense;
            this.bonus_tir = bonus_tir;
            this.emplacementEquip = new Rectangle(325, 160, 34, 36);
            this.emplacementInventaire = new Rectangle(400, 200, 34, 36);
        }

        public void stack(List<Equipements> inventaire)
        {
            this.emplacementInventaire = new Rectangle(400 + (inventaire.Count -1) * 46, 200, 34, 36);

             switch (place)
                {
                    case "Tete": this.emplacementEquip = new Rectangle(325, 160, 34, 36);
                        break;
                    case "Corps": this.emplacementEquip = new Rectangle(325, 190, 34, 36);
                        break;
                    case "Main": this.emplacementEquip = new Rectangle(335, 245, 34, 36);
                        break;
                    case "Pieds": this.emplacementEquip = new Rectangle(325, 250, 34, 36);
                        break; 
                } 
               
        }
    }
}

