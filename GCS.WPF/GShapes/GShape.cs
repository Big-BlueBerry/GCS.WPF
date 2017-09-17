using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows;
using GCS.Math;

namespace GCS.WPF.GShapes
{
    public abstract class GShape : IShape
    {
        public abstract class GShapeRule
        {
            public GShape gShape;
            public GShapeRule(GShape shape)
            {
                gShape = shape;
            }

            protected bool _isMoving = false;
            public void Move()
            {
                if (_isMoving) return;
                _isMoving = true;

                HandleMove();

                Fix();
                MoveChilds();
                _isMoving = false;
            }

            public void MoveChilds()
            {
                foreach (var c in gShape.Childs)
                    c._rule.OnParentMoved();
            }

            public void OnParentMoved()
            {
                if (_isMoving) return;
                Fix();
                MoveChilds();
            }

            public abstract void Fix();
            public abstract void HandleMove();
        }

        public Shape Control { get; protected set; }
        public List<GShape> Parents = new List<GShape>();
        public List<GShape> Childs = new List<GShape>();
        protected GShapeRule _rule;

        protected abstract int _attrCount { get; }
        protected abstract Point this[int index] { get; set; }

        public void Move(Point delta)
        {
            for (int i = 0; i < _attrCount; i++)
                this[i] += new Vector(delta.X, delta.Y);
            _rule?.Move();
        }
        public void MoveTo(Point to)
        {
            Move(to - new Vector(this[0].X, this[0].Y));
        }

        public static void SetRelationship(GShape parent, params GShape[] childs)
        {
            foreach(var c in childs)
            {
                parent.Childs.Add(c);
                c.Parents.Add(parent);
            }
        }
    }
}
