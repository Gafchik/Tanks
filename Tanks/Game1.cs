using Corsaries_by_VBUteamGKMI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Tanks.Model;


namespace Tanks
{
    public class Game1 : Game
    {
        bool isUpdate = true;
        Texture2D PlayerPrefab;
        long myIp;        
        Server server = new Server();
        public Camera2d _camera = new Camera2d();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public List<ServerTank> tanks = new List<ServerTank>();
       
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
           // myIp = 17;
            

            server.Reg(myIp, 0, 0);           
            PlayerPrefab = Content.Load<Texture2D>("tank");
           tanks= server.GetList();
             new Thread(() => 
             {
                 while (isUpdate)
                 {
                     try
                     {
                         Thread.Sleep(300);
                         List<ServerTank> temp = server.GetList();                    
                         if (temp.Count > tanks.Count)
                         {
                             foreach (var item in temp)
                             {
                                 if (!tanks.Exists(i => i.ip == item.ip))
                                     tanks.Add(item);
                             }
                         }
                            List<ServerTank> remuve = new List<ServerTank>();
                         if (temp.Count < tanks.Count)
                         {
                            foreach (var item in tanks)
                             {
                                 if (!temp.Exists(i => i.ip == item.ip))
                                     remuve.Add(item);
                             }
                             remuve.ForEach(i => tanks.Remove(i));

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
                         GC.Collect(GC.GetGeneration(remuve));
                         GC.Collect(GC.GetGeneration(temp));
                     }
                     catch (Exception e) {  isUpdate = false; throw e; }
                 }
             }).Start();
        }
        protected override void UnloadContent() { server.Remuve(myIp); isUpdate = false; }


        protected override void LoadContent() => _spriteBatch = new SpriteBatch(GraphicsDevice);


        protected override void Update(GameTime gameTime)
        {
           // UpDateList();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #region кнопки перемещения
            // перемещение  по карте
            //лево
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                 new Thread(() => { server.Go_L(myIp); }).Start();
                //server.Go_L(myIp);
            //право
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                new Thread(() => { server.Go_R(myIp);}).Start();
                //server.Go_R(myIp);

            //верх
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                 new Thread(() => { server.Go_U(myIp); }).Start();
                //server.Go_U(myIp);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                new Thread(() => { server.Go_D(myIp); }).Start();
                //server.Go_D(myIp);

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
