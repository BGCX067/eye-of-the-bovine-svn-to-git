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

namespace Eye_of_the_Bovine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class App : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        
        enum GameState { Start, MainMenu, Playing, Paused, LevelSwitch, End };
        enum MenuState { };
        Camera Vision;
        public Vector2 upperleft;
        public World Level;
        public Bacterium[] Players;
        IState currentState = null;
        public Music Sound;
        public Music Melody;
        public IState CurrentState
        {
            get { return currentState; }
            set
            {
                // shutdown the current state
                if (currentState != null) { currentState.Shutdown(); }
                // init the new state
                value.Initalize(GraphicsDevice, Content);
                // update teh state
                currentState = value;
            }
        }
        
        #region Singleton Stuff
        static private App instance = null; 
        public static App GetInstance() 
        {
            if (instance == null)
                instance = new App();
            return instance;
        }
        #endregion
        
        public App()
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
            Sound = new Music(this);
            Melody = new Music(this);
            Vision = new Camera(GraphicsDevice.Viewport);
            upperleft = new Vector2(0, 0);
            UploadSound();
            Melody.LoadSong("Layer 1 Final Mix Loop");//0
            Melody.LoadSong("Layer 2 Final Mix Loop");//1
            Melody.LoadSong("Layer 3 Final Mix Loop");//2
            Melody.LoadSong("End Sting");//3
            MediaPlayer.Play(Melody.SONGS[0]);
            base.Initialize();
        }
        public List<Texture2D> TileSwitch;

        private void UploadSound() {
            Sound.LoadWav("Antibody 2");//0
            Sound.LoadWav("ATP 9");//1
            Sound.LoadWav("Awesome Success");//2
            Sound.LoadWav("Endothermic B");//3
            Sound.LoadWav("Enzyme 2");//4
            Sound.LoadWav("Player Death 2");//5
            Sound.LoadWav("Player Hit 3");//6
            Sound.LoadWav("Poison 1");//7
            Sound.LoadWav("Small Success");//8
            Sound.LoadWav("Virus Death");//9
            Sound.LoadWav("White Blood Cell Death");//10
            Sound.LoadWav("Collect 1");//11
            Sound.LoadWav("Collect 2");//12
            Sound.LoadWav("Collect 3");//13
            Sound.LoadWav("Death");//14
            Sound.LoadWav("Hit");//15
            Sound.LoadWav("Icky Squish");//16
            Sound.LoadWav("Slime Splash");//17
            Sound.LoadWav("Spit Shoot 1");//18
            Sound.LoadWav("Splashy Goo");//19
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            CurrentState = new Menu(Content.Load<SpriteFont>("MenuFont"), new string[] { "Play Game"});
            TileSwitch = new List<Texture2D>();
            TileSwitch.Add(Content.Load<Texture2D>("Assets/Bridge"));
            TileSwitch.Add(Content.Load<Texture2D>("Assets/floor8"));
            TileSwitch.Add(Content.Load<Texture2D>("Assets/ATP Lantern On"));
            TileSwitch.Add(Content.Load<Texture2D>("Assets/Catalyst Lantern Full"));
            TileSwitch.Add(Content.Load<Texture2D>("Assets/floor8"));
            /*
            TileSwitch.Add(Content.Load<Texture2D>(""));
            TileSwitch.Add(Content.Load<Texture2D>(""));
            TileSwitch.Add(Content.Load<Texture2D>(""));
            TileSwitch.Add(Content.Load<Texture2D>(""));
            TileSwitch.Add(Content.Load<Texture2D>(""));
             */
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

            // TODO: Add your update logic here
            CurrentState.Update(gameTime);
            /*
            if (((MediaPlayer.PlayPosition.Minutes * 60) + MediaPlayer.PlayPosition.Seconds) >= ((((float)Sound.SONGS[0].Duration.Minutes * 60f) + (float)Sound.SONGS[0].Duration.Seconds) - ((((float)Sound.SONGS[0].Duration.Minutes * 60f) + (float)Sound.SONGS[0].Duration.Seconds) * .01f)))
            {
                MediaPlayer.Play(Sound.SONGS[0]);
            }
            */

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            CurrentState.Render(spriteBatch);
            spriteBatch.End();
            /*
            switch (StateGame)
            {
                case (int)GameState.Start:
                    break;
                case (int)GameState.MainMenu:
                    switch (StateMenu) { 
                        default:
                            break;
                    }
                    break;
                case (int)GameState.Playing:
                    break;
                case(int)GameState.Paused:
                    break;
                case(int)GameState.LevelSwitch:
                    break;
                case(int)GameState.End:
                    break;
                default:
                    break;
            }
             * */
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
