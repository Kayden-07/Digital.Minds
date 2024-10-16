﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace disparos.sprites
{
    public class Sprite : ICloneable
    {
        protected Texture2D _texture;

        protected float _rotation;

        protected KeyboardState _currentKey;

        protected KeyboardState _previousKey;

        public System.Numerics.Vector2 Position;

        public System.Numerics.Vector2 Origin;

        public System.Numerics.Vector2 Direction;

        public float RotationVelocity = 3f;

        public float LinearVelocity = 4f;

        public Sprite Parent;

        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        public Sprite(Texture2D texture)
        {
            _texture = texture;

            // The default origin in the centre of the sprite
            Origin = new System.Numerics.Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
