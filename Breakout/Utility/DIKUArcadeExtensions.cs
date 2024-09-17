namespace Breakout.Utility;

using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Utilities;

/// <summary>
/// Utility class providing extension methods for classes exposed by the DIKUArcade API.
/// </summary>
public static class DIKUArcadeExtensions {
    /// <summary>
    /// Given a <see cref="GameEvent"/> try to parse its message field into a <see cref="KeyboardAction"/> value.
    /// </summary>
    /// <param name="gameEvent">The event in question.</param>
    /// <returns>A <see cref="KeyboardAction"/> value</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the message isn't able to be parsed.</exception>
    public static KeyboardAction ActionType(this GameEvent gameEvent) {
        if (!Enum.TryParse<KeyboardAction>(gameEvent.Message, out var action)) {
            var message = $"{nameof(gameEvent.Message)} didn't contain a value representing {nameof(KeyboardAction)}!";
            message += $" Expected '{KeyboardAction.KeyPress}' or '{KeyboardAction.KeyRelease}', got '{gameEvent.Message}'";
            throw new ArgumentOutOfRangeException(message);
        }
        return action;
    }

    /// <summary>
    /// Load an image from the Assets/Images folder.
    /// </summary>
    /// <param name="fileName">Name of the image file.</param>
    /// <returns>An <see cref="IBaseImage"/> object representing the image.</returns>
    /// <exception cref="FileNotFoundException">If the file doesn't exist in the Assets/Images folder.</exception>
    public static IBaseImage LoadImageFromAssets(string fileName) {
        var path = Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", fileName);
        if (!File.Exists(path)) {
            throw new FileNotFoundException($"No file named '{fileName}' in {Path.Combine(FileIO.GetProjectPath(), "Assets", "Images")}");
        }
        return new Image(path);
    }

    /// <summary>
    /// Load a set of image strides from the Assets/Images folder
    /// </summary>
    /// <param name="fileName">The file contianing the strides</param>
    /// <param name="strides">The number of strides to partition</param>
    /// <param name="intervalMs">Interval between strides changing (default: 80)</param>
    /// <returns>An <see cref="IBaseImage"/> representing the strides.</returns>
    /// <exception cref="FileNotFoundException">If the file given by <see cref="fileName"/> doesn't exist in the Assets/Images folder.</exception>
    public static IBaseImage LoadStridesFromAssets(string fileName, int strides, int intervalMs = 80) {
        var path = Path.Combine(FileIO.GetProjectPath(), "Images", fileName);
        if (!File.Exists(path)) {
            throw new FileNotFoundException($"No file named '{fileName}' in {Path.Combine(FileIO.GetProjectPath(), "Assets", "Images")}");
        }
        var images = ImageStride.CreateStrides(strides, path);
        return new ImageStride(intervalMs, images);
    }

    /// <summary>
    /// Compute the unit vector (|u| = 1).
    /// </summary>
    /// <param name="vec">The vector in question</param>
    /// <returns>A vector of length 1 in the same direction af vec</returns>
    public static Vec2F UnitVector(this Vec2F vec) {
        const float EPSILON = 1e-6f;
        var length = vec.Length() + EPSILON;
        return vec / (float) length;
    }

    /// <summary>
    /// Calculate the Euclidean distance between two vectors.
    /// i.e. sqrt((x11-x21) ^2 + ... + (x1n - x2n)^2)
    /// </summary>
    /// <param name="vec1">The first vector</param>
    /// <param name="vec2">The second vector</param>
    /// <returns>A double representing the distance.</returns>
    public static double CalculateEuclidianDistance(Vec2F vec1, Vec2F vec2) {
        var sum = Math.Pow(vec1.X - vec2.X, 2) + Math.Pow(vec1.Y - vec2.Y, 2);
        var distance = Math.Sqrt(sum);
        return distance;
    }
}
