using System.Collections.Generic;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestMonogame.Animation;
using TestMonogame.Components.@abstract;
using TestMonogame.Entities;
using TestMonogame.Entities.@abstract;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TestMonogame.Components
{
    internal class Player : GameObject
    {
        public override int ID { get; set; }
        public override string Name { get; init; }
        public override Texture2D Texture { get; init; }

        private PlayerAnimationEntity _anim;
        private InputEntity _input;
        private bool _moveLeft;
        private const float IDLE_ANIM_SPEED = 600;
        private const float WALK_ANIM_SPEED = 200;

        public Player(Dictionary<PlayerAnimationState, Texture2D[]> frames)
        {
            InitializeTransform();
            SetUpAnimation(frames);
            SetupInput();
        }

        private void InitializeTransform()
        {
            Transform.Position = new Vector2(
                Game1.GameWindowBounds.X / 2,
                Game1.GameWindowBounds.Y / 2);

            Transform.Scale = new Vector2(2, 2);
        }

        private void SetupInput()
        {
            _input = EntityFactory.CreateEntity<InputEntity>() as InputEntity;
            Game1.inputSystem.AddEntity(_input);
        }

        private void SetUpAnimation(Dictionary<PlayerAnimationState, Texture2D[]> frames)
        {
            _anim = new PlayerAnimationEntity(frames, Transform);
            _anim.SetState(PlayerAnimationState.MoveDown);
            Game1.animationSystem.AddEntity(_anim);
        }

        private void SetPlayerIdle()
        {
            var state = _anim.GetState();

            switch (state)
            {
                case PlayerAnimationState.MoveDown:
                    _anim.SetState(PlayerAnimationState.IdleDown);
                    break;
                case PlayerAnimationState.MoveUp:
                    _anim.SetState(PlayerAnimationState.IdleUp);
                    break;
                case PlayerAnimationState.MoveLeft:
                    _anim.SetState(PlayerAnimationState.IdleLeft);
                    _moveLeft = true;
                    break;
                case PlayerAnimationState.MoveRight:
                    _anim.SetState(PlayerAnimationState.IdleRight);
                    _moveLeft = false;
                    break;
            }

            _anim.DrawHorizontalFlipped(_moveLeft);
            _anim.UpdateFrameSpeed(IDLE_ANIM_SPEED);
        }

        private void SetPlayerMoving(PlayerAnimationState state)
        {
            if (state == PlayerAnimationState.MoveLeft) _moveLeft = true;
            if (state == PlayerAnimationState.MoveRight) _moveLeft = false;

            _anim.DrawHorizontalFlipped(_moveLeft);
            _anim.SetState(state);
            _anim.UpdateFrameSpeed(WALK_ANIM_SPEED);
        }

        public override void Update(GameTime gameTime)
        {
            if (_input.GetKeyDown(Keys.A)) SetPlayerMoving(PlayerAnimationState.MoveLeft);
            else if (_input.GetKeyDown(Keys.D)) SetPlayerMoving(PlayerAnimationState.MoveRight);
            else if (_input.GetKeyDown(Keys.S)) SetPlayerMoving(PlayerAnimationState.MoveDown);
            else if (_input.GetKeyDown(Keys.W)) SetPlayerMoving(PlayerAnimationState.MoveUp);
            else SetPlayerIdle();
        }
    }
}