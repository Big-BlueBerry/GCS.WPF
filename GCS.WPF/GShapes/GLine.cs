﻿using System;
using System.Windows;
using System.Windows.Shapes;
using GCS.Math;

using Vector2 = System.Windows.Vector;

namespace GCS.WPF.GShapes
{
    public class GLine : GShape, ILine
    {
        public Vector2 Point1 { get; protected set; }
        public Vector2 Point2 { get; protected set; }

        protected override int _attrCount => 2;
        protected override Vector2 this[int index]
        {
            get => new Vector2[] { Point1, Point2 }[index];
            set
            {
                switch (index)
                {
                    case 0: Point1 = value; return;
                    case 1: Point2 = value; return;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        protected GLine(Line line = null)
        {
            Control = line ?? new Line();
        }

        protected GLine(Vector2 p1, Vector2 p2) : this()
        {
            (Control as Line).SetTwoPoint(p1, p2);
            Point1 = p1; Point2 = p2;
        }

        public static GLine FromTwoDots(GDot p1, GDot p2)
        {
            var line = new GLine();
            new LineOnTwoDotsRule(line, p1, p2);
            return line;
        }

        public class LineOnTwoDotsRule : GShapeRule
        {
            public LineOnTwoDotsRule(GLine line, GDot p1, GDot p2) : base(line)
            {
                SetRelationship(line, p1, p2);
            }
            public override void HandleMove()
            {
                gShape.Parents[0].MoveTo((gShape as GLine).Point1);
                gShape.Parents[0].MoveTo((gShape as GLine).Point2);
            }

            public override void Fix()
            {
                (gShape as GLine).Point1 = (gShape.Parents[0] as GDot).Coord;
                (gShape as GLine).Point2 = (gShape.Parents[1] as GDot).Coord;
            }
        }
    }
}
