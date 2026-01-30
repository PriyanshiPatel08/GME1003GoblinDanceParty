using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GME1003GoblinDanceParty
{

    //***You are free to look at this class, but there's no need to change anything.
    internal class Goblin
    {
        private Texture2D _goblinSprite;
        private int _rows, _cols, _currentFrame;
        private Vector2 _position;

        private int timeSinceLastFrame = 0;
        private int millisecondsPerFrame = 65;

        private SpriteEffects _faceLeft = SpriteEffects.FlipHorizontally;
        private int _faceLeftCount = 0;

        public Goblin(Texture2D goblinSprite, int x, int y)
        {
            _goblinSprite = goblinSprite;
            _position = new Vector2(x, y);

            _currentFrame = 0;
            _cols = 8;
            _rows = 1;
        }

        public void Update(GameTime gameTime)
        {
            _faceLeftCount++;

            if(_faceLeftCount == 120)
            {
                if (_faceLeft == SpriteEffects.FlipHorizontally)
                    _faceLeft = SpriteEffects.None;
                else
                    _faceLeft = SpriteEffects.FlipHorizontally;

                _faceLeftCount = 0;
            }


            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                _currentFrame++;
                if (_currentFrame == _cols)
                    _currentFrame = 0;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            int width = _goblinSprite.Width / _cols;
            int height = _goblinSprite.Height / _rows;
            int row = 0;
            int column = _currentFrame;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            //Rectangle destinationRectangle = new Rectangle((int)_position.X, (int)_position.Y, width, height);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            spriteBatch.Draw(_goblinSprite, _position, sourceRectangle, Color.White, 0, new Vector2(width / 2, height / 2), new Vector2(5, 5), _faceLeft, 0);
            spriteBatch.End();
        }

    }



}
