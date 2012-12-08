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

namespace HoriZon
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region (variable skin)

        private Texture2D skin_face;
        private Texture2D skin_dos;
        private Texture2D skin_droit;
        private Texture2D skin_gauche;

        #endregion (variable skin)

        #region (variable fond)

        Texture2D pelouse;

        #endregion (variable fond)

        #region (variable position et deplacement du perso)

        Vector2 position_perso;
        Vector2 deplacement_perso;

        #endregion (variable position et deplacement du perso)

        #region (skin_perso)

        Texture2D skin_perso;

        #endregion (skin_perso)

        #region (gameplay)

        KeyboardState clavier;

        int point_de_vie;
        Vector2 position_point_de_vie;
        SpriteFont police_point_de_vie;

        #endregion (gameplay)

        #region(menu)

        bool menu_ou_pas;
        bool enter_menu_ou_pas;
        int compteur_menu;

        Texture2D menu;

        Texture2D jouer_menu;
        Texture2D options_menu;
        Texture2D credit_menu;
        Texture2D quitter_menu;

        Texture2D credit_menu_enter;

        #endregion(menu)

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            enter_menu_ou_pas = false;
            compteur_menu = 0;
            menu_ou_pas = true;

            point_de_vie = 100;
            position_point_de_vie = new Vector2(2, 3);

            position_perso = new Vector2(2, 3);
            deplacement_perso = new Vector2(0, 0);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            skin_face = Content.Load<Texture2D>("skin_face");
            skin_dos = Content.Load<Texture2D>("skin_dos");
            skin_droit = Content.Load<Texture2D>("skin_droit");
            skin_gauche = Content.Load<Texture2D>("skin_gauche");

            //initialisation
            skin_perso = Content.Load<Texture2D>("skin_dos");

            pelouse = Content.Load<Texture2D>("pelouse");

            police_point_de_vie = Content.Load<SpriteFont>("police_point_de_vie");

            jouer_menu = Content.Load<Texture2D>("jouer_menu");
            options_menu = Content.Load<Texture2D>("options_menu");
            credit_menu = Content.Load<Texture2D>("credit_menu");
            quitter_menu = Content.Load<Texture2D>("quitter_menu");

            credit_menu_enter = Content.Load<Texture2D>("credit_menu_enter");

            //initialisation
            menu = Content.Load<Texture2D>("jouer_menu");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            clavier = Keyboard.GetState();

            if (menu_ou_pas)
            {
                if (enter_menu_ou_pas)
                {
                    if (clavier.IsKeyDown(Keys.Back))
                    {
                        enter_menu_ou_pas = false;
                    }
                    else if (compteur_menu % 4 == 0)
                    {
                        enter_menu_ou_pas = false;
                        menu_ou_pas = false;
                    }
                    else if (compteur_menu % 4 == 1)
                    {
                        //en attendant le menu options
                        menu = credit_menu_enter;
                    }
                    else if (compteur_menu % 4 == 2)
                    {
                        menu = credit_menu_enter;
                    }
                    else if (compteur_menu % 4 == 3)
                    {
                        Exit();
                    }
                }
                else
                {
                    if (clavier.IsKeyDown(Keys.Up))
                    {
                        compteur_menu = compteur_menu + 1;
                    }
                    else if (clavier.IsKeyDown(Keys.Down))
                    {
                        compteur_menu = compteur_menu + 1;
                    }
                    else if (clavier.IsKeyDown(Keys.Enter))
                    {
                        enter_menu_ou_pas = true;
                    }

                    if (compteur_menu % 4 == 0)
                    {
                        menu = jouer_menu;
                    }
                    else if (compteur_menu % 4 == 1)
                    {
                        menu = options_menu;
                    }
                    else if (compteur_menu % 4 == 2)
                    {
                        menu = credit_menu;
                    }
                    else if (compteur_menu % 4 == 3)
                    {
                        menu = quitter_menu;
                    }
                }
            }

            else
            {

                if (clavier.IsKeyDown(Keys.Up))
                {
                    skin_perso = skin_dos;
                    deplacement_perso = new Vector2(0, -1);
                }
                else if (clavier.IsKeyDown(Keys.Down))
                {
                    skin_perso = skin_face;
                    deplacement_perso = new Vector2(0, 1);
                }

                else if (clavier.IsKeyDown(Keys.Right))
                {
                    skin_perso = skin_droit;
                    deplacement_perso = new Vector2(1, 0);
                }
                else if (clavier.IsKeyDown(Keys.Left))
                {
                    skin_perso = skin_gauche;
                    deplacement_perso = new Vector2(-1, 0);
                }

                position_perso = position_perso + deplacement_perso;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            #region(affichage_images)

            spriteBatch.Begin();

            // la ou on fou l'affichage des images
            if (menu_ou_pas)
            {
                spriteBatch.Draw(menu, Vector2.Zero, Color.White);
            }

            else
            {
                spriteBatch.Draw(pelouse, Vector2.Zero, Color.White);
                spriteBatch.Draw(skin_perso, position_perso, Color.White);
                spriteBatch.DrawString(police_point_de_vie, "point de vie " + point_de_vie, position_point_de_vie, Color.White);
            }

            spriteBatch.End();

            #endregion(affichage_images)

            base.Draw(gameTime);
        }
    }
}
