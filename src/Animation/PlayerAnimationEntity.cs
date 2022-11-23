using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Components;
using TestMonogame.Entities;
using TestMonogame.Math;

namespace TestMonogame.Animation
{
    internal class PlayerAnimationEntity : AnimationEntity<PlayerAnimationState>
    {

        public PlayerAnimationEntity(Dictionary<PlayerAnimationState, Texture2D[]> animDictionary, MonoTransform objTransform) 
            : base(objTransform)
        {
            lookup = animDictionary;
        }


    }
}
