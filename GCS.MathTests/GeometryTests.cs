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
                CollectionAssert.AreEqual(line1 & line2,
                    new[] { new Vector2(0, 0) });
            }
            {
                Line line1 = new Line(new Vector2(-10, -10), new Vector2(10, 10));
                Line line2 = new Line(new Vector2(10, 10), new Vector2(20, 10));
                CollectionAssert.AreEqual(line1 & line2,
                    new[] { new Vector2(10, 10) });
            }
            {
                Segment seg1 = new Segment(new Vector2(0, 0), new Vector2(3, -0.5));
                Segment seg2 = new Segment(new Vector2(4, 1), new Vector2(1, -3));
                CollectionAssert.AreEqual(seg1 & seg2,
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
                CollectionAssert.AreEqual(cir1 & cir2,
                    new[] { new Vector2(1.75, 1.79), new Vector2(1.75, -1.79) }, pointComparer);
            }
            {
                Circle cir1 = new Circle(new Vector2(1, 2), new Vector2(3, 4));
                Circle cir2 = new Circle(new Vector2(3.5, 1.5), new Vector2(4.5, 2.5));
                CollectionAssert.AreEqual(cir1 & cir2,
                    new[] { new Vector2(3.68, 2.9), new Vector2(3.13, 0.14) }, pointComparer);
            }
            {
                Circle cir1 = new Circle(new Vector2(0, 0), new Vector2(2, 1.5));
                Circle cir2 = new Circle(new Vector2(0, 1.5), new Vector2(0, 2.5));
                CollectionAssert.AreEqual(cir1 & cir2,
                    new[] { new Vector2(0, 2.5) }, pointComparer);
            }
            {
                Circle cir1 = new Circle(new Vector2(2, 4), new Vector2(4, 2));
                Circle cir2 = new Circle(new Vector2(3, 2.5), new Vector2(4, 2.5));
                CollectionAssert.AreEqual(cir1 & cir2,
                    new Vector2[] { }, pointComparer);
            }
            {
                Circle cir1 = new Circle(new Vector2(2, 4), new Vector2(4, 2));
                Circle cir2 = new Circle(new Vector2(6, 4), new Vector2(6.5, 4));
                CollectionAssert.AreEqual(cir1 & cir2,
                    new Vector2[] { }, pointComparer);
            }
            {
                Circle cir1 = new Circle(new Vector2(2, 4), new Vector2(4, 2));
                Circle cir2 = new Circle(new Vector2(3, 2.5), new Vector2(3.5, 3.5));
                CollectionAssert.AreEqual(cir1 & cir2,
                    new [] { new Vector2(4, 2), new Vector2(3.08, 1.38) }, pointComparer);
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
                Line line1 = new Line(new Vector2(0, 0), new Vector2(0, 0));
                Line line2 = new Line(new Vector2(0, 0), new Vector2(100, 100));
                var res = line1 & line2;
                Assert.Fail();
            }
            catch (InvalidShapeException) { }
            try
            {
                Circle circle = new Circle(new Vector2(42, 42), new Vector2(42, 42));
                Line line = new Line(new Vector2(0, 0), new Vector2(100, 100));
                var res = circle & line;
                Assert.Fail();
            }
            catch (InvalidShapeException) { }
            try
            {
                Line line1 = new Line(new Vector2(0, 0), new Vector2(10, 10));
                Line line2 = new Line(new Vector2(0, 0), new Vector2(100, 100));
                var res = line1 & line2;
                Assert.Fail();
            }
            catch (SameShapeException) { }
            try
            {
                Line line1 = new Line(new Vector2(3, 7), new Vector2(7, 1));
                Line line2 = new Line(new Vector2(4, 5.5), new Vector2(6, 2.5));
                var res = line1 & line2;
                Assert.Fail();
            }
            catch (SameShapeException) { }
        }
    }
}
