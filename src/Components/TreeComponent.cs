using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Components.@abstract;
using TestMonogame.Entities;

namespace TestMonogame.Components
{
    internal class TreeComponent : GameObject, IIdleImplemented
    {
        public override string Name { get; init; }
        public override Texture2D Texture { get; init; }
        public override int ID { get; set; }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public int LastKnowntime { get; set; }
        public void AdvanceTime(GameTime gameTime)
        {

        }

        public void AdvanceTime(float value)
        {
            throw new NotImplementedException();
        }

        public void ApplySecondDifference(int secondDifference)
        {

        }
    }
}
