namespace Breakout.Levels;

using System;
using System.Collections.Generic;
using Breakout.Utility;
using DIKUArcade.Graphics;

/// <summary>
/// Data from the legend field of a level file. It is essentially just a dictionary, but has modified
/// setting functionality, creating value objects using file-paths to images.
/// </summary>
public class LegendData {
    private readonly Dictionary<char, IBlockImagePair> namedImages;

    public LegendData() {
        namedImages = new Dictionary<char, IBlockImagePair>();
    }

    public IBlockImagePair this[char name] {
        get {
            return namedImages[name];
        }
    }
    public int Count => namedImages.Count;
    public bool ContainsKey(char name) {
        return namedImages.ContainsKey(name);
    }

    /// <summary>
    /// Add a legend to the dictionary by loading an image file from disc.
    /// </summary>
    /// <param name="name">Name of the legend element</param>
    /// <param name="filename">Filename of the image to add.</param>
    /// <exception cref="InvalidOperationException">If the entry already exists in the map</exception>
    public void AddLegend(char name, string filename) {
        if (ContainsKey(name)) {
            throw new InvalidOperationException($"Legend map already contains element '{name}'");
        }
        namedImages[name] = new BlockImagePair(filename);
    }

    /// <summary>
    /// Implementation of <see cref="IBlockImagePair"/>.
    /// We implement the interface as a nested class in order to have clients of <see cref="LegendData"/> obey the Dependency Inversion Principle.
    /// </summary>
    public class BlockImagePair : IBlockImagePair {
        private readonly IBaseImage healthyBlockImage;
        private readonly IBaseImage damagedBlockImage;
        /// <summary>
        /// Given a filename, load two images (1) the normal/healthy block, and (2) the damaged one whose name is postfixed with "-damaged"
        /// </summary>
        /// <param name="filename">Filename of the healthy block's image.</param>
        public BlockImagePair(string filename) {
            var damagedName = GetDamagedFileName(filename);

            healthyBlockImage = DIKUArcadeExtensions.LoadImageFromAssets(filename);
            damagedBlockImage = DIKUArcadeExtensions.LoadImageFromAssets(damagedName);
        }
        public IBaseImage HealthyBlockImage => healthyBlockImage;
        public IBaseImage DamagedBlockImage => damagedBlockImage;
        private static string GetDamagedFileName(string filename) {
            var ext = Path.GetExtension(filename);
            var basefilename = Path
                .ChangeExtension(filename, null);
            return Path.ChangeExtension($"{basefilename}-damaged", ext);
        }
    }
}
