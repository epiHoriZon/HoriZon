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


namespace HoriZontestmenu
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        Personnage heros;
        Rectangle attaque;

        Personnage mechant;

        KeyboardEvent Keyboard; // Variable qui gère le clavier d'apres la classe KeyboardEvent

        Random rnd = new Random();

        #region variables menu
        Texture2D fond;
        MenuButton jouer_menu;
        MenuButton options_menu;
        MenuButton credit_menu;
        MenuButton quit_menu;

        MenuButton langue_menu;
        MenuButton fra_menu;
        MenuButton angl_menu;
        MenuButton PE_menu;
        MenuButton Onoff_menu;
        MenuButton Retour;

        MouseEvent mousevent;


        bool menu_actif = true;

        bool sousmenu_actif = false;
        bool langue_francais = true;
        bool plein_ecran = false;

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

            mechant =(new Personnage(Content.Load<Texture2D>("ronflex"), new Rectangle(rnd.Next(0, 800), 0, 32, 32), 100));


            Keyboard = new KeyboardEvent();
            this.IsMouseVisible = true;
            #region variables menu
            mousevent = new MouseEvent();
            jouer_menu = new MenuButton(new Vector2(300, 25), Content.Load<Texture2D>("jouer"), Content.Load<Texture2D>("joueractiv"));
            options_menu = new MenuButton(new Vector2(300, 80), Content.Load<Texture2D>("option"), Content.Load<Texture2D>("optionactiv"));
            credit_menu = new MenuButton(new Vector2(310, 135), Content.Load<Texture2D>("credits"), Content.Load<Texture2D>("creditactiv"));
            quit_menu = new MenuButton(new Vector2(310, 190), Content.Load<Texture2D>("quit"), Content.Load<Texture2D>("quitactiv"));

            langue_menu = new MenuButton(new Vector2(50, 50), Content.Load<Texture2D>("langue"), Content.Load<Texture2D>("langueactiv"));
            fra_menu = new MenuButton(new Vector2(300, 50), Content.Load<Texture2D>("fraactiv"), Content.Load<Texture2D>("fra"));
            angl_menu = new MenuButton(new Vector2(500, 48), Content.Load<Texture2D>("angl"), Content.Load<Texture2D>("anglactiv"));
            PE_menu = new MenuButton(new Vector2(50, 200), Content.Load<Texture2D>("PE"), Content.Load<Texture2D>("PEactiv"));
            Onoff_menu = new MenuButton(new Vector2(300, 200), Content.Load<Texture2D>("offactiv"), Content.Load<Texture2D>("onactiv"));
            Retour = new MenuButton(new Vector2(600, 400), Content.Load<Texture2D>("bouton_retour"), Content.Load<Texture2D>("bouton_retour"));
            #endregion
            // Initialisation des variables monstres et personnages :
            heros = new Personnage(Content.Load<Texture2D>("walk_iso"), new Rectangle(200, 200, 75, 101), 300);
       
            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            fond = Content.Load<Texture2D>("fond");

        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            mousevent.old_mouse_state = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            if (menu_actif)
            {
                fond = Content.Load<Texture2D>("fond");
                #region menu
                #region menu principal

                if (mousevent.getmousecontainer().Intersects(jouer_menu.getcontainer()))
                {
                    jouer_menu.activ();

                    options_menu.desactiv(); credit_menu.desactiv(); quit_menu.desactiv();

                    if (mousevent.UpdateMouse())
                    {

                        menu_actif = false;
                    }

                }
                if (mousevent.getmousecontainer().Intersects(options_menu.getcontainer()))
                {
                    options_menu.activ();
                    jouer_menu.desactiv(); credit_menu.desactiv(); quit_menu.desactiv();
                    if (mousevent.UpdateMouse())
                    {

                        fond = Content.Load<Texture2D>("fond");
                        menu_actif = false;
                        sousmenu_actif = true;


                    }
                }
                if (mousevent.getmousecontainer().Intersects(credit_menu.getcontainer()))
                {
                    credit_menu.activ();
                    jouer_menu.desactiv(); options_menu.desactiv(); quit_menu.desactiv();
                    if (mousevent.UpdateMouse())
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
                    if (mousevent.UpdateMouse())
                    {
                        Exit();
                    }
                }
                #endregion
            }
            else if (sousmenu_actif)
            {

                if (mousevent.getmousecontainer().Intersects(angl_menu.getcontainer()) || mousevent.getmousecontainer().Intersects(fra_menu.getcontainer()) || mousevent.getmousecontainer().Intersects(langue_menu.getcontainer()))
                {

                    langue_menu.activ();
                    if (mousevent.UpdateMouse() && mousevent.old_mouse_state.LeftButton == ButtonState.Released)
                    {
                        if (langue_francais)
                        {
                            fra_menu.desactiv();
                            angl_menu.desactiv();
                            langue_francais = false;


                        }
                        else
                        {
                            fra_menu.activ();
                            angl_menu.activ();
                            langue_francais = true;
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
                    if (mousevent.UpdateMouse() && !(mousevent.old_mouse_state.LeftButton == ButtonState.Pressed))
                    {
                        if (plein_ecran)
                        {
                            Onoff_menu.activ();
                            plein_ecran = false;
                        }
                        else
                        {
                            Onoff_menu.desactiv();
                            plein_ecran = true;
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
                    if (mousevent.UpdateMouse())
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
                fond = Content.Load<Texture2D>("bt_credit");
                if (mousevent.getmousecontainer().Intersects(Retour.getcontainer()))
                {
                    Retour.activ();
                    if (mousevent.UpdateMouse())
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
                fond = Content.Load<Texture2D>("fond_gameover");
                if (mousevent.UpdateMouse() && mousevent.getmousecontainer().Intersects(new Rectangle(510, 438, 800, 600)))
                {

                    menu_gameover = false;
                    menu_actif = true;
                }
                #endregion
            }

            else
            {
                fond = Content.Load<Texture2D>("fondville");
                if (Keyboard.Is_Back_Pressed())
                {
                    menu_actif = true;
                }


                heros.deplacement();
               

           
         

                    if (mechant.position.Intersects(heros.position))
                    {
                        heros.Points_Vie_Perso--;

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
                        mechant.position = mechant.positiondepart;
                        menu_gameover = true;
                    }

                    if (Keyboard.Is_A_Pressed())
                    {
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
                        attaque = new Rectangle(0, 0, 0, 0);
                    }


                    if (attaque.Intersects(mechant.position))
                    {
                        Console.Write("attaque OK");
                        mechant.Points_Vie_Perso -= 5;
                    }


                }

            
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();


            spriteBatch.Draw(fond, Vector2.Zero, Color.White);

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
                fra_menu.DrawButton(spriteBatch);
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
                    spriteBatch.Draw(Content.Load<Texture2D>("jauge_pv"), new Rectangle(10, 10, (heros.Points_Vie_Perso)/2, 10), Color.Red);
                }
                else
                {

                    heros.position = heros.positiondepart;
                    heros.Points_Vie_Perso = 300;
                    mechant.Points_Vie_Perso = 100;
                }
               

                  
                        mechant.Drawperso(spriteBatch, 32, 32);
                    

                
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
