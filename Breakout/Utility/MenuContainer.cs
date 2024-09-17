namespace Breakout.Utility;

using System.Linq;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// A container of <see cref="MenuElement"/>'s allowing for circular enumeration.
/// </summary>
public class MenuContainer {
    private readonly List<MenuElement> items;

    /// <summary>
    /// Initialize a <see cref="MenuContainer"/> with a list of strings.
    /// </summary>
    /// <param name="labels">
    /// Strings to display on screen.
    /// Note: The order is such that the first element in the list is the highest element on the screen.
    /// </param>
    /// <param name="bottomLeftPosition">The bottom-left position of the entire list, i.e. the list "grows upwards"</param>
    /// <param name="elementExtent">The size of a single element.</param>
    public MenuContainer(IEnumerable<string> labels, Vec2F bottomLeftPosition, Vec2F elementExtent) {
        var elementHeight = bottomLeftPosition.Y;
        items = labels
            .Reverse() // Since the first label should be on top, we reverse the list.
            .Select((l, i) =>
                new MenuElement {
                    Label = l,
                    Text = new Text(
                    l,
                    bottomLeftPosition + new Vec2F(0.0f, elementHeight * i),
                    elementExtent)
                })
            .ToList();
    }

    /// <summary>
    /// Apply a method on each element in the list.
    /// </summary>
    /// <param name="action">The method to apply.</param>
    public void ForEach(Action<Text> action) {
        items.ForEach(it => action(it.Text));
    }
    public void Render() {
        items.ForEach(t => t.Text.RenderText());
    }

    /// <summary>
    /// Create an enumerator allowing for circular enumeration.
    /// </summary>
    /// <returns>A <see cref="CircularListEnumerator"/> initialized on the current collection.</returns>
    public CircularListEnumerator GetConcreteEnumerator() {
        return new CircularListEnumerator(items);
    }

    /// <summary>
    /// "Iterator"-like class for <see cref="MenuContainer"/> which iterates over the collection, looping back to the start or to the front if it goes out of bounds.
    /// </summary>
    public class CircularListEnumerator {
        private readonly IReadOnlyList<MenuElement> items;
        private int position;

        /// <summary>
        /// Initialize a <see cref="CircularListEnumerator"/> on a list of <see cref="MenuElement"/>'s.
        /// Note: The initial position will be se to the end of the list.
        /// </summary>
        /// <param name="list"></param>
        public CircularListEnumerator(List<MenuElement> list) {
            items = list.AsReadOnly();
            position = items.Count - 1;
        }
        public string Current {
            get {
                return items[position].Label;
            }
        }

        /// <summary>
        /// Select the element that is graphically, directly above the current one.
        /// In code, this is the next element in the list.
        /// </summary>
        public void MoveUp() {
            if (++position >= items.Count) {
                position = 0;
            }
        }
        /// <summary>
        /// Select the element that is graphically, directly below the current one.
        /// In code, this is the previous element in the list.
        /// </summary>
        public void MoveDown() {
            if (--position < 0) {
                position = items.Count - 1;
            }
        }

        /// <summary>
        /// Set the color on the current element
        /// </summary>
        /// <param name="color">A 3-vector describing the RGB color the text should have.</param>
        public void SetColor(Vec3I color) {
            items[position].Text.SetColor(color);
        }

        /// <summary>
        /// Reset the current element to the original position.
        /// </summary>
        public void Reset() {
            position = items.Count - 1;
        }
    }
}
