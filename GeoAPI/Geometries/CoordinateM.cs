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
    /// <c>Coordinate</c>s are two-dimensional points, with an additional M-ordinate.
    /// NTS does not support any operations on the M-ordinate except the basic accessor functions.
    /// </para>
    /// </summary>
    [Serializable]
#pragma warning disable 612,618
    public class CoordinateM : Coordinate
#pragma warning restore 612,618
    {
        /// <summary>
        /// Constructs a <c>Coordinate</c> at (x,y,m).
        /// </summary>
        /// <param name="x">X value.</param>
        /// <param name="y">Y value.</param>
		/// <param name="m">M value.</param>
		public CoordinateM(double x, double y, double? m)
			: base(x, y)
		{
			M = m;
		}
		
        /// <summary>
        ///  Constructs a <c>Coordinate</c> at (0,0,0).
        /// </summary>
        public CoordinateM() : this(0.0, 0.0, null) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,m) values as
        /// <c>other</c>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        [Obsolete]
        public CoordinateM(ICoordinate c) : this(c.X, c.Y, c.M) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,m) values as
        /// <c>other</c>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        public CoordinateM(Coordinate c) : this(c.X, c.Y, c.M) { }

        /// <summary>
        /// Gets/Sets <c>Coordinate</c>s (x,y,m) values.
        /// </summary>
        public override Coordinate CoordinateValue
        {
            get { return this; }
            set
            {
                X = value.X;
                Y = value.Y;
                M = value.M;
            }
        }

		public override double? M { get; set; }

		public override bool HasM
		{
			get { return true; }
		}

        /// <summary>
        /// Compares this object with the specified object for order.
        /// <param name="other"><c>Coordinate</c> with which this <c>Coordinate</c> is being compared.</param>
        /// <returns>
        /// A negative integer, zero, or a positive integer as this <c>Coordinate</c>
        ///         is less than, equal to, or greater than the specified <c>Coordinate</c>.
        /// </returns>
        public override int CompareTo(Coordinate other)
        {
			int ret = base.CompareTo(other);
			if (ret != 0) return ret;
			if (M < other.M)
				return -1;
			if (M > other.M)
				return 1;
			return 0;
		}

        /// <summary>
        /// Returns <c>true</c> if <c>other</c> has the same values for x, y and m.
        /// </summary>
        /// <param name="other"><c>Coordinate</c> with which to do the 3D comparison.</param>
        /// <returns><c>true</c> if <c>other</c> is a <c>Coordinate</c> with the same values for x, y and m.</returns>
        public override bool Equals3D(Coordinate other)
        {
            return (X == other.X) && (Y == other.Y) && (M == other.M);
        }

        /// <summary>
        /// Returns a <c>string</c> of the form <I>(x,y,z)</I> .
        /// </summary>
        /// <returns><c>string</c> of the form <I>(x,y,z)</I></returns>
        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + M + ")";
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
            result = 37 * result + GetHashCode(M.GetValueOrDefault());
// ReSharper restore NonReadonlyFieldInGetHashCode
            return result;
        }

        public override Coordinate Add(Coordinate c)
        {
            return new CoordinateM(X + c.X, Y + c.Y, M + c.M);
        }

        public override Coordinate Divide(Coordinate c)
        {
            return new CoordinateM(X / c.X, Y / c.Y, M / c.M);
        }

        public override Coordinate Divide(double d)
        {
            return new CoordinateM(X / d, Y / d, M / d);
        }

        public override Coordinate Multiply(Coordinate c)
        {
            return new CoordinateM(X * c.X, Y * c.Y, M * c.M);
        }

        public override Coordinate Multiply(double d)
        {
            return new CoordinateM(X * d, Y * d, M * d);
        }

        public override Coordinate Subtract(Coordinate c)
        {
            return new CoordinateM(X - c.X, Y - c.Y, M - c.M);
        }

        public override Ordinates Ordinates { get { return Geometries.Ordinates.XYM; } }

        public override double[] GetOrdinates()
        {
            return new double[] { X, Y, M.GetValueOrDefault() };
        }


    }
}