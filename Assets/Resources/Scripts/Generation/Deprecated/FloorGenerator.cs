//using UnityEngine;
//using System.Collections.Generic;
//using System.Linq;
//
//public class FloorGenerator : MonoBehaviour
//{
//	public Room floorMap;
//	
//	public List<Tile> edgeTiles;
//	private int renderCount = 10, featureCount = 0, gen_iterations = 0;
//	private readonly int MINIMUM_FEATURE_COUNT = 200;
//	private readonly int MAXIMUM_GENERATION_ITERATIONS = 10000;
//
//	void Start ()
//	{
//		edgeTiles = new List<Tile> ();
//		renderQueue = new Queue<Tile> ();
//		floorMap = new Room (400, 400);
//		
//		int x = Random.Range (120, 240);
//		int y = Random.Range (120, 240);
//		
//		AddRoom (floorMap [x, y]);
//		
//		if (edgeTiles.Count > 0)
//			while (featureCount < MINIMUM_FEATURE_COUNT) {
//				var edge = edgeTiles [Random.Range (0, edgeTiles.Count)];
//				int featureType = Random.Range (0, 4);
//				switch (featureType) {
//				case 0:
//					AddHall (edge);
//					break;
//				case 1:
//				case 2:
//				case 3:
//					AddRoom (edge);
//					break;
//				}
//				gen_iterations++;
//				if (gen_iterations >= MAXIMUM_GENERATION_ITERATIONS)
//					break;
//			}
//	}
//	
//	void AddHall (Tile start)
//	{
//		int hallLength = 10;
//		switch (Random.Range (0, 4)) {
//		case 0:
//			if (FillRect (start.x, start.y , 1, hallLength, Room.TileType.Floor)) {
//				featureCount++;
//			}
//			break;
//		case 1:
//			if (FillRect (start.x, start.y - hallLength, 1, hallLength, Room.TileType.Floor)) {
//				featureCount++;
//			}
//			break;
//		case 2:
//			if (FillRect (start.x, start.y, hallLength, 1, Room.TileType.Floor)) {
//				featureCount++;
//			}
//			break;
//		case 3:
//			if (FillRect (start.x - hallLength, start.y, hallLength, 1, Room.TileType.Floor)) {
//				featureCount++;
//			}
//			break;
//		}
//	}
//	
//	void AddRoom (Tile start)
//	{
//		int length = Random.Range (2, 21);
//		int width = Random.Range (2, 21);
//		if (start.x + width > floorMap.Width || start.y + length > floorMap.Height)
//			return;
//		if (FillRect (start.x, start.y, width, length, Room.TileType.Floor)) {
//			featureCount++;
//		}
//	}
//	
//	void Update ()
//	{
//		int passes = 0;
//		if (renderQueue.Count == 0)
//			return;
//		while (renderQueue.Count > 0 && passes++ < renderCount) {
//			Tile renderTarget = renderQueue.Dequeue ();
//			if (renderTarget.type == Room.TileType.Floor) {
//				var tile = GameObject.CreatePrimitive (PrimitiveType.Cube);
//				tile.transform.position = new Vector3 (renderTarget.x, -1, renderTarget.y);
//				tile.transform.parent = transform;
//			}
//		}
//	}
//	
//	private bool FillRect (int x, int y, int length, int width, Map.TileType type)
//	{
//		List<Tile> filledLocations = new List<Tile> ();
//		for (int dx = 0; dx < width; dx++)
//			for (int dy = 0; dy < length; dy++) {
//				if (floorMap [x + dx, y + dy].type == Room.TileType.Solid) {
//					floorMap [x + dx, y + dy] = new Tile (x + dx, y + dy, type);
//					filledLocations.Add (floorMap [x + dx, y + dy]);
//					if ((dx + 1 == width || dx == 0 || dy + 1 == length || dy == 0)) {
//						edgeTiles.Add (floorMap [x + dx, y + dy]);
//					}
//				} else {
//					foreach (Tile t in filledLocations) {
//						floorMap [t.x, t.y] = new Tile (t.x, t.y, Room.TileType.Solid);
//					}
//					return false;
//				}
//			}
//		filledLocations.ForEach (t => renderQueue.Enqueue (t));
//		return true;
//	}
//}
