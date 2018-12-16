using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landscaper {

    // Represents a rectangular semi-randomly generated landscape.
    class Landscape {

        // Random numbers generator to be used by this class.
        static private Random RndGen = new Random();

        // Heights "above the sea level" of the corners of the landscape.
        // Right and Bottom values refer to the points next to the right-
        // and bottom-most points of the landscape.
        private int TopLeftHeight;
        private int TopRightHeight;
        private int BottomLeftHeight;
        private int BottomRightHeight;

        // Width and Height of this landscape fragment.
        public readonly int Width;
        public readonly int Height;

        // Sub-fragments of this part of the landscape. Their widths and hights
        // must sum-up to Width and Hight, either all or none are Null. Their
        // relevant corner hieights "above the sea level" must be consistent with
        // this element *Height properties.
        private Landscape TopLeft;
        private Landscape TopRight;
        private Landscape BottomLeft;
        private Landscape BottomRight;

        // Constructs a sub-fragment of the landscape of a given width and height, and
        // having its corners at given heights "above the sea level". If both sizes of
        // the fragment are at least equal to minSize, sub-fragments will be recursively
        // generated.
        private Landscape(int width, int height, int minSize,
                          int topLeftHeight, int topRightHeight,
                          int bottomLeftHeight, int bottomRightHeight) {
            Width = width;
            Height = height;
            TopLeftHeight = topLeftHeight;
            TopRightHeight = topRightHeight;
            BottomLeftHeight = bottomLeftHeight;
            BottomRightHeight = bottomRightHeight;
            if (width < 2 || height < 2 || width < minSize && height < minSize) {
                TopLeft = null;
                TopRight = null;
                BottomLeft = null;
                BottomRight = null;
            } else {
                int splitW = RndGen.Next(width / 4, (3 * width) / 4);
                int splitH = RndGen.Next(height / 4, (3 * height) / 4);
                int lowest = Math.Min(Math.Min(topLeftHeight, topRightHeight),
                                      Math.Min(bottomLeftHeight, bottomRightHeight));
                int highest = Math.Max(Math.Max(topLeftHeight, topRightHeight),
                                       Math.Max(bottomLeftHeight, bottomRightHeight));
                int diff = highest - lowest;
                int splitHeight = RndGen.Next(lowest + diff / 4, lowest + (3 * diff) / 4);
                int topSplitH = topLeftHeight + ((topRightHeight - topLeftHeight) * splitW) / width;
                int bottomSplitH = bottomLeftHeight + ((bottomRightHeight - bottomLeftHeight) * splitW) / width;
                int leftSplitH = topLeftHeight + ((bottomLeftHeight - topLeftHeight) * splitH) / height;
                int rightSplitH = topRightHeight + ((bottomRightHeight - topRightHeight) * splitH) / height;
                TopLeft = new Landscape(splitW, splitH, minSize,
                                        topLeftHeight, topSplitH, leftSplitH, splitHeight);
                TopRight = new Landscape(width - splitW, splitH, minSize,
                                         topSplitH, topRightHeight, splitHeight, rightSplitH);
                BottomLeft = new Landscape(splitW, height - splitH, minSize,
                                           leftSplitH, splitHeight, bottomLeftHeight, bottomSplitH);
                BottomRight = new Landscape(width - splitW, height - splitH, minSize,
                                            splitHeight, rightSplitH, bottomSplitH, bottomRightHeight);
            }
        }

        // Constructs the top level fragment of the landscape and initializes recursive
        // generating of the sub-fragments. If both sizes of the fragment are at least
        // equal to minSize, sub-fragments will be recursively generated. The first
        // split point into fragments will have the splitHeight "above the sea level".
        // All corners will be at "the sea level".
        public Landscape(int width, int height, int minSize, int splitHeight) {
            Width = width;
            Height = height;
            TopLeftHeight = 0;
            TopRightHeight = 0;
            BottomLeftHeight = 0;
            BottomRightHeight = 0;
            if (width < 2 || height < 2 || width < minSize && height < minSize) {
                TopLeft = null;
                TopRight = null;
                BottomLeft = null;
                BottomRight = null;
            } else {
                int splitW = RndGen.Next(width / 4, (3 * width) / 4);
                int splitH = RndGen.Next(height / 4, (3 * height) / 4);
                TopLeft = new Landscape(splitW, splitH, minSize, 0, 0, 0, splitHeight);
                TopRight = new Landscape(width - splitW, splitH, minSize, 0, 0, splitHeight, 0);
                BottomLeft = new Landscape(splitW, height - splitH, minSize, 0, splitHeight, 0, 0);
                BottomRight = new Landscape(width - splitW, height - splitH, minSize, splitHeight, 0, 0, 0);
            }
        }

        // Return the height "above the sea level" of the given point. Outside of
        // the fragment width and height extrapolates the heights at the corners.
        public int GetPointHeight(int x, int y) {
            if (x < 0) {
                if (y < 0) {
                    return TopLeftHeight;
                } else if (y >= Height) {
                    return BottomLeftHeight;
                }
                return TopLeftHeight + ((BottomLeftHeight - TopLeftHeight) * y) / Height;
            }
            if (x >= Width) {
                if (y < 0) {
                    return TopRightHeight;
                } else if (y >= Height) {
                    return BottomRightHeight;
                }
                return TopRightHeight + ((BottomRightHeight - TopRightHeight) * y) / Height;
            }
            // x is in the proper range
            if (y < 0) {
                return TopLeftHeight + ((TopRightHeight - TopLeftHeight) * x) / Width;
            } else if (y >= Height) {
                return BottomLeftHeight + ((BottomRightHeight - BottomLeftHeight) * x) / Width;
            }
            // x and y are in the proper range
            if (TopLeft == null || TopRight == null || BottomLeft == null || BottomRight == null) {
                int leftH = TopLeftHeight + ((BottomLeftHeight - TopLeftHeight) * y) / Height;
                int rightH = TopRightHeight + ((BottomRightHeight - TopRightHeight) * y) / Height;
                return leftH + ((rightH - leftH) * x) / Width;
            }
            // this fragment of the landscape has subfragments
            if (x < TopLeft.Width) {
                if (y < TopLeft.Height) {
                    return TopLeft.GetPointHeight(x, y);
                }
                return BottomLeft.GetPointHeight(x, y - TopLeft.Height);
            }
            if (y < TopLeft.Height) {
                return TopRight.GetPointHeight(x - TopLeft.Width, y);
            }
            return BottomRight.GetPointHeight(x - TopLeft.Width, y - TopLeft.Height);
        }
    }

}
