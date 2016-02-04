using System;

#if !WINDOWS_PHONE
using BitConverter = System.BitConverter;
#else

using BitConverter = GeoAPI.BitConverterEx;

#endif

namespace GeoAPI.Geometries
{
    /// <summary>
    /// A lightweight class used to store coordinates on the 2-dimensional Cartesian plane.
    /// <para>
    /// It is distinct from <see cref="IPoint"/>, which is a subclass of <see cref="IGeometry"/>.
    /// Unlike objects of type <see cref="IPoint"/> (which contain additional
    /// information such as an envelope, a precision model, and spatial reference
    /// system information), a <c>Coordinate</c> only contains ordinate values
    /// and propertied.
    /// </para>
    /// <para>
    /// <c>Coordinate</c>s are two-dimensional points, with an additional Z-ordinate.
    /// NTS does not support any operations on the Z-ordinate except the basic accessor functions.
    /// If an Z-ordinate value is not specified or not defined,
    /// constructed coordinates have a Z-ordinate of <code>NaN</code>
    /// (which is also the value of <see cref="NullOrdinate"/>).
    /// </para>
    /// </summary>
    [Serializable]
#pragma warning disable 612,618
    public class CoordinateZM : CoordinateZ
#pragma warning restore 612,618
    {
        /// <summary>
        /// Constructs a <c>Coordinate</c> at (x,y,z).
        /// </summary>
        /// <param name="x">X value.</param>
        /// <param name="y">Y value.</param>
		/// <param name="z">Z value.</param>
		/// <param name="m">M value.</param>
        public CoordinateZM(double x, double y, double z, double? m)
			:base(x,y,z)
        {
			M = m;
        }

		/// <summary>
        ///  Constructs a <c>Coordinate</c> at (0,0,0, null).
        /// </summary>
		public CoordinateZM() : this(0.0, 0.0, 0.0, null) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,z,m) values as
        /// <c>other</c>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        [Obsolete]
        public CoordinateZM(ICoordinate c) : this(c.X, c.Y, c.Z, c.M) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,z,m) values as
        /// <c>other</c>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        public CoordinateZM(Coordinate c) : this(c.X, c.Y, c.Z, c.M) { }


        /// <summary>
        /// Gets/Sets <c>Coordinate</c>s (x,y,z,m) values.
        /// </summary>
        public override Coordinate CoordinateValue
        {
            get { return this; }
            set
            {
				if(value.Ordinates != Ordinates) throw new ArgumentException("value");
				X = value.X;
                Y = value.Y;
                Z = value.Z;
				M = value.M;
            }
        }

		public override double? M { get; set; }

		public override bool HasM
		{
			get { return true; }
		}

        /// <summary>
        /// Returns a <c>string</c> of the form <I>(x,y,z)</I> .
        /// </summary>
        /// <returns><c>string</c> of the form <I>(x,y,z)</I></returns>
        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ", " + M + ")";
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
            result = 37 * result + GetHashCode(M.GetValueOrDefault());
// ReSharper restore NonReadonlyFieldInGetHashCode
            return result;
        }

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

        public override Coordinate Add(Coordinate c)
        {
            throw new NotImplementedException();
        }

        public override Coordinate Divide(Coordinate c)
        {
            throw new NotImplementedException();
        }

        public override Coordinate Divide(double d)
        {
            throw new NotImplementedException();
        }

        public override Coordinate Multiply(Coordinate c)
        {
            throw new NotImplementedException();
        }

        public override Coordinate Multiply(double d)
        {
            throw new NotImplementedException();
        }

        public override Coordinate Subtract(Coordinate c)
        {
            throw new NotImplementedException();
        }

        public override Ordinates Ordinates { get { return Geometries.Ordinates.XYZM; } }

        public override double[] GetOrdinates()
        {
            return new double[] { X, Y, Z, M.GetValueOrDefault() };
        }

    }
}