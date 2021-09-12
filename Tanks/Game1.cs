using Corsaries_by_VBUteamGKMI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Tanks.Model;


namespace Tanks
{
    public class Game1 : Game
    {      
       
        Texture2D PlayerPrefab;
        long myIp;        
       Server server = new Server();
        public Camera2d _camera = new Camera2d();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public List<ServerTank> tanks = new List<ServerTank>();
        private async void UpDateList()
        {
           await Task.Run(() =>
            {               
                    try
                    {
                        var temp = server.GetList();

                        if (temp.Count != tanks.Count)
                        {
                            tanks.Clear();
                            tanks = temp;
                        }
                        else
                        {
                            for (int i = 0; i < tanks.Count; i++)
                            {
                                if (tanks[i].x != temp[i].x)
                                    tanks[i].x = temp[i].x;
                                if (tanks[i].y != temp[i].y)
                                    tanks[i].y = temp[i].y;
                            }
                        }
                        GC.Collect(GC.GetGeneration(temp));
                    }
                    catch (Exception) {  }
                
            });
        }
        public Game1()
        {
           
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;            
        }

        protected override void Initialize()
        {
            base.Initialize();
             myIp = Dns.GetHostAddresses(Dns.GetHostName())[0].Address;
            

            server.Reg(myIp, 0, 0);           
            PlayerPrefab = Content.Load<Texture2D>("tank");
           tanks= server.GetList();
        }
        protected override void UnloadContent()
        {           
            server.Remuve(myIp);
        
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #region кнопки перемещения
            // перемещение  по карте





            //лево
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                 server.Go_L(myIp);
            //право
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                 server.Go_R(myIp); 
            //верх
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                 server.Go_U(myIp); 
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                 server.Go_D(myIp); 

            UpDateList();
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            _spriteBatch.Begin();
            tanks.ForEach(i => _spriteBatch.Draw(PlayerPrefab, new Vector2(i.x,i.y), Color.White));
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
