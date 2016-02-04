using System;

#if !WINDOWS_PHONE
using BitConverter = System.BitConverter;
#else

using BitConverter = GeoAPI.BitConverterEx;

#endif

namespace GeoAPI.Geometries
{
    /// <summary>
    /// A lightweight class used to store coordinates on the 3-dimensional Cartesian plane.
    /// <para>
    /// It is distinct from <see cref="IPoint"/>, which is a subclass of <see cref="IGeometry"/>.
    /// Unlike objects of type <see cref="IPoint"/> (which contain additional
    /// information such as an envelope, a precision model, and spatial reference
    /// system information), a <c>Coordinate</c> only contains ordinate values
    /// and propertied.
    /// </para>
    /// <para>
    /// <c>Coordinate</c>s are three-dimensional points
    /// </para>
    /// </summary>
    [Serializable]
#pragma warning disable 612,618
    public class CoordinateZ : Coordinate
#pragma warning restore 612,618
    {
        /// <summary>
        /// Constructs a <c>Coordinate</c> at (x,y,z).
        /// </summary>
        /// <param name="x">X value.</param>
        /// <param name="y">Y value.</param>
		/// <param name="z">Z value.</param>
        public CoordinateZ(double x, double y, double z)
			:base (x, y)
        {
            Z = z;
        }

	        /// <summary>
        ///  Constructs a <c>Coordinate</c> at (0,0,0).
        /// </summary>
        public CoordinateZ() : this(0.0, 0.0, 0.0) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,z) values as
        /// <c>other</c>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        [Obsolete]
        public CoordinateZ(ICoordinate c) : this(c.X, c.Y, c.Z) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,z) values as
        /// <c>other</c>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        public CoordinateZ(Coordinate c) : this(c.X, c.Y, c.Z) { }

        /// <summary>
        /// Gets/Sets <c>Coordinate</c>s (x,y,z) values.
        /// </summary>
        public override Coordinate CoordinateValue
        {
            get { return this; }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

		public override  double Z { get; set; }

		public override bool HasZ
		{
			get { return true; }
		}
        

        /// <summary>
        /// Compares this object with the specified object for order.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// Returns
        ///   -1  : this.x lowerthan other.x || ((this.x == other.x) AND (this.y lowerthan other.y))
        ///    0  : this.x == other.x AND this.y = other.y
        ///    1  : this.x greaterthan other.x || ((this.x == other.x) AND (this.y greaterthan other.y))
        /// </summary>
        /// <param name="o"><c>Coordinate</c> with which this <c>Coordinate</c> is being compared.</param>
        /// <returns>
        /// A negative integer, zero, or a positive integer as this <c>Coordinate</c>
        ///         is less than, equal to, or greater than the specified <c>Coordinate</c>.
        /// </returns>
        public int CompareTo(object o)
        {
            var other = (Coordinate)o;
            return CompareTo(other);
        }

        /// <summary>
        /// Compares this object with the specified object for order.
        /// Returns
        ///   -1  : this.x lowerthan other.x || ((this.x == other.x) AND (this.y lowerthan other.y))
        ///    0  : this.x == other.x AND this.y = other.y
        ///    1  : this.x greaterthan other.x || ((this.x == other.x) AND (this.y greaterthan other.y))
        /// </summary>
        /// <param name="other"><c>Coordinate</c> with which this <c>Coordinate</c> is being compared.</param>
        /// <returns>
        /// A negative integer, zero, or a positive integer as this <c>Coordinate</c>
        ///         is less than, equal to, or greater than the specified <c>Coordinate</c>.
        /// </returns>
		public override int CompareTo(Coordinate other)
		{
			int ret = base.CompareTo(other);
			if (ret != 0) return ret;
			if (Z < other.Z)
				return -1;
			if (Z > other.Z)
				return 1;
			return 0;
		}

        /// <summary>
        /// Returns a <c>string</c> of the form <I>(x,y,z)</I> .
        /// </summary>
        /// <returns><c>string</c> of the form <I>(x,y,z)</I></returns>
        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }

        /// <summary>
        /// Computes the 3-dimensional Euclidean distance to another location.
        /// </summary>
        /// <param name="p"><c>Coordinate</c> with which to do the distance comparison.</param>
        /// <returns>the 3-dimensional Euclidean distance between the locations</returns>
        public override double Distance(Coordinate p)
        {
            var dx = X - p.X;
            var dy = Y - p.Y;
			var dz = Z - p.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// Gets a hashcode for this coordinate.
        /// </summary>
        /// <returns>A hashcode for this coordinate.</returns>
        public override int GetHashCode()
        {
            var result = 17;
// ReSharper disable NonReadonlyFieldInGetHashCode
            result = 37 * result + GetHashCode(X);
            result = 37 * result + GetHashCode(Y);
            result = 37 * result + GetHashCode(Z);
// ReSharper restore NonReadonlyFieldInGetHashCode
            return result;
        }

        public override Coordinate Add(Coordinate c)
        {
            return new CoordinateZ(X + c.X, Y + c.Y, Z + c.Z);
        }

        public override Coordinate Divide(Coordinate c)
        {
            return new CoordinateZ(X / c.X, Y / c.Y, Z / c.Z);
        }

        public override Coordinate Divide(double d)
        {
            return new CoordinateZ(X / d, Y / d, Z / d);
        }

        public override Coordinate Multiply(Coordinate c)
        {
            return new CoordinateZ(X * c.X, Y * c.Y, Z * c.Z);
        }

        public override Coordinate Multiply(double d)
        {
            return new CoordinateZ(X * d, Y * d, Z * d);
        }

        public override Coordinate Subtract(Coordinate c)
        {
            return new CoordinateZ(X - c.X, Y - c.Y, Z - c.Z);
        }

        public override Ordinates Ordinates { get { return Geometries.Ordinates.XYZ; } }

        public override double[] GetOrdinates()
        {
            return new double[] { X, Y, Z};
        }

        public override Coordinate WithMeasure(double m)
        {
            return new CoordinateZM(X, Y, Z, m);
        }

    }
}