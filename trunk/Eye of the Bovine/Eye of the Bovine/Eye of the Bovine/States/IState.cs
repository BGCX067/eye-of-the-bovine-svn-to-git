using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Eye_of_the_Bovine
{
    public interface IState
    {
        // Inits the state
        void Initalize(GraphicsDevice graphicsDevice, ContentManager contentManager);
        // Updates the state
        // If true is returned, will update lower layers
        void Update(GameTime gameTime);
        // Render the state
        void Render(SpriteBatch spriteBatch);
        // Kills the state
        void Shutdown();
    }
}
