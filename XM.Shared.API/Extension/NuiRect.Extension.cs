using Anvil.API;

namespace XM.Shared.API.Extension
{
    /// <summary>
    /// Extension methods for NuiRect to provide utility functionality.
    /// </summary>
    public static class NuiRectExtensions
    {
        /// <summary>
        /// Determines if the specified point (x, y) is within the bounds of this NuiRect.
        /// </summary>
        /// <param name="rect">The NuiRect to check against.</param>
        /// <param name="x">The X coordinate of the point to test.</param>
        /// <param name="y">The Y coordinate of the point to test.</param>
        /// <returns>True if the point is within the rectangle bounds, false otherwise.</returns>
        public static bool Contains(this NuiRect rect, float x, float y)
        {
            return x >= rect.X && 
                   x <= rect.X + rect.Width && 
                   y >= rect.Y && 
                   y <= rect.Y + rect.Height;
        }

        /// <summary>
        /// Determines if the specified point is within the bounds of this NuiRect.
        /// </summary>
        /// <param name="rect">The NuiRect to check against.</param>
        /// <param name="point">A tuple containing the (x, y) coordinates of the point to test.</param>
        /// <returns>True if the point is within the rectangle bounds, false otherwise.</returns>
        public static bool Contains(this NuiRect rect, (float x, float y) point)
        {
            return rect.Contains(point.x, point.y);
        }

        /// <summary>
        /// Gets the center point of this NuiRect.
        /// </summary>
        /// <param name="rect">The NuiRect to get the center of.</param>
        /// <returns>A tuple containing the (x, y) coordinates of the center point.</returns>
        public static (float x, float y) GetCenter(this NuiRect rect)
        {
            return (rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
        }

        /// <summary>
        /// Determines if this NuiRect intersects with another NuiRect.
        /// </summary>
        /// <param name="rect">The first NuiRect.</param>
        /// <param name="other">The second NuiRect to check for intersection.</param>
        /// <returns>True if the rectangles intersect, false otherwise.</returns>
        public static bool Intersects(this NuiRect rect, NuiRect other)
        {
            return rect.X < other.X + other.Width &&
                   rect.X + rect.Width > other.X &&
                   rect.Y < other.Y + other.Height &&
                   rect.Y + rect.Height > other.Y;
        }

        /// <summary>
        /// Gets the area of this NuiRect.
        /// </summary>
        /// <param name="rect">The NuiRect to calculate the area of.</param>
        /// <returns>The area of the rectangle.</returns>
        public static float GetArea(this NuiRect rect)
        {
            return rect.Width * rect.Height;
        }

        /// <summary>
        /// Determines if this NuiRect is empty (has zero or negative width or height).
        /// </summary>
        /// <param name="rect">The NuiRect to check.</param>
        /// <returns>True if the rectangle is empty, false otherwise.</returns>
        public static bool IsEmpty(this NuiRect rect)
        {
            return rect.Width <= 0 || rect.Height <= 0;
        }
    }
}
