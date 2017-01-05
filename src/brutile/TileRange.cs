// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

namespace Terrain.BruTile
{
    public struct TileRange
    {
        public int FirstCol { get; }
        public int FirstRow { get; }
        public int ColCount { get; }
        public int RowCount { get; }

        public TileRange(int col, int row) : this(col, row, 1, 1) { }

        public TileRange(int firstCol, int firstRow, int colCount, int rowCount) : this()
        {
            FirstCol = firstCol;
            FirstRow = firstRow;
            ColCount = colCount;
            RowCount = rowCount;
        }
    }
}
