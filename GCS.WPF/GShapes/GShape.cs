using System.Collections.Generic;
using System.Windows.Shapes;

namespace GCS.WPF.GShapes
{
    public abstract class GShape
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
        public List<GShape> Childs;
        protected GShapeRule _rule;
    }
}
