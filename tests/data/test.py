# import cStringIO
# import requests
# from quantized_mesh_tile.terrain import TerrainTile
# from quantized_mesh_tile.global_geodetic import GlobalGeodetic
# [z, x, y] = [14, 24297, 10735]
# geodetic = GlobalGeodetic(True)
# [west, south, east, north] = bounds = geodetic.TileBounds(x, y, z)
# url = 'http://assets.agi.com/stk-terrain/world/%s/%s/%s.terrain?v=1.16389.0' % (z, x, y)
# response = requests.get(url)
# content = cStringIO.StringIO(response.content)
# print west, south, east, north
# ter = TerrainTile(west=west, south=south, east=east, north=north)
# ter.fromStringIO(content)
# print ter.header
# print ter.getVerticesCoordinates()

# path = "9_533_383.terrain"
from quantized_mesh_tile.global_geodetic import GlobalGeodetic
from quantized_mesh_tile.terrain import TerrainTile
geodetic = GlobalGeodetic(True)
# [z, x, y] = [16,67465,51617]
[z, x, y] = [0,0,0]
[minx, miny, maxx, maxy] = geodetic.TileBounds(x, y, z)
ter = TerrainTile(west=minx, south=miny, east=maxx, north=maxy)
print geodetic.TileBounds(0,0,0)
# ter.fromFile('ahn_416656.terrain')
ter.fromFile('51617.terrain')

print ter.getTrianglesCoordinates()