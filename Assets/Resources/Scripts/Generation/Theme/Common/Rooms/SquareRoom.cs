using UnityEngine;
using System.Collections.Generic;

public class SquareRoom : Room<SquareRoom>
{
	public float size = 3;

	public void Initialize (int size)
	{
		this.size = size;
		List<int> doorIndex = new List<int> ();
		if (size % 2 == 1) {
			doorIndex.Add ((int)Mathf.Floor (size / 2));
		} else {
			doorIndex.Add ((int)size / 2);
			doorIndex.Add ((int)(size / 2) - 1);
		}
		for (int x = 0; x < size; x++)
			for (int y = 0; y < size; y++) {
				var tile = new Tile (x, y, Tile.TileType.Floor);
				Add (tile);
				if (doorIndex.Contains (x)) {
					if (y == 0) {
						AddExit (new Tile (tile.x, tile.y - 1, tile.type));
					} else if (y == size - 1) {
						AddExit (new Tile (tile.x, tile.y + 1, tile.type));
					} 
				} else if (doorIndex.Contains (y)) {
					if (x == 0) {
						AddExit (new Tile (tile.x - 1, tile.y, tile.type));
					} else if (x == size - 1) {
						AddExit (new Tile (tile.x + 1, tile.y, tile.type));
					}
				}
			}
	}
}

