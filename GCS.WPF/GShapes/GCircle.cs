﻿using System;
using System.Windows;
using System.Windows.Shapes;
using GCS.Math;

using Vector2 = System.Windows.Vector;

namespace GCS.WPF.GShapes
{
    public class GCircle : GShape, ICircle
    {
        public Vector2 Center { get; protected set; }
        public Vector2 Another { get; protected set; }

        protected override int _attrCount => 2;
        protected override Vector2 this[int index]
        {
            get => new Vector2[] { Center, Another }[index];
            set
            {
                switch(index)
                {
                    case 0: Center = value; return;
                    case 1: Another = value; return;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        protected GCircle(Ellipse circle = null)
        {
            Control = circle ?? new Ellipse();
        }

        protected GCircle(Vector2 center, Vector2 another) : this()
        {
            (Control as Ellipse).SetCircle(center, another);
            Center = center;
            Another = another;
        }

        public static GCircle FromTwoDots(GDot center, GDot another)
        {
            var circle = new GCircle();
            new CircleOnTwoDotsRule(circle, center, another);
            return circle;
        }

        public class CircleOnTwoDotsRule : GShapeRule
        {
            public CircleOnTwoDotsRule(GCircle circle, GDot p1, GDot p2) : base(circle)
            {
                SetRelationship(circle, p1, p2);
            }

            public override void HandleMove()
            {
                gShape.Parents[0].MoveTo((gShape as GCircle).Center);
                gShape.Parents[1].MoveTo((gShape as GCircle).Another);
            }

            public override void Fix()
            {
                (gShape as GCircle).Center = (gShape.Parents[0] as GDot).Coord;
                (gShape as GCircle).Another = (gShape.Parents[1] as GDot).Coord;
            }
        }
    }
}
