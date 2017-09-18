using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using GCS.Math;

using Vector2 = System.Windows.Vector;

using Ellipse = GCS.Math.MinimalizedShapes.Ellipse;
using Circle = GCS.Math.MinimalizedShapes.Circle;
using Line = GCS.Math.MinimalizedShapes.Line;
using Segment = GCS.Math.MinimalizedShapes.Segment;
using Dot = GCS.Math.MinimalizedShapes.Dot;

namespace GCS.MathTests
{
    [TestClass]
    public class GeometryTests
    {
        System.Collections.IComparer pointComparer = new PointComparer();
        [TestCategory("Intersect")]
        [TestMethod]
        public void TestLinelikeIntersects()
        {
            {
                Line line1 = new Line(new Vector2(-10, -10), new Vector2(10, 10));
                Line line2 = new Line(new Vector2(10, -10), new Vector2(-10, 10));
                CollectionAssert.AreEqual(Geometry.GetIntersects(line1, line2),
                    new[] { new Vector2(0, 0) });
            }
            {
                Line line1 = new Line(new Vector2(-10, -10), new Vector2(10, 10));
                Line line2 = new Line(new Vector2(10, 10), new Vector2(20, 10));
                CollectionAssert.AreEqual(Geometry.GetIntersects(line1, line2),
                    new[] { new Vector2(10, 10) });
            }
            {
                Segment seg1 = new Segment(new Vector2(0, 0), new Vector2(3, -0.5));
                Segment seg2 = new Segment(new Vector2(4, 1), new Vector2(1, -3));
                CollectionAssert.AreEqual(Geometry.GetIntersects(seg1, seg2),
                    new[] { new Vector2(2.89, -0.48) }, pointComparer);
            }
        }

        [TestCategory("Intersect")]
        [TestMethod]
        public void TestCirclentersects()
        {
            {
                Circle cir1 = new Circle(new Vector2(0, 0), new Vector2(2.5, 0));
                Circle cir2 = new Circle(new Vector2(3.5, 0), new Vector2(1, 0));
                // CollectionAssert.AreEquivalent 를 써야 할텐데 comparer을 못써서 일단은 ㅇㅅㅇ;;
                CollectionAssert.AreEqual(Geometry.GetIntersects(cir1, cir2),
                    new[] { new Vector2(2.89, -0.48) }, pointComparer);
            }
        }

        [TestCategory("Intersect")]
        [TestMethod]
        public void TestEllipseIntersects()
        {
            
        }

        [TestCategory("Intersect")]
        [TestMethod]
        public void TestInvalidShapesIntersects()
        {
            try
            {
                Line line1 = new Line(new Vector2(0, 0), new Vector2(10, 10));
                Line line2 = new Line(new Vector2(0, 0), new Vector2(100, 100));
                Geometry.GetIntersects(line1, line2);
                Assert.Fail();
            }
            catch (SameShapeException) { }
        }
    }
}
