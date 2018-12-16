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

        // Elevations of the corners of the landscape. Right and Bottom values
        // refer to the points next to the right- and bottom-most points of the
        // landscape.
        private int TopLeftElev;
        private int TopRightElev;
        private int BottomLeftElev;
        private int BottomRightElev;

        // Width and Height of this landscape fragment.
        public readonly int Width;
        public readonly int Height;

        // Sub-fragments of this part of the landscape. Their widths and hights
        // must sum-up to Width and Hight, either all or none are Null. Their 
        // relevant corner elevations must be consistent with this element
        // *Elev properties.
        private Landscape TopLeft;
        private Landscape TopRight;
        private Landscape BottomLeft;
        private Landscape BottomRight;

        // Constructs a sub-fragment of the landscape of a given width and
        // height, and having its corners at given elevations. If both sizes
        // of the fragment are at least equal to minSize, sub-fragments will
        // be recursively generated.
        private Landscape(int width, int height, int minSize,
                          int topLeftElev, int topRightElev,
                          int bottomLeftElev, int bottomRightElev) {
            Width = width;
            Height = height;
            TopLeftElev = topLeftElev;
            TopRightElev = topRightElev;
            BottomLeftElev = bottomLeftElev;
            BottomRightElev = bottomRightElev;
            if (width < 2 || height < 2 || width < minSize || height < minSize) {
                TopLeft = null;
                TopRight = null;
                BottomLeft = null;
                BottomRight = null;
            } else {
                int splitW = RndGen.Next(width / 4, (3 * width) / 4);
                int splitH = RndGen.Next(height / 4, (3 * height) / 4);
                int lowest = Math.Min(Math.Min(topLeftElev, topRightElev),
                                      Math.Min(bottomLeftElev, bottomRightElev));
                int highest = Math.Max(Math.Max(topLeftElev, topRightElev),
                                       Math.Max(bottomLeftElev, bottomRightElev));
                int diffRange = (highest - lowest) / 4;
                int splitElev = RndGen.Next(lowest + diffRange, highest - diffRange);
                int topSplitElev = topLeftElev +
                                   ((topRightElev - topLeftElev) * splitW) / width;
                int bottomSplitElev = bottomLeftElev +
                                      ((bottomRightElev - bottomLeftElev) * splitW) / width;
                int leftSplitElev = topLeftElev +
                                    ((bottomLeftElev - topLeftElev) * splitH) / height;
                int rightSplitElev = topRightElev +
                                     ((bottomRightElev - topRightElev) * splitH) / height;
                TopLeft = new Landscape(splitW, splitH, minSize,
                                        topLeftElev, topSplitElev,
                                        leftSplitElev, splitElev);
                TopRight = new Landscape(width - splitW, splitH, minSize,
                                         topSplitElev, topRightElev,
                                         splitElev, rightSplitElev);
                BottomLeft = new Landscape(splitW, height - splitH, minSize,
                                           leftSplitElev, splitElev,
                                           bottomLeftElev, bottomSplitElev);
                BottomRight = new Landscape(width - splitW, height - splitH, minSize,
                                            splitElev, rightSplitElev,
                                            bottomSplitElev, bottomRightElev);
            }
        }

        // Constructs the top level fragment of the landscape and initializes
        // recursive generating of the sub-fragments. If both sizes of the
        // fragment are at least equal to minSize, sub-fragments will be
        // recursively generated. The first split point to fragments will have
        // the elevation splitElev. All corners will have the elevation 0.
        public Landscape(int width, int height, int minSize, int splitElev) {
            Width = width;
            Height = height;
            TopLeftElev = 0;
            TopRightElev = 0;
            BottomLeftElev = 0;
            BottomRightElev = 0;
            if (width < 2 || height < 2 || width < minSize || height < minSize) {
                TopLeft = null;
                TopRight = null;
                BottomLeft = null;
                BottomRight = null;
            } else {
                int splitW = RndGen.Next(width / 4, (3 * width) / 4);
                int splitH = RndGen.Next(height / 4, (3 * height) / 4);
                TopLeft = new Landscape(splitW, splitH, minSize,
                                        0, 0, 0, splitElev);
                TopRight = new Landscape(width - splitW, splitH, minSize,
                                         0, 0, splitElev, 0);
                BottomLeft = new Landscape(splitW, height - splitH, minSize,
                                           0, splitElev, 0, 0);
                BottomRight = new Landscape(width - splitW, height - splitH, minSize,
                                            splitElev, 0, 0, 0);
            }
        }

        // Return the elevation of the given point. Outside of the fragment
        // width and height extrapolates the heights at the corners.
        public int GetPointElev(int x, int y) {
            if (x < 0) {
                if (y < 0) {
                    return TopLeftElev;
                } else if (y >= Height) {
                    return BottomLeftElev;
                }
                return TopLeftElev + ((BottomLeftElev - TopLeftElev) * y) / Height;
            }
            if (x >= Width) {
                if (y < 0) {
                    return TopRightElev;
                } else if (y >= Height) {
                    return BottomRightElev;
                }
                return TopRightElev + ((BottomRightElev - TopRightElev) * y) / Height;
            }
            // x is in the proper range
            if (y < 0) {
                return TopLeftElev + ((TopRightElev - TopLeftElev) * x) / Width;
            } else if (y >= Height) {
                return BottomLeftElev + ((BottomRightElev - BottomLeftElev) * x) / Width;
            }
            // x and y are in the proper range
            if (TopLeft == null || TopRight == null || BottomLeft == null || BottomRight == null) {
                int leftElev = TopLeftElev + ((BottomLeftElev - TopLeftElev) * y) / Height;
                int rightElev = TopRightElev + ((BottomRightElev - TopRightElev) * y) / Height;
                return leftElev + ((rightElev - leftElev) * x) / Width;
            }
            // this fragment of the landscape has subfragments
            if (x < TopLeft.Width) {
                if (y < TopLeft.Height) {
                    return TopLeft.GetPointElev(x, y);
                }
                return BottomLeft.GetPointElev(x, y - TopLeft.Height);
            }
            if (y < TopLeft.Height) {
                return TopRight.GetPointElev(x - TopLeft.Width, y);
            }
            return BottomRight.GetPointElev(x - TopLeft.Width, y - TopLeft.Height);
        }
    }

}
