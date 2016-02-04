//=================================================================================================================================
//
// This is part of the Cyrez geometry library source code.
// Copyright (c) 2002 Cyrez Technology Inc. All rights reserved.
//
// File    : BoundingBox.cs
// Author  : Mathieu Gauthier
//		   : Eric Biron
// Date    : December 2002
// Content : 
//
//=================================================================================================================================

using System;
using System.Collections;
using System.Drawing;

namespace Cyrez.Graphics.Geometry
{

    /// <summary>Represents a 2-dimensional bounding box.</summary>
    [Serializable()]
    public class BoundingBox : ICloneable
    {
        double _minX, _minY, _maxX, _maxY;

        /// <summary>Default constructor.</summary>
        public BoundingBox()
        {
            _minX = _minY = double.MaxValue;
            _maxX = _maxY = double.MinValue;
        }

        /// <summary>Constructor with Min and Max values.</summary>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        public BoundingBox(double min, double max)
        {
            _minX = _minY = min;
            _maxX = _maxY = max;
        }

        public BoundingBox(BoundingBox box)
        {
            _minX = box._minX;
            _minY = box._minY;
            _maxX = box._maxX;
            _maxY = box._maxY;
        }

        public BoundingBox(double minX, double minY, double maxX, double maxY)
        {
            _minX = minX;
            _minY = minY;
            _maxX = maxX;
            _maxY = maxY;
        }

        /// <summary>Constructor with Min and Max values.</summary>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        public BoundingBox(Point2D min, Point2D max)
        {
            _minX = min.X;
            _minY = min.Y;
            _maxX = max.X;
            _maxY = max.Y;
        }

        /// <summary>Gets the min point.</summary>
        /// <value>The min point.</value>
        public Point2D Min
        {
            get { return new Point2D(_minX, _minY); }
        }

        /// <summary>Gets the max point.</summary>
        /// <value>The max point.</value>
        public Point2D Max
        {
            get { return new Point2D(_maxX, _maxY); }
        }

        /// <summary>Gets the center of the box.</summary>
        /// <value>The center point.</value>
        /// <remarks>Unoptimized. Calculated on each call.</remarks>
        public Point2D Center
        {
            get { return new Point2D((_minX + _maxY) / 2, (_minY + _maxY) / 2); }
        }

        public double Area
        {
            get { return (_maxX - _minX) * (_maxY - _minY); }
        }

        public bool Inside(BoundingBox box)
        {
            return _minX >= box._minX &&
                _minY >= box._minY &&
                _maxX <= box._maxX &&
                _maxY <= box._maxY;
        }

        public bool Contains(BoundingBox box)
        {
            return box.Inside(this);
        }

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return new BoundingBox(_minX, _minY, _maxX, _maxY);
        }

        /// <summary>Tests if the bounding box is valid.</summary>
        /// <value>True if MIN &lt= MAX.</value>
        public bool IsValid
        {
            get { return _minX <= _maxX && _minY <= _maxY; }
        }

        /// <summary>Gets the width of the box.</summary>
        /// <value>The width.</value>
        public double Width
        {
            get { return _maxX - _minX; }
        }

        /// <summary>Gets the height of the box.</summary>
        /// <value>The height.</value>
        public double Height
        {
            get { return _maxY - _minY; }
        }

        /// <summary>Returns a new point created using the Width and Height of the box.</summary>
        /// <returns>A new point object of (Width, Height). </returns>
        public Point2D ToPoint()
        {
            return new Point2D(this.Width, this.Height);
        }

        /// <summary>Tests if a point lies inside the box.</summary>
        /// <param name="pt">The point to test.</param>
        /// <returns>true if the point is in the box.</returns>
        public bool Contains(Point2D pt)
        {
            return pt.X >= _minX &&
                pt.Y >= _minY &&
                pt.X <= _maxX &&
                pt.Y <= _maxY;
        }

        /// <summary>Tests if a point lies strictly inside the box (does not touch the border).</summary>
        /// <param name="pt">The point to test.</param>
        /// <returns>true if the point is strictly in the box.</returns>
        public bool IsPointStrictlyInside(Point2D pt)
        {
            return pt.X > _minX &&
                pt.Y > _minY &&
                pt.X < _maxX &&
                pt.Y < _maxY;
        }

        /// <summary>Sets the box by copying another one.</summary>
        /// <param name="box">The box to copy.</param>
        public void Set(BoundingBox box)
        {
            _minX = box._minX;
            _minY = box._minY;
            _maxX = box._maxX;
            _maxY = box._maxY;
        }

        /// <summary>Sets the min and max points of the box.</summary>
        /// <param name="min">The min point of the box.</param>
        /// <param name="max">The max point of the box.</param>
        public void Set(Point2D min, Point2D max)
        {
            _minX = min.X;
            _minY = min.Y;
            _maxX = max.X;
            _maxY = max.Y;
        }

        /// <summary>Sets the min and max value of the box (square box).</summary>
        /// <param name="min">The min value of the box.</param>
        /// <param name="max">The max value of the box.</param>
        public void Set(double min, double max)
        {
            _minX = _minY = min;
            _maxX = _maxY = max;
        }

        /// <summary>Gets the intersection between 2 boxes.</summary>
        /// <param name="box">The 2nd bounding box.</param>
        /// <returns>The intersection. null if the dont intersect.</returns>
        public BoundingBox Intersection(BoundingBox box)
        {
            throw new Exception("Not coded yet!");
        }

        public bool Intersect(BoundingBox box)
        {
            return !(box._minX >= _maxX ||
                box._minY >= _maxY ||
                box._maxX <= _minX ||
                box._maxY <= _minY);
        }

        /// <summary>Adjust the box to fit the point.</summary>
        /// <param name="pt">The point.</param>
        public void Adjust(Point2D pt)
        {
            if (pt.X < _minX) _minX = pt.X;
            if (pt.X > _maxX) _maxX = pt.X;
            if (pt.Y < _minY) _minY = pt.Y;
            if (pt.Y > _maxY) _maxY = pt.Y;
        }

        public void Adjust(PointF pt)
        {
            if (pt.X < _minX) _minX = pt.X;
            if (pt.X > _maxX) _maxX = pt.X;
            if (pt.Y < _minY) _minY = pt.Y;
            if (pt.Y > _maxY) _maxY = pt.Y;
        }

        /// <summary>Adjust the box to fit another bounding box,</summary>
        /// <param name="box">The bounding box.</param>
        public void Adjust(BoundingBox box)
        {
            if (box._minX < _minX) _minX = box._minX;
            if (box._minY < _minY) _minY = box._minY;
            if (box._maxX > _maxX) _maxX = box._maxX;
            if (box._maxY > _maxY) _maxY = box._maxY;
        }

        /// <summary>Get the box that is the union with another bounding box,</summary>
        /// <param name="box">The bounding box.</param>
        public BoundingBox Union(BoundingBox box)
        {
            BoundingBox unionBox = new BoundingBox(this);
            unionBox.Adjust(box);
            return unionBox;
        }

        /// <summary>Reset the box.</summary>
        /// <value>Sets the min and max to +/- double.MaxValue.</value>
        public void Reset()
        {
            _minX = _minY = double.MaxValue;
            _maxX = _maxY = double.MinValue;
        }

        public override string ToString()
        {
            return string.Format("minX={0}, minY={1}, maxX={2}, maxY={3}", _minX, _minY, _maxX, _maxY);
        }

        public double MinX
        {
            get { return _minX; }
        }

        public double MinY
        {
            get { return _minY; }
        }

        public double MaxX
        {
            get { return _maxX; }
        }

        public double MaxY
        {
            get { return _maxY; }
        }
    }
}