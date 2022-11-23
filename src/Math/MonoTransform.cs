using System.Collections.Generic;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TestMonogame.Math
{
    internal class MonoTransform
    {
        private Vector2 _localSpace;
        private MonoTransform _parent;
        private Vector2 _worldSpace;
        
        public List<MonoTransform> children = new();
        public Vector2 Scale { get; set; }

        public MonoTransform Parent
        {
            get => _parent;
            internal set
            {
                value.AddChild(this);
                _parent = value;
                Position += value.Position;
            }
        }
        
        // todo round off error fix
        public Vector2 LocalPosition
        {
            get => _localSpace;
            set
            {
                if (Parent != null)
                {
                    _localSpace = value;
                    _worldSpace = Parent.Position + value;
                }
                else _localSpace = value;
            }
        }

        public Vector2 Position
        {
            get => _worldSpace;
            set
            {
                foreach (var child in children) child.Position += value - _worldSpace;
                _worldSpace = value;
                if (Parent != null) _localSpace = value - Parent.Position;
            }
        }

        public void AddChild(MonoTransform child) =>
            children.Add(child);
        
        // don't think I need this??
        public void MovePosition(float x, float y) =>
            Position += new Vector2(x, y);

        public void MovePosition(Vector2 variable) =>
            Position += variable;

        public readonly Vector2 Down = new()
            { X = 0, Y = 1};

        public void MoveDownBy(float number) =>
            MovePosition(Down * number);

        public readonly Vector2 Up = new()
            { X = 0, Y = -1};

        public void MoveUpBy(float number) =>
            MovePosition(Up * number);

        public readonly Vector2 Left = new()
            { X = -1, Y = 0 };

        public void MoveLeftBy(float number) =>
            MovePosition(Left * number);

        public readonly Vector2 Right = new()
            { X = 1, Y = 0 };

         public void MoveRightBy(float number) =>
            MovePosition(Right * number);
    }
}