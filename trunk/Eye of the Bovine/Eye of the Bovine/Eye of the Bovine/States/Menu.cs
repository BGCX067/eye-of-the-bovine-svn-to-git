using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Eye_of_the_Bovine
{
    public class Menu : IState
    {
        SpriteFont spritFont;
        string[] lines;
        int currIndex = 0;
        Vector2 centerPos;
        Texture2D background;
        PlayerIndex[] PlayerIndexs = { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };

        public GraphicsDevice GraphicsDevice { get; set; }
        public ContentManager Content { get; set; }

        public Menu(SpriteFont sf, string[] lines) {
            spritFont = sf;
            this.lines = lines;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            // render the background
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);
            // Find the center of the string
            Vector2 FontOrigin = spritFont.MeasureString(lines[0]) / 2;
            // Draw the string
            spriteBatch.DrawString(spritFont, lines[currIndex], centerPos, Color.Red,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

        }

        public void Update(GameTime gameTime)
        {
            for(int number=0; number<4; number++){
                GamePadState currState = GamePad.GetState(PlayerIndexs[number]);

                if (currState.IsConnected && currState.Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    // start playing the game
                    App.GetInstance().CurrentState = new Game();
                    break;
                }
            }
        }

        public void Initalize(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            GraphicsDevice = graphicsDevice;
            Content = contentManager;
            // TODO: Load your game content here            
            centerPos = new Vector2(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2);
            background = Content.Load<Texture2D>("grass");
            
        }

        public void Shutdown()
        {
            
        }


    }
}
