namespace Breakout.Levels;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DIKUArcade.Math;

/// <summary>
/// Collection holding all <see cref="MapElement"/>'s as parsed from a level configuration.
/// </summary>
public class AsciiMapContainer : IEnumerable<MapElement> {
    private readonly List<MapElement> mapElements;

    /// <summary>
    /// Initializes the collection with a given grid-size
    /// </summary>
    /// <param name="gridSize">Grid-size (number of rows and columns in the level file's ASCII-map)</param>
    public AsciiMapContainer(Vec2I gridSize) {
        mapElements = new List<MapElement>();
        GridSize = gridSize;
    }

    public Vec2I GridSize {
        get;
    }

    public float ColumnWidth => 1.0f / GridSize.X;

    public float RowHeight => 1.0f / GridSize.Y;

    public int Count => mapElements.Count;

    public MapElement this[int inx] {
        get => mapElements[inx];
    }

    public void Add(MapElement element) {
        mapElements.Add(element);
    }

    /// <summary>
    /// Using the current grid's size, transform a <see cref="Vec2I"/> containing grid coordinates to a <see cref="Vec2F"/>
    /// in the range 0 <= (x,y) <= 1, as used by DIKUArcade.
    /// </summary>
    /// <param name="gridPos">The grid position to transform.</param>
    /// <returns>A newly created floating point vector.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the vector is outside the grid.</exception>
    public Vec2F ToDecimalCoordinates(Vec2I gridPos) {
        if (gridPos.X < 0 || gridPos.X > GridSize.X ||
            gridPos.Y < 0 || gridPos.Y > GridSize.Y) {
            throw new ArgumentOutOfRangeException(nameof(gridPos));
        }

        return new Vec2F(gridPos.X * ColumnWidth, gridPos.Y * RowHeight);
    }

    #region IEnumerable<MapElement>
    public IEnumerator<MapElement> GetEnumerator() {
        return new AsciiMapEnumerator(mapElements);
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    /// <summary>
    /// Basic enumerator implementation, inspired by the one in <see cref="DIKUArcade.Entities.EntityContainer"/>
    /// </summary>
    private class AsciiMapEnumerator : IEnumerator<MapElement> {
        private readonly ReadOnlyCollection<MapElement> mapElements;
        private readonly bool disposed = false;
        private int position = -1;

        /// <summary>
        /// Create a new enumerator for the given list of <see cref="MapElement"/>'s.
        /// </summary>
        public AsciiMapEnumerator(List<MapElement> elements) {
            mapElements = elements.AsReadOnly();
        }

        /// <summary>
        /// Move to the next element in the collection.
        /// </summary>
        public bool MoveNext() {
            position++;
            return position < mapElements.Count;
        }

        /// <summary>
        /// Reset the enumerator to the beginning of the collection.
        /// </summary>
        public void Reset() {
            position = -1;
        }

        /// <summary>
        /// Get the current element in the collection.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Get the current element in the collection.
        /// </summary>
        public MapElement Current {
            get {
                try {
                    return mapElements[position];
                } catch (IndexOutOfRangeException) {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Dispose of the enumerator.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing) {
            if (disposing) {
                if (!disposed) {
                    // free unmanaged resources
                }
                // free managed
            }
        }
    }

    #endregion
}
