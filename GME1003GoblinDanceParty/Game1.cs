using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

namespace GME1003GoblinDanceParty
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Star storage - single list of lightweight value types
        private struct Star
        {
            public Vector2 Position;
            public Color Color;
            public float Scale;
            public float Transparency;
            public float Rotation;
        }

        private int _numStars;
        private List<Star> _stars;

        private Texture2D _starSprite;  // the sprite image for our star
        private Vector2 _starOrigin;    // cached origin based on sprite size

        private Random _rng;            // for all our random number needs

        //***This is for the goblin. Ignore it.
        Goblin goblin;
        Song music;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // single Random instance
            _rng = new Random();

            // more sensible default as suggested in the comment
            _numStars = _rng.Next(100, 301);

            // allocate list with known capacity to avoid resizing
            _stars = new List<Star>(_numStars);

            // get screen bounds (fallback to common values if GraphicsDevice not ready)
            int screenWidth = GraphicsDevice?.Viewport.Width ?? 800;
            int screenHeight = GraphicsDevice?.Viewport.Height ?? 480;

            // Populate stars with per-star properties
            for (int i = 0; i < _numStars; i++)
            {
                // rotation speed in range [-PI/4, PI/4] radians/sec (~±45°/s)
                float rotSpeed = (float)(_rng.NextDouble() * Math.PI / 2.0 - Math.PI / 4.0);

                var s = new Star
                {
                    Position = new Vector2(
                        _rng.Next(0, screenWidth + 1),
                        _rng.Next(0, screenHeight + 1)
                    ),

                    // keep colors in the lighter half like original (128..255)
                    Color = new Color(
                        (byte)(128 + _rng.Next(0, 129)),
                        (byte)(128 + _rng.Next(0, 129)),
                        (byte)(128 + _rng.Next(0, 129))
                    ),

                    // scale roughly between 0.25 and 0.5 (similar to original)
                    Scale = _rng.Next(50, 100) / 200f,

                    // transparency between 0.25 and 1.0
                    Transparency = _rng.Next(25, 101) / 100f,

                    // rotation full circle
                    Rotation = (float)(_rng.NextDouble() * Math.PI * 2.0)
                };

                _stars.Add(s);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // load our star sprite
            _starSprite = Content.Load<Texture2D>("starSprite");
            _starOrigin = new Vector2(_starSprite.Width / 2f, _starSprite.Height / 2f);

            //***This is for the goblin. Ignore it for now.
            goblin = new Goblin(Content.Load<Texture2D>("goblinIdleSpriteSheet"), 400, 400);
            music = Content.Load<Song>("chiptune");

            // if you're tired of the music player, comment this out!
            MediaPlayer.Play(music);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //***This is for the goblin. Ignore it for now.
            goblin.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // draw stars using per-star values
            for (int i = 0; i < _stars.Count; i++)
            {
                var s = _stars[i];

                // multiply the color by transparency - MonoGame/XNA supports multiplying a Color by a float
                _spriteBatch.Draw(
                    _starSprite,
                    s.Position,
                    null,
                    s.Color * s.Transparency,
                    s.Rotation,
                    _starOrigin,
                    new Vector2(s.Scale, s.Scale),
                    SpriteEffects.None,
                    0f
                );
            }

            _spriteBatch.End();

            //***This is for the goblin. Ignore it for now.
            goblin.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
