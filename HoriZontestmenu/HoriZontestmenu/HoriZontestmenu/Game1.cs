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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        
        SpriteBatch spriteBatch;

        Texture2D fond;
        MenuButton jouer_menu;
        MenuButton options_menu;
        MenuButton credit_menu;
        MenuButton quit_menu;
        MouseEvent mousevent;
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

         
            this.IsMouseVisible = true;
            mousevent = new MouseEvent();
            jouer_menu = new MenuButton(new Vector2(300, 25), Content.Load<Texture2D>("jouer"), Content.Load<Texture2D>("joueractiv"),new Rectangle(300,25,500,125));
            options_menu = new MenuButton(new Vector2(300, 80), Content.Load<Texture2D>("option"), Content.Load<Texture2D>("optionactiv"), new Rectangle(300, 80, 500, 180));
            credit_menu = new MenuButton(new Vector2(310, 135), Content.Load<Texture2D>("credits"), Content.Load<Texture2D>("creditactiv"),new Rectangle(310,135,510,235));
            quit_menu = new MenuButton(new Vector2(310, 190), Content.Load<Texture2D>("quit"), Content.Load<Texture2D>("quitactiv"), new Rectangle(310, 190, 510, 290));
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
           
            fond = Content.Load<Texture2D>("fond");
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


            if (mousevent.getmousecontainer().Intersects(jouer_menu.getcontainer()))
            {
                jouer_menu.activ(); 
                if (mousevent.UpdateMouse())
                {
                    fond = Content.Load<Texture2D>("bl");
                }
            }
            if (mousevent.getmousecontainer().Intersects(options_menu.getcontainer()))
            {
               options_menu.activ();
               
                if (mousevent.UpdateMouse())
                {
                    Exit();
                }
            }
            if (mousevent.getmousecontainer().Intersects(credit_menu.getcontainer()))
            {
                credit_menu.activ();
                
                if (mousevent.UpdateMouse())
                {
                    Exit();
                }
            }
            if (mousevent.getmousecontainer().Intersects(quit_menu.getcontainer()))
            {
                quit_menu.activ();
              
                if (mousevent.UpdateMouse())
                {
                    Exit();
                }
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
            spriteBatch.Begin();


            spriteBatch.Draw(fond, Vector2.Zero, Color.White);
            jouer_menu.DrawButton(spriteBatch);
            options_menu.DrawButton(spriteBatch);
            credit_menu.DrawButton(spriteBatch);
            quit_menu.DrawButton(spriteBatch);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
