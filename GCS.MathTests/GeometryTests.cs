using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using GCS.Math;

using Ellipse = GCS.MathTests.MinimalizedShapes.Ellipse;
using Circle = GCS.MathTests.MinimalizedShapes.Circle;
using Line = GCS.MathTests.MinimalizedShapes.Line;
using Segment = GCS.MathTests.MinimalizedShapes.Segment;
using Dot = GCS.MathTests.MinimalizedShapes.Dot;

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
                Line line1 = new Line(new Point(-10, -10), new Point(10, 10));
                Line line2 = new Line(new Point(10, -10), new Point(-10, 10));
                CollectionAssert.AreEqual(Geometry.GetIntersects(line1, line2),
                    new[] { new Point(0, 0) });
            }
            {
                Line line1 = new Line(new Point(-10, -10), new Point(10, 10));
                Line line2 = new Line(new Point(10, 10), new Point(20, 10));
                CollectionAssert.AreEqual(Geometry.GetIntersects(line1, line2),
                    new[] { new Point(10, 10) });
            }
            {
                Segment seg1 = new Segment(new Point(0, 0), new Point(3, -0.5));
                Segment seg2 = new Segment(new Point(4, 1), new Point(1, -3));
                CollectionAssert.AreEqual(Geometry.GetIntersects(seg1, seg2),
                    new[] { new Point(2.89, -0.48) }, pointComparer);
            }
        }

        [TestCategory("Intersect")]
        [TestMethod]
        public void TestCirclentersects()
        {

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
                Line line1 = new Line(new Point(0, 0), new Point(10, 10));
                Line line2 = new Line(new Point(0, 0), new Point(100, 100));
                Geometry.GetIntersects(line1, line2);
                Assert.Fail();
            }
            catch (SameShapeException) { }
        }
    }
}
