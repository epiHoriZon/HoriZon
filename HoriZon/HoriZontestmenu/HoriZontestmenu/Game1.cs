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

        SpriteBatch spriteBatch;
        SpriteFont Font_PDV;

        Personnage heros;
        Rectangle attaque;
        Animations attaque_cac;

        Stack<Munitions> munitionsLoaded;
        Stack<Munitions> munitionsShooted;
        Stack<Munitions> munitionsposs�d�es;

        List<Personnage> pileronflex;

        KeyboardEvent Keyboard; // Variable qui g�re le clavier d'apres la classe KeyboardEvent
        KeyboardState kboldstate;

        MouseState oldstate;
        Random rnd = new Random();

        #region musique

        Song musique_menu;
        Song musique_jeu;
        bool musique_jeu_principal_lancer;
        Song game_over_musique;

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


        MouseEvent mousevent;


        bool menu_actif = true;

        bool sousmenu_actif = false;
        bool langue_francais = true;
        public bool plein_ecran = false;

        bool credit_actif = false;

        bool menu_gameover = false;
        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);


            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {



            attaque_cac = new Animations(Content.Load<Texture2D>("animation_attaque"), new Rectangle(0, 0, 0, 0));

            munitionsLoaded = new Stack<Munitions>();
            munitionsShooted = new Stack<Munitions>();
            munitionsposs�d�es = new Stack<Munitions>();

            pileronflex = new List<Personnage>();
            for (int i = 0; i < 25; i++)
            {
                munitionsLoaded.Push(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical")));
                munitionsposs�d�es.Push(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical")));
            }
            for (int i = 0; i < 10; i++)
            {
                pileronflex.Add(new Personnage(Content.Load<Texture2D>("ronflex"), new Rectangle(rnd.Next(100, 1000), rnd.Next(150, 800), 32, 32), 100));
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
            #endregion

            // Initialisation des variables monstres et personnages :

            Font_PDV = Content.Load<SpriteFont>("Font_PDV");
            heros = new Personnage(Content.Load<Texture2D>("walk_iso"), new Rectangle(00, 00, 75, 101), 300);

            #region musique

            musique_jeu_principal_lancer = false;
            musique_menu = Content.Load<Song>("musique_menu");
            MediaPlayer.Play(musique_menu);

            #endregion musique

            old_keys_deplacement = Keys.U;
            etre_toucher_ou_pas = false;
            cri_montre_devient_rouge_fait = false;

            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"), Content.Load<Texture2D>("fond"));


            #region musique

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
                        }
                        else
                        {
                            Onoff_menu.desactiv();
                            plein_ecran = true;
                            graphics.IsFullScreen = true;
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
                if (mousevent.UpdateMouse() && mousevent.getmousecontainer().Intersects(new Rectangle(510, 438, 800, 600)))
                {

                    menu_gameover = false;
                    menu_actif = true;
                }
                #endregion
            }

            else
            {
                if (musique_jeu_principal_lancer == false)
                {
                    MediaPlayer.Play(musique_jeu);
                    musique_jeu_principal_lancer = true;
                }
                fond = new MenuButton(Vector2.Zero, Content.Load<Texture2D>("fondville"), Content.Load<Texture2D>("fondville"), Content.Load<Texture2D>("fondville"), Content.Load<Texture2D>("fondville"));
                if (Keyboard.Is_Back_Pressed())
                {
                    menu_actif = true;
                    MediaPlayer.Play(musique_menu);
                    musique_jeu_principal_lancer = false;
                }
                heros.deplacement();
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


                        if (mechant.position.X < heros.position.X)
                        {
                            mechant.position.X++;
                            mechant.direction = Direction.Right;
                            mechant.animationmonstre();
                            mechant.Animate(2);
                        }
                        else if (mechant.position.X > heros.position.X)
                        {
                            mechant.position.X--;
                            mechant.direction = Direction.Left;
                            mechant.animationmonstre();
                            mechant.Animate(2);
                        }
                        else
                        {


                            if (mechant.position.Y <= heros.position.Y)
                            {
                                mechant.position.Y++;
                                mechant.direction = Direction.Down;
                                mechant.animationmonstre();
                                mechant.Animate(2);
                            }
                            else if (mechant.position.Y > heros.position.Y)
                            {
                                mechant.position.Y--;
                                mechant.direction = Direction.Up;
                                mechant.animationmonstre();
                                mechant.Animate(2);
                            }

                        }
                    }

                        #endregion

                    if (heros.Points_Vie_Perso <= 0)
                    {
                        for (int i = (munitionsLoaded.Count() - 25) / 2; i < 25; i++)
                        {
                            munitionsLoaded.Push(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical")));
                            munitionsposs�d�es.Push(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical")));
                        }
                        mechant.position = mechant.positiondepart;
                        game_over_son.Play();
                        MediaPlayer.Play(game_over_musique);
                        menu_gameover = true;
                    }
                }
                #region attque CAC
                if (Keyboard.Is_A_Pressed())
                {
                    if (old_keys_deplacement != Keys.A)
                    {
                        corp_a_corp_son.Play();
                    }
                    old_keys_deplacement = Keys.A;

                    attaque_cac.Animate(5);
                    switch (heros.direction)
                    {
                        case Direction.Up: attaque = new Rectangle(heros.position.X, heros.position.Y - 25, heros.skin.Width, 25);
                            break;
                        case Direction.Down: attaque = new Rectangle(heros.position.X, heros.position.Y + heros.skin.Height, heros.skin.Width, 25);
                            break;
                        case Direction.Left: attaque = new Rectangle(heros.position.X - 25, heros.position.Y, 25, heros.skin.Height);
                            break;
                        case Direction.Right: attaque = new Rectangle(heros.position.X + heros.skin.Width, heros.position.Y, 25, heros.skin.Height);
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
                        munitionsLoaded.Pop();
                        switch (heros.direction)
                        {
                            case Direction.Up: munitionsShooted.Push(new Munitions(new Vector2(heros.position.X + 32, heros.position.Y), Content.Load<Texture2D>("tir_horizontal")));
                                break;
                            case Direction.Down: munitionsShooted.Push(new Munitions(new Vector2(heros.position.X + 32, heros.position.Y + 100), Content.Load<Texture2D>("tir_horizontal")));
                                break;
                            case Direction.Left: munitionsShooted.Push(new Munitions(new Vector2(heros.position.X, heros.position.Y + 50), Content.Load<Texture2D>("tir_vertical")));
                                break;
                            case Direction.Right: munitionsShooted.Push(new Munitions(new Vector2(heros.position.X + 72, heros.position.Y + 50), Content.Load<Texture2D>("tir_vertical")));
                                break;
                        }

                        foreach (Munitions cs in munitionsShooted)
                        {
                            switch (heros.direction)
                            {
                                case Direction.Up: cs.munitiondirection = Direction.Up;
                                    break;
                                case Direction.Down: cs.munitiondirection = Direction.Down;
                                    break;
                                case Direction.Left: cs.munitiondirection = Direction.Left;
                                    break;
                                case Direction.Right: cs.munitiondirection = Direction.Right;
                                    break;
                            }
                        }
                    }
                }
                foreach (Munitions cs in munitionsShooted)
                {


                    switch (cs.munitiondirection)
                    {
                        case Direction.Up: cs.position.Y -= 5;
                            break;
                        case Direction.Down: cs.position.Y += 5;
                            break;
                        case Direction.Left: cs.position.X -= 5;
                            break;
                        case Direction.Right: cs.position.X += 5;
                            break;
                    }

                }
                if (munitionsLoaded.Count() == 0 || Keyboard.Is_R_Pressed())
                {
                    int i = 25 - munitionsLoaded.Count();
                    while (i < 25)
                    {
                        if (munitionsposs�d�es.Count() > 0)
                        {
                            munitionsposs�d�es.Pop();
                            munitionsLoaded.Push(new Munitions(new Vector2(100, 100), Content.Load<Texture2D>("tir_vertical")));
                        }
                        i++;
                    }


                }



                #endregion
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
                heros.Drawperso(spriteBatch, 75, 101);
                if (heros.Points_Vie_Perso > 0)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("jauge_pv"), new Rectangle(10, 10, (heros.Points_Vie_Perso) / 2, 10), Color.Red);
                    spriteBatch.DrawString(Font_PDV, "Munitions:" + munitionsLoaded.Count() + "|" + munitionsposs�d�es.Count(), new Vector2(10, 450), Color.Orange);
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
                    en.Drawperso(spriteBatch, 32, 32);
                }




                if (munitionsShooted.Count() != 0)
                {
                    foreach (Munitions cs in munitionsShooted)
                    {
                        cs.DrawMunitions(spriteBatch);
                    }
                }
                if (Keyboard.Is_A_Pressed())
                {
                    attaque_cac.DrawAnimate(spriteBatch);
                }

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
