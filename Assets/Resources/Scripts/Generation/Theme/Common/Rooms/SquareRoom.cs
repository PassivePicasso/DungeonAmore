using UnityEngine;
using System.Collections.Generic;

public class SquareRoom : Room
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
				if (parent == null) {
					Orient (tile, Orientation.North);
				} else {
					Orient (tile, parent.orientation);
				}
				Add (tile);
				if (doorIndex.Contains (x)) {
					if (y == 0) {
						tile = new Tile (tile.x, tile.y - 1, tile.type);
						tile.orientation = Room.Orientation.North;
						AddExit (tile);
					} else if (y == size - 1) {
						tile = new Tile (tile.x, tile.y + 1, tile.type);
						tile.orientation = Room.Orientation.South;
						AddExit (tile);
					} 
				} else if (doorIndex.Contains (y)) {
					if (x == 0) {
						tile = new Tile (tile.x - 1, tile.y - 1, tile.type);
						tile.orientation = Room.Orientation.East;
						AddExit (tile);
					} else if (x == size - 1) {
						tile = new Tile (tile.x + 1, tile.y, tile.type);
						tile.orientation = Room.Orientation.West;
						AddExit (tile);
					}
				}
			}
	}
	
	private void Orient (Tile tile, Orientation orientation)
	{
		switch (orientation) {
		case Orientation.South:
			tile.y *= -1;
			break;
		case Orientation.West:
			tile.x *= -1;
			break;
		}
	}
}

