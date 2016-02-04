//=================================================================================================================================
//
// This is part of the Cyrez geometry library source code.
// Copyright (c) 2002 Cyrez Technology Inc. All rights reserved.
//
// File    : Point.vb
// Author  : Mathieu Gauthier
// Date    : December 2002
// Content : 
//
//=================================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;

namespace Cyrez.Graphics.Geometry
{

    /// <summary>Represents a two-dimensional point.</summary>
    [Serializable()]
    public class Point2D
    {

        #region variables

        private double _x;
        private double _y;


        public static readonly Point2D empty = new Point2D();
        #endregion

        #region constructors



        //=============================================================================================================================
        /// <summary>Copy constructor.</summary>
        /// <param name="point">The source point.</param>
        //=============================================================================================================================

        public Point2D(Point2D point)
        {
            _x = point._x;
            _y = point._y;
        }

        //=============================================================================================================================
        /// <summary>Contructor with x,y coordiates</summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        //=============================================================================================================================

        public Point2D(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Point2D()
        {
            _x = _y = 0;
        }
        #endregion

        #region properties

        //=============================================================================================================================
        /// <summary>Gets or sets the X component of the point.</summary>
        /// <value>The X component.</value>
        //=============================================================================================================================

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        //=============================================================================================================================
        /// <summary>Gets or sets the Y component of the point.</summary>
        /// <value>The Y component.</value>
        //=============================================================================================================================

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        //=============================================================================================================================
        /// <summary>Gets the length of the vector.</summary>
        /// <value>The length.</value>
        //=============================================================================================================================

        public double VectorLength
        {
            get
            {
                return Math.Sqrt(_x * _x + _y * _y);
            }
            set
            {
                double len = this.VectorLength;

                _x = (_x / len) * value;
                _y = (_y / len) * value;
            }
        }

        //=============================================================================================================================
        /// <summary>Gets the length of the point.</summary>
        /// <value>The length.</value>
        //=============================================================================================================================

        public double Length
        {
            get
            {
                return 0.0;
            }
        }

        #endregion

        #region methods

        //=============================================================================================================================
        /// <summary>Calculate the dot product of 2 points.</summary>
        /// <param name="point">The 2nd point.</param>
        /// <returns>The dot product.</returns>
        //=============================================================================================================================

        public double Dot(Point2D point)
        {
            return (_x * point._x) + (_y * point._y);
        }

        //=============================================================================================================================
        /// <summary>Normalizes the vector (Sets the length to 1.0).</summary>
        //=============================================================================================================================

        public void Normalize()
        {
            VectorLength = 1.0;
        }

        //=============================================================================================================================
        /// <summary>Clone the object.</summary>
        //=============================================================================================================================
        public object Clone()
        {
            return new Point2D(this);
        }


        //=============================================================================================================================
        /// <summary>Addition operator.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The sum of the two points.</returns>
        //=============================================================================================================================

        public static Point2D operator +(Point2D pt1, Point2D pt2)
        {
            return new Point2D(pt1._x + pt2._x, pt1._y + pt2._y);
        }

        //=============================================================================================================================
        /// <summary>Subtraction operator.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The difference between the two points.</returns>
        //=============================================================================================================================

        public static Point2D operator -(Point2D pt1, Point2D pt2)
        {
            return new Point2D(pt1._x - pt2._x, pt1._y - pt2._y);
        }

        //=============================================================================================================================
        /// <summary>Multiplication operator.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The result of the multiplication.</returns>
        //=============================================================================================================================

        public static Point2D operator *(Point2D pt1, Point2D pt2)
        {
            return new Point2D(pt1._x * pt2._x, pt1._y * pt2._y);
        }

        //=============================================================================================================================
        /// <summary>Division operator.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The result of the division.</returns>
        //=============================================================================================================================

        public static Point2D operator /(Point2D pt1, Point2D pt2)
        {
            return new Point2D(pt1._x / pt2._x, pt1._y / pt2._y);
        }

        //=============================================================================================================================
        /// <summary>Addition operator with a constant.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The sum of the point and the constant.</returns>
        //=============================================================================================================================

        public static Point2D operator +(Point2D pt1, double value)
        {
            return new Point2D(pt1._x + value, pt1._y + value);
        }

        //=============================================================================================================================
        /// <summary>Subtraction operator with a constant.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The difference between the point and the constant.</returns>
        //=============================================================================================================================

        public static Point2D operator -(Point2D pt1, double value)
        {
            return new Point2D(pt1._x - value, pt1._y - value);
        }

        //=============================================================================================================================
        /// <summary>Multiplication operator with a constant.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The point scaled by the constant.</returns>
        //=============================================================================================================================

        public static Point2D operator *(Point2D pt1, double value)
        {
            return new Point2D(pt1._x * value, pt1._y * value);
        }

        //=============================================================================================================================
        /// <summary>Division operator with a constant.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>The point scaled by the constant.</returns>
        //=============================================================================================================================

        public static Point2D operator /(Point2D pt1, double value)
        {
            return new Point2D(pt1._x / value, pt1._y / value);
        }

        //=============================================================================================================================
        /// <summary>Tests if two points are equal.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>If the two points are equal true, otherwise false.</returns>
        //=============================================================================================================================

        public static bool operator ==(Point2D pt1, Point2D pt2)
        {
            if (object.Equals(pt1, null) && object.Equals(pt2, null)) return true;
            if (object.Equals(pt1, null) || object.Equals(pt2, null)) return false;

            return pt1._x == pt2._x && pt1._y == pt2._y;
        }

        //=============================================================================================================================
        /// <summary>Tests if two points are different.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>If the two points are different true, otherwise false.</returns>
        //=============================================================================================================================

        public static bool operator !=(Point2D pt1, Point2D pt2)
        {
            return !(pt1 == pt2);
        }

        //=============================================================================================================================
        /// <summary>Tests if one points is lower than the other one.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>If the two points are different true, otherwise false.</returns>
        //=============================================================================================================================

        public static bool operator <(Point2D pt1, Point2D pt2)
        {
            return pt1._x < pt2._x &&
                pt1._y < pt2._y;
        }

        //=============================================================================================================================
        /// <summary>Tests if one points is lower than the other one or equal.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>If the two points are different true, otherwise false.</returns>
        //=============================================================================================================================

        public static bool operator <=(Point2D pt1, Point2D pt2)
        {
            return pt1._x <= pt2._x &&
                pt1._y <= pt2._y;
        }

        //=============================================================================================================================
        /// <summary>Tests if one points is higher than the other one.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>If the two points are different true, otherwise false.</returns>
        //=============================================================================================================================

        public static bool operator >(Point2D pt1, Point2D pt2)
        {
            return pt1._x > pt2._x &&
                pt1._y > pt2._y;
        }
        //=============================================================================================================================
        /// <summary>Tests if one points is higher than the other one or equal.</summary>
        /// <param name="pt1">The first point.</param>
        /// <param name="pt2">The second point.</param>
        /// <returns>If the two points are different true, otherwise false.</returns>
        //=============================================================================================================================

        public static bool operator >=(Point2D pt1, Point2D pt2)
        {
            return pt1._x >= pt2._x &&
                pt1._y >= pt2._y;
        }

        //=============================================================================================================================
        /// <summary>Return a new point with the sum of a point and the current point. </summary>
        /// <param name="point">The point to add.</param>
        /// <returns>The result of the addition.</returns>
        /// <remarks>The current point remains unchanged.</remarks>
        //=============================================================================================================================

        public Point2D Add(Point2D point)
        {
            return new Point2D(_x + point._x, _y + point._y);
        }

        //=============================================================================================================================
        /// <summary>Return a new point with constant added to the current point.</summary>
        /// <param name="value">The constant to add.</param>
        /// <returns>The result of the addition.</returns>
        /// <remarks>The current point remains unchanged.</remarks>
        //=============================================================================================================================

        public Point2D Add(double value)
        {
            return new Point2D(_x + value, _y + value);
        }

        //=============================================================================================================================
        /// <summary>Return the current point added with a point. </summary>
        /// <param name="point">The constant to add.</param>
        /// <returns>The result of the addition.</returns>
        /// <remarks>The current point is modified.</remarks>
        //=============================================================================================================================

        public Point2D SelfAdd(double value)
        {
            _x += value;
            _y += value;
            return this;
        }

        //=============================================================================================================================
        /// <summary>Return the current point added with a constant. </summary>
        /// <param name="point">The Constant to add.</param>
        /// <returns>The result of the addition.</returns>
        /// <remarks>The current point is modificed.</remarks>
        //=============================================================================================================================

        public Point2D SelfAdd(Point2D point)
        {
            _x += point._x;
            _y += point._y;
            return this;
        }


        //=============================================================================================================================
        /// <summary>Subtracts a point from the current point.</summary>
        /// <param name="point">The point to subtract.</param>
        /// <returns>The result of the subtraction.</returns>
        /// <remarks>The current point remains unchanged.</remarks>
        //=============================================================================================================================

        public Point2D Subtract(Point2D point)
        {
            return new Point2D(_x - point._x, _y - point._y);
        }

        //=============================================================================================================================
        /// <summary>Subtracts a constant from the current point.</summary>
        /// <param name="value">The constant to subtract.</param>
        /// <returns>The result of the subtraction.</returns>
        /// <remarks>The current point remains unchanged.</remarks>
        //=============================================================================================================================

        public Point2D Subtract(double value)
        {
            return new Point2D(_x - value, _y - value);
        }

        //=============================================================================================================================
        /// <summary>Multiplies the current point by another point.</summary>
        /// <param name="point">The point to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        /// <remarks>The current point remains unchanged.</remarks>
        //=============================================================================================================================

        public Point2D Multiply(Point2D point)
        {
            return new Point2D(_x * point._x, _y * point._y);
        }

        //=============================================================================================================================
        /// <summary>Multiplies the current point by a constant.</summary>
        /// <param name="value">The constant to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        /// <remarks>The current point remains unchanged.</remarks>
        //=============================================================================================================================

        public Point2D Multiply(double value)
        {
            return new Point2D(_x * value, _y * value);
        }

        //=============================================================================================================================
        /// <summary>Divides the current point by another point.</summary>
        /// <param name="point">The point to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <remarks>The current point remains unchanged.</remarks>
        //=============================================================================================================================

        public Point2D Divide(Point2D pt)
        {
            return new Point2D(_x / pt._x, _y / pt._y);
        }


        //=============================================================================================================================
        /// <summary>Divides the current point by a constant.</summary>
        /// <param name="value">The constant to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <remarks>The current point remains unchanged.</remarks>
        //=============================================================================================================================

        public Point2D Divide(double value)
        {
            return new Point2D(_x / value, _y / value);
        }


        //=============================================================================================================================
        /// <summary>Divides the current point by a constant.</summary>
        /// <param name="value">The constant to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <remarks>The current point is changed.</remarks>
        //=============================================================================================================================

        public Point2D SelfDivide(double value)
        {
            _x /= value;
            _y /= value;
            return this;
        }

        //=============================================================================================================================
        /// <summary>Divides the current point by another point.</summary>
        /// <param name="point">The point to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <remarks>The current point is changed.</remarks>
        //=============================================================================================================================

        public Point2D SelfDivide(Point2D pt)
        {
            _x /= pt._x;
            _y /= pt._y;
            return this;
        }

        //=============================================================================================================================
        /// <summary>Multiply the current point with a constant.</summary>
        /// <param name="value">The constant to Multiply with.</param>
        /// <returns>The result of the Multiplication.</returns>
        /// <remarks>The current point is changed.</remarks>
        //=============================================================================================================================

        public Point2D SelfMultiply(double value)
        {
            _x *= value;
            _y *= value;
            return this;
        }

        //=============================================================================================================================
        /// <summary>Multiply the current point with another point.</summary>
        /// <param name="point">The point to Multiply with.</param>
        /// <returns>The result of the Multiplication.</returns>
        /// <remarks>The current point is changed.</remarks>
        //=============================================================================================================================

        public Point2D SelfMultiply(Point2D pt)
        {
            _x *= pt._x;
            _y *= pt._y;
            return this;
        }

        //=============================================================================================================================
        /// <summary>Subtracts a constant from the current point.</summary>
        /// <param name="value">The constant to subtract.</param>
        /// <returns>The current point.</returns>
        /// <remarks>The current point is changed.</remarks>
        //=============================================================================================================================

        public Point2D SelfSubtract(Point2D pt)
        {

            _x -= pt._x;
            _y -= pt._y;
            return this;
        }

        //=============================================================================================================================
        /// <summary>Subtracts a constant from the current point.</summary>
        /// <param name="value">The constant to subtract.</param>
        /// <returns>The current point.</returns>
        /// <remarks>The current point is changed.</remarks>
        //=============================================================================================================================

        public Point2D SelfSubtract(double value)
        {
            _x -= value;
            _y -= value;
            return this;
        }

        //=============================================================================================================================
        /// <summary>Determines whether instances of the same type of an evidence object are equivalent.</summary>
        /// <param name="obj">An object of same type as the current evidence object. </param>
        /// <returns>true if the two instances are equivalent; otherwise, false.</returns>
        //=============================================================================================================================

        public override bool Equals(object obj)
        {
            return (obj is Point2D) && (this == (Point2D)obj);
        }

        //=============================================================================================================================
        /// <summary>Gets the hash code of the current point.</summary>
        /// <returns>The hash code.</returns>
        //=============================================================================================================================

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }

        //=============================================================================================================================
        /// <summary>Converts the numeric value of this instance to its equivalent string.</summary>
        /// <returns>The value of this instance.</returns>
        //=============================================================================================================================

        public override string ToString()
        {
            return "(" + _x + "," + _y + ")";
        }

        //=============================================================================================================================
        /// <summary>Translates the current geometry.</summary>
        /// <param name="offset">The point describing the translation to perform.</param>
        /// <returns>The current geometry.</returns>
        //=============================================================================================================================

        public Point2D Translate(Point2D offset)
        {
            _x += offset._x;
            _y += offset._y;
            return this;
        }

        //=============================================================================================================================
        /// <summary>Rotates the current point object around the origin.</summary>
        /// <param name="angle">The angle in degrees.</param>
        //=============================================================================================================================

        public void Rotate(double angle)
        {
            RotateRadians(Util.ToRadians(angle));
        }

        //=============================================================================================================================
        /// <summary>Rotates the current point object around a specific point.</summary>
        /// <param name="center">The center of the rotation.</param>
        /// <param name="angle">The angle in degrees.</param>
        //=============================================================================================================================

        public void Rotate(Point2D center, double angle)
        {
            RotateRadians(center, Util.ToRadians(angle));
        }

        //=============================================================================================================================
        /// <summary>Rotates the current point object around the origin.</summary>
        /// <param name="angle">The angle in radians.</param>
        //=============================================================================================================================

        public void RotateRadians(double angle)
        {
            Point2D temp = new Point2D(this);

            _x = temp._x * Math.Cos(angle) - temp._y * Math.Sin(angle);
            _y = temp._x * Math.Sin(angle) + temp._y * Math.Cos(angle);
        }

        //=============================================================================================================================
        /// <summary>Rotates the current point object around a specific point.</summary>
        /// <param name="center">The center of the rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        //=============================================================================================================================

        public void RotateRadians(Point2D center, double angle)
        {
            Point2D temp = this;
            temp -= center;
            temp.RotateRadians(angle);
            temp += center;
            _x = temp.X;
            _y = temp.Y;
        }

        //=============================================================================================================================
        /// <summary>Gets the bounding box of the point object.</summary>
        /// <value>The bounding box.</value>
        //=============================================================================================================================

        public BoundingBox BoundingBox
        {
            get
            {
                return new BoundingBox(this, this);
            }
        }

        //=============================================================================================================================
        /// <summary>Gets the angle of a vector from the current point to the origin.</summary>
        /// <returns>The angle value in degrees.</returns>
        //=============================================================================================================================
        public double Angle
        {
            get
            {
                return Util.ToDegrees(Math.Atan2(_y, _x));
            }
        }

        //=============================================================================================================================
        /// <summary>Gets the angle of a vector from the current point to the origin.</summary>
        /// <returns>The angle value in radians.</returns>
        //=============================================================================================================================
        public double AngleRadian
        {
            get
            {
                return Math.Atan2(_y, _x);
            }
        }

        //=====================================================================
        /// 
        /// <summary>
        /// Scales the primitive.
        /// </summary>
        /// <param name="factor">The scale factor to apply.</param>
        /// 
        //=====================================================================

        public void Scale(double factor)
        {
            this._x *= factor;
            this._y *= factor;
        }

        //=====================================================================
        /// 
        /// <summary>
        /// Scales the shape primitive to a base point.
        /// </summary>
        /// <param name="factor">The scale factor.</param>
        /// <param name="pt">The base point.</param>
        /// 
        //=====================================================================

        public void Scale(double factor, Point2D pt)
        {
            Point2D pt2 = this - pt;
            pt2 *= factor;
            this._x = pt2._x + pt._x;
            this._y = pt2._y + pt._y;
        }

        #endregion

        //public static implicit operator Cyrez.Graphics.Geometry3D.Point3D(Point2D point)
        //{
        //    return new Cyrez.Graphics.Geometry3D.Point3D(point._x, point._y, 0);
        //}

        //=============================================================================================================================
        //
        // Description  : Tests to see if the points in the collection are in clockwise order.
        // Parameters   : (none)
        // return value : True if they are clockwise.
        // Remarks      : Unoptimized, calculated on every call.
        //
        //=============================================================================================================================

        public static bool IsClockwise(IList<Point2D> list)
        {
            int leftmost = 0;
            double smallest = double.MaxValue;

            //First we find the leftmost vertex...
            for (int i = 0; i <= list.Count - 1; i++)
            {
                if (list[i].X < smallest)
                {
                    leftmost = i;
                    smallest = list[i].X;
                }
            }

            Point2D pt1;
            Point2D pt2 = list[leftmost];
            Point2D pt3;

            //if the first and last vertex are the same (closed polygon) we take the previous vertex.
            if ((leftmost == 0) && (list[0] == list[list.Count - 1]))
            {
                pt1 = list[list.Count - 2];
            }
            else
            {
                pt1 = (leftmost == 0) ? list[list.Count - 1] : list[leftmost - 1];
            }

            pt3 = (leftmost == (list.Count - 1)) ? list[0] : list[leftmost + 1];

            //if ( the dot product is smaller than zero, it is clockwise
            return (((pt2.X - pt1.X) * (pt3.Y - pt2.Y)) - ((pt3.X - pt2.X) * (pt2.Y - pt1.Y))) < 0.0;
        }


        public static implicit operator System.Windows.Point(Point2D p)
        {
            return new System.Windows.Point(p._x, p._y);
        }
    }
}
 