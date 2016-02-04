using System;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// An object that knows how to build a particular implementation of
    /// <c>ICoordinateSequence</c> from an array of Coordinates.
    /// </summary>
    /// <seealso cref="ICoordinateSequence" />
    public interface ICoordinateSequenceFactory
    {
        /// <summary>
        /// Returns a <see cref="ICoordinateSequence" /> based on the given array; 
        /// whether or not the array is copied is implementation-dependent.
        /// </summary>
        /// <param name="coordinates">A coordinates array, which may not be null nor contain null elements</param>
        /// <returns>A coordinate sequence.</returns>
        ICoordinateSequence Create(Coordinate[] coordinates);

        /// <summary>
        /// Creates a <see cref="ICoordinateSequence" />  which is a copy
        /// of the given <see cref="ICoordinateSequence" />.
        /// This method must handle null arguments by creating an empty sequence.
        /// </summary>
        /// <param name="coordSeq"></param>
        /// <returns>A coordinate sequence</returns>
        ICoordinateSequence Create(ICoordinateSequence coordSeq);

        /// <summary>
        /// Creates a <see cref="ICoordinateSequence" /> of the specified size and ordinates.
        /// For this to be useful, the <see cref="ICoordinateSequence" /> implementation must be mutable.
        /// </summary>
        /// <param name="size">The number of coordinates.</param>
        /// <param name="ordinates">
        /// The ordinates each coordinate has. <see cref="GeoAPI.Geometries.Ordinates.XY"/> is fix, <see cref="GeoAPI.Geometries.Ordinates.Z"/> and <see cref="GeoAPI.Geometries.Ordinates.M"/> can be set.
        /// </param>
        /// <returns>A coordinate sequence.</returns>
        ICoordinateSequence Create(int size, Ordinates ordinates);

        /// <summary>
        /// Gets the Ordinate flags that sequences created by this factory can maximal cope with.
        /// </summary>
        Ordinates Ordinates { get; }
    }
}
