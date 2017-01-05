// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Terrain.BruTile
{
    /// <summary>
    /// Interface definition of a tile schema
    /// </summary>
    // Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

    public interface ITileSchema
    {
        /// <summary>
        /// Gets a value indicating the spatial reference system (srs) of the tile schema
        /// </summary>
        string Srs { get; }

        /// <summary>
        /// Gets a value indicating the extent covered by this tile schema
        /// </summary>
        Extent Extent { get; }

        /// <summary>
        /// Function to get a tile's width for a given zoom level.
        /// </summary>
        /// <param name="levelId">The zoom level's id</param>
        /// <returns>The width of the tile</returns>
        int GetTileWidth(string levelId);

        /// <summary>
        /// Function to get the x vertex of the schema's origin for a given zoom level.
        /// </summary>
        /// <param name="levelId">The zoom level's id</param>
        /// <returns>The x vertex of the origin</returns>
        double GetOriginX(string levelId);

        /// <summary>
        /// Function to get the y vertex of the schema's origin for a given zoom level.
        /// </summary>
        /// <param name="levelId">The zoom level's id</param>
        /// <returns>The y vertex of the origin</returns>
        double GetOriginY(string levelId);

        /// <summary>
        /// Gets a value indicating the resolutions defined in this schema
        /// </summary>
        IDictionary<string, Resolution> Resolutions { get; }

        /// <summary>
        /// Gets a value indicating the orientation of the y-axis
        /// </summary>
        YAxis YAxis { get; }
    }
}