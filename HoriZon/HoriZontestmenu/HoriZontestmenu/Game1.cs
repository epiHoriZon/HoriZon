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

        MouseEvent mousevent;

        bool menu_actif =true;
        bool sousmenu_actif = false;
        #endregion
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
            jouer_menu = new MenuButton(new Vector2(300, 25), Content.Load<Texture2D>("jouer"), Content.Load<Texture2D>("joueractiv"));
            options_menu = new MenuButton(new Vector2(300, 80), Content.Load<Texture2D>("option"), Content.Load<Texture2D>("optionactiv"));
            credit_menu = new MenuButton(new Vector2(310, 135), Content.Load<Texture2D>("credits"), Content.Load<Texture2D>("creditactiv"));
            quit_menu = new MenuButton(new Vector2(310, 190), Content.Load<Texture2D>("quit"), Content.Load<Texture2D>("quitactiv"));

            langue_menu = new MenuButton(new Vector2(50, 50), Content.Load<Texture2D>("langue"), Content.Load<Texture2D>("langueactiv"));
            fra_menu = new MenuButton(new Vector2(300, 50), Content.Load<Texture2D>("fraactiv"), Content.Load<Texture2D>("fra"));
            angl_menu = new MenuButton(new Vector2(500, 48), Content.Load<Texture2D>("angl"), Content.Load<Texture2D>("anglactiv"));
            PE_menu = new MenuButton(new Vector2(50, 200), Content.Load<Texture2D>("PE"), Content.Load<Texture2D>("PEactiv"));
            Onoff_menu = new MenuButton(new Vector2(300, 200), Content.Load<Texture2D>("offactiv"), Content.Load<Texture2D>("onactiv"));
            #endregion

            heros = new Personnage(Content.Load<Texture2D>("walk_iso"), new Rectangle(0,0,75,101));
 
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

            
          
            if (menu_actif )
            {
                #region menu
                #region menu principal
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
                if (sousmenu_actif)
                {
                    if (mousevent.getmousecontainer().Intersects(angl_menu.getcontainer()))
                    {
                        langue_menu.activ();
                        Console.WriteLine("intersect ok");
                        if (mousevent.UpdateMouse())
                        {     
                                angl_menu.activ(); fra_menu.desactiv();
                                Console.WriteLine("click ok");
                        }
                    }
                    Console.WriteLine("sous menu actif");
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
            else if (sousmenu_actif)
            {
                langue_menu.DrawButton(spriteBatch);
                angl_menu.DrawButton(spriteBatch);
                fra_menu.DrawButton(spriteBatch);
                Onoff_menu.DrawButton(spriteBatch);
                PE_menu.DrawButton(spriteBatch);
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
