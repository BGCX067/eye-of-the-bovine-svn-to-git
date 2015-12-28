using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;


namespace Eye_of_the_Bovine
{
    using IntTuple = Tuple<int, int>;
    using Microsoft.Xna.Framework.Input;
    public class Game : IState
    {
        public GraphicsDevice GraphicsDevice { get; set; }
        public ContentManager Content { get; set; }
        //bool isPaused = false;
        World currWorld = new World();
        public int tileSize = 64;
        const int numPlayers = 4;
        int last = 0;
        int progress = 0;
        protected List<Virus> infection = new List<Virus>();
        public List<Bacterium> players = new List<Bacterium>();
        List<Texture2D> Background = new List<Texture2D>();
        PlayerIndex[] PlayerIndexs = { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };
        public Vector2 camPosition;

        const int DefaultPlayerHealth = 12;

        public void Initalize(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            GraphicsDevice = graphicsDevice;
            Content = contentManager;
            Background.Add(Content.Load<Texture2D>("Backdrop 1"));
            // First, start loading Level 1
            StreamReader reader = new StreamReader("Content/Levels/level1.txt");
            currWorld.LoadLevel(reader);
            for (int i = 0; i < currWorld.playerSpawns.Count && i < numPlayers; i++)
                players.Add(new Bacterium(Content.Load<Texture2D>("bact 0"), currWorld.playerSpawns[i],
                    DefaultPlayerHealth, Bacterium.PlayerIndexs[i]));
            infection.Add(new Virus(this, Content.Load<Texture2D>("Assets/Virus"), new Vector2(10, 2)));
            infection.Add(new Virus(this, Content.Load<Texture2D>("Assets/Virus"), new Vector2(14, 8)));
            infection.Add(new Virus(this, Content.Load<Texture2D>("Assets/Virus"), new Vector2(5, 7)));
            infection.Add(new Virus(this, Content.Load<Texture2D>("Assets/Virus"), new Vector2(1, 1)));
            infection.Add(new Virus(this, Content.Load<Texture2D>("Assets/Virus"), new Vector2(3, 14)));
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Update the players
            foreach ( Bacterium player in players) {
                player.Update(gameTime, currWorld);
            }
            foreach (Virus v in infection)
                v.Update();

            App.GetInstance().Melody.Update(last, progress);
        }

        public void Render(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            for (int beat = 0; beat < Background.Count; beat++)
            {
                spriteBatch.Draw(Background[beat],App.GetInstance().upperleft, Color.White);
            }
            // Calc Camera position in the world
            camPosition = new Vector2(0.0f, 0.0f);

            for ( int i = 0; i < players.Count; i++) 
                camPosition += players[i].Position;


            // We have our camera position
            camPosition /= (float)players.Count;
            camPosition += new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / (tileSize * -2.0F),
                spriteBatch.GraphicsDevice.Viewport.Height / (tileSize * -2.0F));

            Vector2 upperLeft = new Vector2(
                camPosition.X - (spriteBatch.GraphicsDevice.Viewport.Width / 2.0f) - 1,
                camPosition.Y - (spriteBatch.GraphicsDevice.Viewport.Height / 2.0f) - 1);
            Vector2 lowerRight=new Vector2(
                camPosition.X + (spriteBatch.GraphicsDevice.Viewport.Width / 2.0f) + 1,
                camPosition.Y + (spriteBatch.GraphicsDevice.Viewport.Height / 2.0f) + 1);

            for (int row = 0; row < currWorld.Width; row++)
            {
                for (int col = 0; col < currWorld.Height; col++)
                {
                    // check if this is cube will be in the view box
                    Tile currTile = currWorld.Tiles[row, col];

                    // check if our lower left corner is lower than the lower right corner
                    if (row + 1 > upperLeft.X && col + 1 > upperLeft.Y &&
                        row - 1 < lowerRight.X && col - 1 < lowerRight.Y)
                    {
                        Rectangle rect = new Rectangle((int)(row * tileSize - camPosition.X * tileSize),
                            (int)(col * tileSize - camPosition.Y * tileSize), tileSize, tileSize);
                        spriteBatch.Draw(currTile.Photo, rect, Color.White);
                    }
                }
            }
            foreach (Virus v in infection)
                v.Draw(spriteBatch);

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].damaged == false)
                {
                    spriteBatch.Draw(players[i].Texture,
                        new Rectangle((int)((players[i].Position.X - camPosition.X) * (float)tileSize),
                        (int)((players[i].Position.Y - camPosition.Y) * (float)tileSize), tileSize, tileSize),
                        Color.White);
                }
                else
                {
                    spriteBatch.Draw(players[i].Texture,
                        new Rectangle((int)((players[i].Position.X - camPosition.X) * (float)tileSize),
                        (int)((players[i].Position.Y - camPosition.Y) * (float)tileSize), tileSize, tileSize),
                        Color.Red);
                    players[i].damaged = false;
                }
            }
            
          
        }

        public void Shutdown()
        {
        }
    }
}
