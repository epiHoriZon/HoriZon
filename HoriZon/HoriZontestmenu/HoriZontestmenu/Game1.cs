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
        Monstre ronflex;


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

        public bool menu_actif =true;
        bool sousmenu_actif = false;
        bool menu_gameover = false;
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

            heros = new Personnage(Content.Load<Texture2D>("walk_iso"), new Rectangle(200,200,75,101),100,Content.Load<SpriteFont>("Font_PDV"));
            ronflex = new Monstre(Content.Load<Texture2D>("monstre"), new Vector2 ( 20,250));
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
            else if (sousmenu_actif)
            {
                if (mousevent.getmousecontainer().Intersects(angl_menu.getcontainer()) || mousevent.getmousecontainer().Intersects(fra_menu.getcontainer()) || mousevent.getmousecontainer().Intersects(langue_menu.getcontainer()))
                {
                    langue_menu.activ();
                    Console.WriteLine("intersect OK");
                }
                else
                {
                    langue_menu.desactiv();
                }
            }
            else if (menu_gameover)
            {
                fond = Content.Load<Texture2D>("fond_gameover");
                    if(mousevent.UpdateMouse() && mousevent.getmousecontainer().Intersects(new Rectangle(510,438,800,600)))
                    {

                        menu_gameover = false;
                        menu_actif = true;
                    }

            }
#endregion
            else
            {

                heros.deplacement();

                if (heros.getplayercontainer().Intersects(ronflex.getmonstercontainer()) )
                {
                  heros.Points_Vie_Perso --;
  
                }
                if (heros.Points_Vie_Perso <= 0)
                {

                    menu_gameover = true;
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
            }
            else if (menu_gameover)
            {
                
            }
            else
            {
                heros.Drawperso(spriteBatch);
                heros.Draw_PDV(spriteBatch);
                ronflex.DrawMonstre(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
