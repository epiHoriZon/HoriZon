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

        Texture2D fond;
        MenuButton jouer_menu;
        MenuButton options_menu;
        MenuButton credit_menu;
        MenuButton quit_menu;
        MouseEvent mousevent;
        bool menu_actif =true;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
           

         
            this.IsMouseVisible = true;
            #region variables menu
            mousevent = new MouseEvent();
            jouer_menu = new MenuButton(new Vector2(300, 25), Content.Load<Texture2D>("jouer"), Content.Load<Texture2D>("joueractiv"),new Rectangle(300,25,500,125));
            options_menu = new MenuButton(new Vector2(300, 80), Content.Load<Texture2D>("option"), Content.Load<Texture2D>("optionactiv"), new Rectangle(300, 80, 500, 180));
            credit_menu = new MenuButton(new Vector2(310, 135), Content.Load<Texture2D>("credits"), Content.Load<Texture2D>("creditactiv"),new Rectangle(310,135,510,235));
            quit_menu = new MenuButton(new Vector2(310, 190), Content.Load<Texture2D>("quit"), Content.Load<Texture2D>("quitactiv"), new Rectangle(310, 190, 510, 290));
            #endregion

            heros = new Personnage(Content.Load<Texture2D>("testanim"), new Rectangle(0,0,32,64));
            
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
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
          
            if (menu_actif)
            {
                #region menu

                if (mousevent.getmousecontainer().Intersects(jouer_menu.getcontainer()))
                {
                    jouer_menu.activ();

                    options_menu.desactiv(); credit_menu.desactiv(); quit_menu.desactiv();

                    if (mousevent.UpdateMouse())
                    {
                        fond = Content.Load<Texture2D>("bl");
                        menu_actif = false;
                    }

                }
                if (mousevent.getmousecontainer().Intersects(options_menu.getcontainer()))
                {
                    options_menu.activ();
                    jouer_menu.desactiv(); credit_menu.desactiv(); quit_menu.desactiv();
                    if (mousevent.UpdateMouse())
                    {
                        Exit();

                    }
                }
                if (mousevent.getmousecontainer().Intersects(credit_menu.getcontainer()))
                {
                    credit_menu.activ();
                    jouer_menu.desactiv(); options_menu.desactiv(); quit_menu.desactiv();
                    if (mousevent.UpdateMouse())
                    {
                        Exit();
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
         
            else
            {

                heros.deplacement();
                heros.getplayercontainer();
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
            else
            {
                heros.Drawperso(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
