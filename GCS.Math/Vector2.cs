using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCS.Math
{
    public struct Vector2
    {
        public float X;
        public float Y;
        private static readonly Vector2 _unitX = new Vector2(1f, 0f);
        private static readonly Vector2 _unitY = new Vector2(0f, 1f);
        private static readonly Vector2 _unit = new Vector2(1f, 1f);
        private static readonly Vector2 _zero = new Vector2(0f, 0f);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public static Vector2 One => _unit;
        public static Vector2 Zero => _zero;
        public static Vector2 UnitX => _unitX;
        public static Vector2 UnitY => _unitY;


        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        public static Vector2 operator -(Vector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        public static Vector2 operator *(Vector2 left, float right)
        {
            left.X *= right;
            left.Y *= right;
            return left;
        }

        public static Vector2 operator *(float left, Vector2 right)
        {
            right.X *= left;
            right.Y *= left;
            return right;
        }

        public static Vector2 operator /(float left, Vector2 right)
        {
            right.X /= left;
            right.Y /= left;
            return right;
        }

        public static Vector2 operator /(Vector2 left, float right)
        {
            left.X /= right;
            left.Y /= right;
            return left;
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return (left.X == right.X) && (left.Y == right.Y);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        public float Length()
        {
            return (float)System.Math.Sqrt(X * X + Y * Y);
        }

        public static float Distance(Vector2 left, Vector2 right)
        {
            return (float)System.Math.Sqrt((left.X - right.X) * (left.X - right.X) + (left.Y - right.Y) * (left.Y - right.Y));
        }

        public static Vector2 Normalize(Vector2 value)
        {
            return new Vector2(value.X / value.Length(), value.Y / value.Length());
        }

        public void Normalize()
        {
            X /= Length();
            Y /= Length();
        }

        public static float Dot(Vector2 left, Vector2 right)
        {
            return left.X * right.X + left.Y * right.Y;
        }

    }
}
