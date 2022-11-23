using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestMonogame.Components
{
    public enum PlayerAnimationState
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        IdleDown,
        IdleUp,
        IdleLeft,
        IdleRight
    }

    internal static class AnimationDictionaryBuilder
    {
        public static void LoadPlayerFrames(Game game)
        {
            // idle == GunWalk1 && GunWalk4
            // populate player frames
            _upFrames = new[]
            {
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk2"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk3"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk5"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk6"),
            };
            _downFrames = new[]
            {
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk2"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk3"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk5"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk6"),
            };
            _leftFrames = new[]
            {
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk2"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk3"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk5"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk6"),
            };
            _rightFrames = new[]
            {
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk2"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk3"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk5"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk6"),
            };

            _idleUpFrames = new[]
            {
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
            };

            _idleDownFrames = new[]
            {
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
            };

            _idleLeftFrames = new[]
            {
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
            };

            _idleRightFrames = new[]
            {
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk1"),
                game.Content.Load<Texture2D>("./Cyborg/GunWalk4"),
            };
        }

        private static Texture2D[] _upFrames;
        private static Texture2D[] _downFrames;
        private static Texture2D[] _leftFrames;
        private static Texture2D[] _rightFrames;

        private static Texture2D[] _idleUpFrames;
        private static Texture2D[] _idleDownFrames;
        private static Texture2D[] _idleLeftFrames;
        private static Texture2D[] _idleRightFrames;

        public static Dictionary<PlayerAnimationState, Texture2D[]> BuildPlayerDictionary()
        {
            var result = new Dictionary<PlayerAnimationState, Texture2D[]>
            {
                // move
                { PlayerAnimationState.MoveUp, _upFrames },
                { PlayerAnimationState.MoveDown, _downFrames },
                { PlayerAnimationState.MoveLeft, _leftFrames },
                { PlayerAnimationState.MoveRight, _rightFrames },

                // idle
                { PlayerAnimationState.IdleUp, _idleUpFrames },
                { PlayerAnimationState.IdleDown, _idleDownFrames },
                { PlayerAnimationState.IdleLeft, _idleLeftFrames },
                { PlayerAnimationState.IdleRight, _idleRightFrames },
                
            };
            return result;
        }
    }
}