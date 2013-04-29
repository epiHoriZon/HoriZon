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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        Camera camera;

        SpriteBatch spriteBatch;
        SpriteFont Font_PDV;

        Personnage heros;
        Rectangle attaque;
        Animations attaque_cac;

        List<Munitions> munitionsLoaded;
        List<Munitions> munitionsShooted;
        Stack<Munitions> munitionspossédées;
        Color laser;

        List<Equipements> inventaire;
        List<Equipements> equip;
        Equipements item1;
        Equipements item2;

        Recompense coffre;
        List<Recompense> liste_coffre;
        int argent = 5000;
        int vague = 1;

        List<Personnage> pileronflex;

     

        KeyboardEvent Keyboard; // Variable qui gère le clavier d'apres la classe KeyboardEvent
        KeyboardState kboldstate;

        MouseState oldstate;
        Random rnd = new Random();

        #region musique

        Song musique_menu;
        bool musique_menu_lancer;

        Song musique_jeu;
        bool musique_jeu_principal_lancer;

        Song game_over_musique;
        bool musique_game_over_lancer;

        #endregion musique

        #region Effets sonores

        SoundEffect tir_son;
        public static SoundEffect deplacement_robot_son;
        SoundEffect game_over_son;
        SoundEffect corp_a_corp_son;
        SoundEffect etre_toucher_son;
        bool etre_toucher_ou_pas;
        public static SoundEffect cri_montre_devient_rouge;
        public static bool cri_montre_devient_rouge_fait;

        #endregion Effets sonores

        public static Keys old_keys_deplacement;


        #region variables menu
        MenuButton fond;
        MenuButton jouer_menu;
        MenuButton options_menu;
        MenuButton credit_menu;
        MenuButton quit_menu;

        MenuButton langue_menu;
        MenuButton angl_menu;
        MenuButton PE_menu;
        MenuButton Onoff_menu;
        MenuButton Retour;

        MenuButton mag1, mag2, mag3, mag4;

        Map map;

        MouseEvent mousevent;


        bool menu_actif = true;

        bool sousmenu_actif = false;
        bool langue_francais = true;
        public bool plein_ecran = false;

        bool credit_actif = false;
        bool inventaire_actif = false;
        bool menu_gameover = false;


        bool magasin_actif = false;
        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            
            coffre = new Recompense(Content.Load<Texture2D>("coffre_ferme"), Content.Load<Texture2D>("coffre_ouvert"), new Rectangle(rnd.Next(0, 750), rnd.Next(0, 450), 25, 25));
            liste_coffre = new List<Recompense>();

            attaque_cac = new Animations(Content.Load<Texture2D>("animation_attaque"), new Rectangle(0, 0, 0, 0));
            map = new Map(Content.Load<Texture2D>("tile0"), Content.Load<Texture2D>("tile1"), Content.Load<Texture2D>("tile_water"), "map.txt");
            munitionsLoaded = new List<Munitions>();
            munitionsShooted = new List<Munitions>();
            munitionspossédées = new Stack<Munitions>();

            map.init();
            pileronflex = new List<Personnage>();
            for (int i = 0; i < 25; i++)
            {
                munitionsLoaded.Add(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical"),laser));
                munitionspossédées.Push(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical"), laser));
            }
            for (int i = 0; i <= 3; i++)
            {
                pileronflex.Add(new Personnage(Content.Load<Texture2D>("ronflex"), new Rectangle(rnd.Next(100, 800), rnd.Next(150, 500), 32, 32), 10, 1, 0, 0, null, null, map));
                pileronflex[i].numero = 2;
            }
            for (int i = 4; i < 7; i++)
            {
                pileronflex.Add(new Personnage(Content.Load<Texture2D>("cyborgcostar"), new Rectangle(rnd.Next(100, 800), rnd.Next(150, 500), 32, 32), 10, 2, 0, 0, null, null, map));
                pileronflex[i].numero = 4;
            }

            
            Keyboard = new KeyboardEvent();
            this.IsMouseVisible = true;
            #region variables menu
            mousevent = new MouseEvent();
            jouer_menu = new MenuButton(new Vector2(50, 425), Content.Load<Texture2D>("jouer"), Content.Load<Texture2D>("joueractiv"), Content.Load<Texture2D>("play"), Content.Load<Texture2D>("playactiv"));
            options_menu = new MenuButton(new Vector2(250, 425), Content.Load<Texture2D>("option"), Content.Load<Texture2D>("optionactiv"), Content.Load<Texture2D>("option"), Content.Load<Texture2D>("optionactiv"));
            credit_menu = new MenuButton(new Vector2(450, 425), Content.Load<Texture2D>("credits"), Content.Load<Texture2D>("creditactiv"), Content.Load<Texture2D>("credits"), Content.Load<Texture2D>("creditactiv"));
            quit_menu = new MenuButton(new Vector2(650, 425), Content.Load<Texture2D>("quit"), Content.Load<Texture2D>("quitactiv"), Content.Load<Texture2D>("exit"), Content.Load<Texture2D>("exitactiv"));

            langue_menu = new MenuButton(new Vector2(275, 425), Content.Load<Texture2D>("langue"), Content.Load<Texture2D>("langueactiv"), Content.Load<Texture2D>("language"), Content.Load<Texture2D>("languageactiv"));
            angl_menu = new MenuButton(new Vector2(400, 425), Content.Load<Texture2D>("fra"), Content.Load<Texture2D>("fraactiv"), Content.Load<Texture2D>("english"), Content.Load<Texture2D>("englishactiv"));
            PE_menu = new MenuButton(new Vector2(20, 425), Content.Load<Texture2D>("PE"), Content.Load<Texture2D>("PEactiv"), Content.Load<Texture2D>("FS"), Content.Load<Texture2D>("FSactiv"));
            Onoff_menu = new MenuButton(new Vector2(150, 425), Content.Load<Texture2D>("offactiv"), Content.Load<Texture2D>("onactiv"), Content.Load<Texture2D>("offactiv"), Content.Load<Texture2D>("onactiv"));
            Retour = new MenuButton(new Vector2(700, 425), Content.Load<Texture2D>("bouton_retour"), Content.Load<Texture2D>("bouton_retour"), Content.Load<Texture2D>("back"), Content.Load<Texture2D>("backactiv"));

            mag1 = new MenuButton(new Vector2(400, 130), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"));
            mag2 = new MenuButton(new Vector2(400, 230), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"));
            mag3  =new MenuButton(new Vector2(400, 330 ), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"));
            mag4 = new MenuButton(new Vector2(400, 430), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"), Content.Load<Texture2D>("tuto"));
            
            
            
            #endregion

            // Initialisation des variables monstres et personnages :

            laser = Color.Red;
            inventaire = new List<Equipements>();
            equip = new List<Equipements>();

            item1 = new Equipements("Main", Content.Load<Texture2D>("item1"), 50, 50, 50);
            item2 = new Equipements("Tete", Content.Load<Texture2D>("item1"), 50, 50, 50);

            inventaire.Add(item1);
            inventaire.Add(item2);
            inventaire[0].stack(inventaire);
            heros = new Personnage(Content.Load<Texture2D>("walk_iso"), new Rectangle(00, 00, 75, 101), 300, 5, 5, 5, inventaire, equip, map);

            Font_PDV = Content.Load<SpriteFont>("Font_PDV");
            heros = new Personnage(Content.Load<Texture2D>("heros"), new Rectangle(200, 200, 24, 32), 350,5,5,5, inventaire, equip ,map);
            camera = new Camera(new Vector2(heros.position.X, heros.position.Y), Content.Load<Texture2D>("map"));
            heros.Inventaire();

            #region musique

            musique_jeu_principal_lancer = false;
            musique_menu_lancer = false;
            old_keys_deplacement = Keys.U;
            etre_toucher_ou_pas = false;
            cri_montre_devient_rouge_fait = false;
            #endregion musique
            base.Initialize();
        }


        protected override void LoadContent()
        {
            

            spriteBatch = new SpriteBatch(GraphicsDevice);

            fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"));


            #region musique

            musique_menu = Content.Load<Song>("musique_menu");
            game_over_musique = Content.Load<Song>("game_over_musique");
            musique_jeu = Content.Load<Song>("musique_jeu");

            #endregion musique

            #region Effets sonores

            tir_son = Content.Load<SoundEffect>("tir_son");
            deplacement_robot_son = Content.Load<SoundEffect>("deplacement_robot_son");
            game_over_son = Content.Load<SoundEffect>("game_over_son");
            corp_a_corp_son = Content.Load<SoundEffect>("corp_a_corp_son");
            etre_toucher_son = Content.Load<SoundEffect>("etre_toucher_son");
            cri_montre_devient_rouge = Content.Load<SoundEffect>("cri_montre_devient_rouge");

            #endregion Effets sonores

        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
           
            mousevent.old_mouse_state = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
          
            oldstate = mousevent.ButtonPressed;
            kboldstate = Keyboard.ButtonPressed;


            if (menu_actif)
            {
                Musique.lancer_musique(ref musique_menu_lancer, musique_menu);              
                musique_jeu_principal_lancer = false;
                musique_game_over_lancer = false;

                fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"));
                #region menu
                #region menu principal

                if (mousevent.getmousecontainer().Intersects(jouer_menu.getcontainer()))
                {
                    jouer_menu.activ();

                    options_menu.desactiv(); credit_menu.desactiv(); quit_menu.desactiv();

                    if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                    {

                        menu_actif = false;
                    }

                }
                if (mousevent.getmousecontainer().Intersects(options_menu.getcontainer()))
                {
                    options_menu.activ();
                    jouer_menu.desactiv(); credit_menu.desactiv(); quit_menu.desactiv();
                    if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                    {

                        fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"));
                        menu_actif = false;
                        sousmenu_actif = true;



                    }
                }
                if (mousevent.getmousecontainer().Intersects(credit_menu.getcontainer()))
                {
                    credit_menu.activ();
                    jouer_menu.desactiv(); options_menu.desactiv(); quit_menu.desactiv();
                    if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                    {
                        menu_actif = false;
                        sousmenu_actif = false;
                        credit_actif = true;
                    }
                }
                if (mousevent.getmousecontainer().Intersects(quit_menu.getcontainer()))
                {
                    quit_menu.activ();
                    jouer_menu.desactiv(); credit_menu.desactiv(); options_menu.desactiv();
                    if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                    {
                        Exit();
                    }
                }
                #endregion
            }
            else if (sousmenu_actif)
            {

                if (mousevent.getmousecontainer().Intersects(angl_menu.getcontainer()) || mousevent.getmousecontainer().Intersects(langue_menu.getcontainer()))
                {

                    langue_menu.activ();
                    if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                    {
                        if (langue_francais)
                        {
                            angl_menu.desactiv();
                            langue_francais = false;

                            jouer_menu.anglais_on = false;
                            options_menu.anglais_on = false;
                            credit_menu.anglais_on = false;
                            quit_menu.anglais_on = false;
                            langue_menu.anglais_on = false;
                            angl_menu.anglais_on = false;
                            PE_menu.anglais_on = false;
                            Onoff_menu.anglais_on = false;
                            Retour.anglais_on = false;
                          

                        }
                        else
                        {

                            angl_menu.activ();
                            langue_francais = true;

                            jouer_menu.anglais_on = true;
                            options_menu.anglais_on = true;
                            credit_menu.anglais_on = true;
                            quit_menu.anglais_on = true;
                            langue_menu.anglais_on = true;
                            angl_menu.anglais_on = true;
                            PE_menu.anglais_on = true;
                            Onoff_menu.anglais_on = true;
                            Retour.anglais_on = true;
                          
                        }
                    }

                }
                else
                {
                    langue_menu.desactiv();
                }
                if (mousevent.getmousecontainer().Intersects(Onoff_menu.getcontainer()) || mousevent.getmousecontainer().Intersects(PE_menu.getcontainer()))
                {
                    PE_menu.activ();
                    if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                    {
                        if (plein_ecran)
                        {
                            Onoff_menu.activ();
                            plein_ecran = false;
                            graphics.IsFullScreen = false;
                            graphics.ApplyChanges();
                           
                        }
                        else
                        {
                            Onoff_menu.desactiv();
                            plein_ecran = true;
                            graphics.IsFullScreen = true;
                            graphics.ApplyChanges();
                        }
                    }
                }
                else
                {
                    PE_menu.desactiv();
                }
                if (mousevent.getmousecontainer().Intersects(Retour.getcontainer()))
                {
                    Retour.activ();
                    if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                    {
                        sousmenu_actif = false;
                        menu_actif = true;
                    }
                }
                else
                {
                    Retour.desactiv();
                }
                mousevent.old_mouse_state = mousevent.ButtonPressed;
            }
            else if (credit_actif)
            {
                fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"));
                if (mousevent.getmousecontainer().Intersects(Retour.getcontainer()))
                {
                    Retour.activ();
                    if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                    {
                        credit_actif = false;
                        menu_actif = true;
                    }
                }
                else
                {
                    Retour.desactiv();
                }



            }
          
            else if (menu_gameover)
            {

                fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("fond_gameover"), Content.Load<Texture2D>("fond_gameover"), Content.Load<Texture2D>("fond_gameover"), Content.Load<Texture2D>("fond_gameover"));
                if (mousevent.UpdateMouse() && mousevent.getmousecontainer().Intersects(new Rectangle(0, 0, 800, 600)))
                {
                    menu_gameover = false;
                    menu_actif = true;
                }
                #endregion
            }
            else if (inventaire_actif)
            {
                for (int i = 0; i < inventaire.Count; i++)
                {
                    if (mousevent.getmousecontainer().Intersects(inventaire[i].emplacementInventaire))
                    {
                        if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                        {
                            equip.Add(inventaire[i]);
                            // equip[equip.Count - 1].stack(equip);
                            inventaire.RemoveAt(i);
                        }
                    }
                }

                for (int i = 0; i < equip.Count; i++)
                {
                    if (mousevent.getmousecontainer().Intersects(equip[i].emplacementEquip))
                    {
                        if (mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released)
                        {
                            inventaire.Add(equip[i]);
                            // inventaire[equip.Count - 1].stack(inventaire, true);
                            equip.RemoveAt(i);
                        }
                    }
                }

                if (Keyboard.Is_I_Pressed() && kboldstate.IsKeyUp(Keys.I))
                {
                    inventaire_actif = false;
                }
                heros.Inventaire();
            }
            else if (magasin_actif)
            {
                fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("magasin"), Content.Load<Texture2D>("magasin"), Content.Load<Texture2D>("magasin"), Content.Load<Texture2D>("magasin"));
                if (mousevent.getmousecontainer().Intersects(mag1.getcontainer()) && mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released && argent >= 50 )
                {
                    heros.bonus_attaque++;
                    argent -= 50;
                }
                if (mousevent.getmousecontainer().Intersects(mag2.getcontainer()) && mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released && argent >= 40 )
                {
                    heros.bonus_defense++;
                    argent -= 40;
                }
                if (mousevent.getmousecontainer().Intersects(mag3.getcontainer()) && mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released && argent >= 2000)
                {
                    argent -= 2000;
                    // voir pour le changement de musique
                }
                if (mousevent.getmousecontainer().Intersects(mag4.getcontainer()) && mousevent.UpdateMouse() && oldstate.LeftButton == ButtonState.Released && argent >= 100)
                {
                    laser = Color.GreenYellow;
                    argent -= 100;
                }
                if(Keyboard.Is_M_Pressed() && kboldstate.IsKeyUp(Keys.M))
                {
                    magasin_actif = false;
                }
            }
            else
            {
                Musique.lancer_musique(ref musique_jeu_principal_lancer, musique_jeu);
                musique_menu_lancer = false;
                musique_game_over_lancer = false;

                camera.Update(heros.position);
                fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("fondville"), Content.Load<Texture2D>("fondville"), Content.Load<Texture2D>("fondville"), Content.Load<Texture2D>("fondville"));
                if (Keyboard.Is_Back_Pressed())
                {
                    menu_actif = true;
                }
                if (Keyboard.Is_I_Pressed() && kboldstate.IsKeyUp(Keys.I))
                {
                    inventaire_actif = true;
                }
                if(Keyboard.Is_M_Pressed() && kboldstate.IsKeyUp(Keys.M))

                {
                    magasin_actif = true;
                   
                }
                heros.deplacement();
                if (Keyboard.Is_Space_Pressed())
                {
                    heros.speed = 4;
                    heros.AnimationSpeed = 7;
                }
                else
                {
                    heros.speed = 2;
                    heros.AnimationSpeed = 5;
                }
                for (int i = 0; i < pileronflex.Count; i++)
                {
                    if (pileronflex[i].Points_Vie_Perso <= 0)
                        pileronflex.RemoveAt(i);
                }
                foreach (Personnage mechant in pileronflex)
                {
                    if (mechant.position.Intersects(heros.position))
                    {
                        heros.Points_Vie_Perso--;
                        if (etre_toucher_ou_pas == false)
                        {
                            etre_toucher_son.Play();
                            etre_toucher_ou_pas = true;
                        }

                    }
                    else
                    {
                        #region deplacement enemi (revoir l'IA )
                        if (mechant.position.Intersects(new Rectangle(heros.position.X - 200, heros.position.Y - 200, heros.position.X + heros.skin.Width + 200, heros.position.Y + heros.skin.Height + 200)))
                        {
                            
                            if (mechant.position.X < heros.position.X)
                            {

                                if (map.GetFropPos(mechant.position.X + 32, mechant.position.Y) == 0 && map.GetFropPos(mechant.position.X + 32, mechant.position.Y  + 30) == 0)
                                    mechant.position.X++;
                                mechant.direction = Direction.Right;
                                mechant.animationmonstre(mechant.numero);
                                mechant.Animate(mechant.numero); 
                            }
                            else if (mechant.position.X > heros.position.X)
                            {
                                if (map.GetFropPos(mechant.position.X, mechant.position.Y) == 0 && map.GetFropPos(mechant.position.X, mechant.position.Y + 30) == 0)
                                    mechant.position.X --;
                                mechant.direction = Direction.Left;
                                mechant.animationmonstre(mechant.numero);
                                mechant.Animate(mechant.numero);
                            }
                            else
                            {
                                if (mechant.position.Y <= heros.position.Y)
                                {
                                    if (map.GetFropPos(mechant.position.X, mechant.position.Y + 32) == 0 && map.GetFropPos(mechant.position.X + 32, mechant.position.Y + 32) == 0)
                                        mechant.position.Y++;
                                    
                                    mechant.direction = Direction.Down;
                                    mechant.animationmonstre(mechant.numero);
                                    mechant.Animate(mechant.numero);
                                }
                                else if (mechant.position.Y > heros.position.Y)
                                {
                                    if (map.GetFropPos(mechant.position.X, mechant.position.Y -1) == 0)
                                        mechant.position.Y --;
                                    mechant.direction = Direction.Up;
                                    mechant.animationmonstre(mechant.numero);
                                    mechant.Animate(mechant.numero);
                                }

                            }
                        }
                       
                    }

                        #endregion
                    
                    if (heros.Points_Vie_Perso <= 0)
                    {
                        for (int i = (munitionsLoaded.Count() - 25) / 2; i < 25; i++)
                        {
                            munitionsLoaded.Add(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical"), laser));
                            munitionspossédées.Push(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical"), laser));
                        }
                        mechant.position = mechant.positiondepart;
                        game_over_son.Play();

                        Musique.lancer_musique(ref musique_game_over_lancer, game_over_musique);
                        musique_menu_lancer = false;
                        musique_jeu_principal_lancer = false;

                        menu_gameover = true;
                    }
                }
                #region attque CAC
                if (Keyboard.Is_A_Pressed() && kboldstate.IsKeyUp(Keys.A))
                {
                    corp_a_corp_son.Play();
                    attaque_cac.Animate(5);
                    switch (heros.direction)
                    {
                        case Direction.Up: attaque = new Rectangle(heros.position.X, heros.position.Y - 25, 24, 25);
                            break;
                        case Direction.Down: attaque = new Rectangle(heros.position.X, heros.position.Y + 56, 24, 25);
                            break;
                        case Direction.Left: attaque = new Rectangle(heros.position.X - 25, heros.position.Y, 25, 32);
                            break;
                        case Direction.Right: attaque = new Rectangle(heros.position.X + 49, heros.position.Y, 25, 32);
                            break;
                    }
                }
                else
                {
                    attaque = new Rectangle();
                }

                foreach (Personnage mechant in pileronflex)
                {
                    if (attaque.Intersects(mechant.position))
                    {

                        mechant.Points_Vie_Perso -= 5;
                    }
                }
                #endregion
                #region gestion munitions et tirs
                if (munitionsLoaded.Count() > 0 && heros.Points_Vie_Perso > 0)
                {
                    if (Keyboard.Is_E_Pressed() && kboldstate.IsKeyUp(Keys.E))
                    {
                        tir_son.Play();
                        munitionsLoaded.RemoveAt(munitionsLoaded.Count -1);
                        switch (heros.direction)
                        {
                            case Direction.Up: munitionsShooted.Add(new Munitions(new Vector2(heros.position.X + 12, heros.position.Y), Content.Load<Texture2D>("tir_horizontal"),laser));
                                break;
                            case Direction.Down: munitionsShooted.Add(new Munitions(new Vector2(heros.position.X + 12, heros.position.Y + 32), Content.Load<Texture2D>("tir_horizontal"), laser));
                                break;
                            case Direction.Left: munitionsShooted.Add(new Munitions(new Vector2(heros.position.X, heros.position.Y + 16), Content.Load<Texture2D>("tir_vertical"), laser));
                                break;
                            case Direction.Right: munitionsShooted.Add(new Munitions(new Vector2(heros.position.X + 24, heros.position.Y + 16), Content.Load<Texture2D>("tir_vertical"),laser));
                                break;
                        }
                        switch (heros.direction)
                        {
                            case Direction.Up: munitionsShooted[munitionsShooted.Count - 1].munitiondirection = Direction.Up;
                                break;
                            case Direction.Down: munitionsShooted[munitionsShooted.Count - 1].munitiondirection = Direction.Down;
                                break;
                            case Direction.Left: munitionsShooted[munitionsShooted.Count - 1].munitiondirection = Direction.Left;
                                break;
                            case Direction.Right: munitionsShooted[munitionsShooted.Count - 1].munitiondirection = Direction.Right;
                                break;
                        }

                    }
                }
                foreach (Munitions cs in munitionsShooted)
                {
                    
                    bool old_intersect_munition_ennemi ;
                    switch (cs.munitiondirection)
                    {
                        case Direction.Up: cs.position.Y -= 10;
                            break;
                        case Direction.Down: cs.position.Y += 10;
                            break;
                        case Direction.Left: cs.position.X -= 10;
                            break;
                        case Direction.Right: cs.position.X += 10;
                            break;
                    }
                    for (int i = 0; i < pileronflex.Count; i++)
                    {
                        old_intersect_munition_ennemi = pileronflex[i].position.Intersects(cs.container);
                        if (pileronflex[i].position.Intersects(cs.container))
                        {
                            pileronflex[i].Points_Vie_Perso -= 50;
                        }
                    }
              

                }

                if (munitionsLoaded.Count() !=0 || Keyboard.Is_R_Pressed())
                {
                    int i = 25 - munitionsLoaded.Count();
                    while (i < 25)
                    {
                        if (munitionspossédées.Count() > 0)
                        {
                            munitionspossédées.Pop();
                            munitionsLoaded.Add(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical"), laser));
                        }
                        i++;
                    }


                }



                #endregion
              
                    if (pileronflex.Count() == 0 && vague == 1)
                    {
                        int rnd01, rnd02;

                        rnd01 = rnd.Next(0, 750);
                        rnd02 = rnd.Next(0, 450);
                        while (map.GetFropPos(rnd01,rnd02) != 0)
                        {
                            rnd01 = rnd.Next(0,750);
                            rnd02 = rnd.Next(0,450);

                        }
                        liste_coffre.Add(new Recompense(Content.Load<Texture2D>("coffre_ferme"), Content.Load<Texture2D>("coffre_ouvert"), new Rectangle(rnd.Next(0, 750), rnd.Next(0, 450), 25, 25)));
                        vague--;
                    }

                for (int i = 0; i < liste_coffre.Count; i++)
                {
                    if (liste_coffre[i].obtention_recompense(heros) && !liste_coffre[i].ouvert)
                    {
                        inventaire.Add(new Equipements("Main", Content.Load<Texture2D>("item1"), 50, 50, 50));
                        inventaire[inventaire.Count - 1].stack(inventaire);
                        argent += rnd.Next(0, 100);
                        liste_coffre[i].ouverture();
                        heros.Points_Vie_Perso += 50;
                        vague++;
                        for (int j = 0; j < 6; j++)
                        {
                            pileronflex.Add(new Personnage(Content.Load<Texture2D>("ronflex"), new Rectangle(rnd.Next(100, 800), rnd.Next(150, 500), 32, 32), 10, 1, 0, 0, null, null,map));
                            pileronflex[j].numero = 2;
                        }
                    }
                    
                }
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();


            fond.DrawFond(spriteBatch, 800, 600);

            if (menu_actif)
            {
                jouer_menu.DrawButton(spriteBatch);
                options_menu.DrawButton(spriteBatch);
                credit_menu.DrawButton(spriteBatch);
                quit_menu.DrawButton(spriteBatch);
            }
            else if (sousmenu_actif)
            {
                langue_menu.DrawButton(spriteBatch);
                angl_menu.DrawButton(spriteBatch);

                Onoff_menu.DrawButton(spriteBatch);
                PE_menu.DrawButton(spriteBatch);
                Retour.DrawButton(spriteBatch);
            }
            else if (credit_actif)
            {
                Retour.DrawButton(spriteBatch);
            }
            
            else if (menu_gameover)
            {

            }
            else
            {

                map.draw(spriteBatch, heros, camera);
                //camera.Draw(spriteBatch, 800, 600);
                camera.DrawHeros(spriteBatch, heros);
                
                if (heros.Points_Vie_Perso > 0)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("jauge_pv"), new Rectangle(12, 25, (heros.Points_Vie_Perso) / 2, 15), Color.Red);
                    spriteBatch.Draw(Content.Load<Texture2D>("jauge_pv"), new Rectangle(12, 56, (munitionsLoaded.Count) * 3, 20), Color.Orange);
                    if (heros.Points_Vie_Perso > 350)
                    {
                        heros.Points_Vie_Perso = 330;
                    }
                    
                }
                else
                {
                    heros.position = heros.positiondepart;
                    heros.Points_Vie_Perso = 300;
                    foreach (Personnage mechant in pileronflex)
                    {
                        mechant.Points_Vie_Perso = 100;
                    }

                }
                foreach (Personnage en in pileronflex)
                {
                    en.affichage = en.position; 
                    if (heros.position.X > camera.CameraWidth / 2)
                        en.affichage.X = camera.CameraWidth / 2 - heros.position.X + en.position.X; ;
                    if (heros.position.Y > camera.CameraHeight / 2)
                        en.affichage.Y = camera.CameraHeight / 2 - heros.position.Y + en.position.Y; 
                    if (en.numero == 2)
                    { 
                        en.Drawperso(spriteBatch, 32, 32);
                    }
                    else if (en.numero == 4)
                    {
                        en.Drawperso(spriteBatch, 64, 80);
                    } 

                }




                if (munitionsShooted.Count() != 0)
                {
                    foreach (Munitions cs in munitionsShooted)
                    {
                        cs.affichage.X = (int)cs.position.X;
                        cs.affichage.Y = (int)cs.position.Y;
                        if (heros.position.X > camera.CameraWidth / 2)
                            cs.affichage.X = camera.CameraWidth / 2 - heros.position.X + (int)cs.position.X ;
                        if (heros.position.Y > camera.CameraHeight / 2)
                            cs.affichage.Y = camera.CameraHeight / 2 - heros.position.Y + (int)cs.position.Y;
                        cs.DrawMunitions(spriteBatch);
                    }
                }
                if (liste_coffre.Count != 0)
                {
                    foreach (Recompense coffre in liste_coffre)
                    {
                        coffre.affichage.X = coffre.container.X;
                        coffre.affichage.X = coffre.container.X;
                        if (heros.position.X > camera.CameraWidth / 2)
                            coffre.affichage.X = camera.CameraWidth / 2 - heros.position.X + coffre.container.X; ;
                        if (heros.position.Y > camera.CameraHeight / 2)
                            coffre.affichage.Y = camera.CameraHeight / 2 - heros.position.Y + coffre.container.Y; 
                        coffre.DrawRecompense(spriteBatch);

                    }
                }
                if (Keyboard.Is_A_Pressed())
                {
                    attaque_cac.DrawAnimate(spriteBatch);
                }
                spriteBatch.Draw(Content.Load<Texture2D>("HUD"), new Rectangle(0, 0, 200, 150), Color.White);
                spriteBatch.DrawString(Font_PDV, "$" + argent, new Vector2(40, 90), Color.Yellow);
            }

            if (inventaire_actif)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("inventaire"), new Rectangle(camera.CameraWidth / 2 - 250, camera.CameraHeight / 2 - 159, 500, 318), Color.White);

                for (int i = 0; i < inventaire.Count; i++)
                {
                    spriteBatch.Draw(inventaire[i].item, inventaire[i].emplacementInventaire, Color.White);
                }

                for (int i = 0; i < equip.Count; i++)
                {
                    spriteBatch.Draw(equip[i].item, equip[i].emplacementEquip, Color.White);
                }
             
            }
            if (magasin_actif)
            {
                fond.DrawFond(spriteBatch, 800, 600);
                mag1.DrawButton(spriteBatch);
                mag2.DrawButton(spriteBatch);
                mag3.DrawButton(spriteBatch);
                mag4.DrawButton(spriteBatch);
                spriteBatch.DrawString(Font_PDV, "$" + argent, new Vector2(40, 90), Color.Yellow);
              
            }

            spriteBatch.End();
            heros.affichage = heros.position;
            base.Draw(gameTime);
            
        }
    }
}
