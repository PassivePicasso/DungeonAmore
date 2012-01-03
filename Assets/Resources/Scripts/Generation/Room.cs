using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
	public enum Orientation
	{
		North,
		East,
		South,
		West
	};
	public int renderCount = 10;
	public Tile parent;
	private List<Tile> cells = new List<Tile> ();
	private List<Tile> exits = new List<Tile> ();
	private Dictionary<Tile, GameObject> tileMap = new Dictionary<Tile, GameObject> ();
	private readonly static Queue<Tile> renderQueue = new Queue<Tile> ();
	private static int existingRooms = 0;
	public static readonly int MAXIMUM_ROOMS = 100;

	protected void Clear ()
	{
		cells.Clear ();
	}
	
	protected void Add (Tile tile)
	{
		var tiles = cells.Where (t => {
			return (t.x == tile.x && t.y == tile.y);
		});
		
		if (tiles.Count () > 0) {
			cells.Remove (tiles.Single ());
			cells.Add (tile);
		} else {
			cells.Add (tile);
			renderQueue.Enqueue (tile);
		}
	}
	
	protected void AddExit (Tile tile)
	{
		if (!cells.Contains (tile)) {
			Add (tile);
		}
		exits.Add (cells [cells.IndexOf (tile)]);
	}
	
	public X AddRoom<X> () where X : Room
	{
		if (existingRooms < Room.MAXIMUM_ROOMS) {
			var exit = exits [UnityEngine.Random.Range (0, exits.Count)];
			exits.Remove (exit);
			if (tileMap.ContainsKey (exit)) {
				var room = tileMap [exit].AddComponent<X> ();
				return room;
			}
		}
		return null;
	}
	
	public List<Tile> Exits {
		get { return exits; }
	}

	public bool IsRendered ()
	{
		return renderQueue.Count == 0;
	}
	
	void Update ()
	{
		int passes = 0;
		if (renderQueue.Count == 0)
			return;
		while (renderQueue.Count > 0 && passes++ < renderCount) {
			Tile renderTarget = renderQueue.Dequeue ();
			if (renderTarget.type == Tile.TileType.Floor) {
				var tile = GameObject.CreatePrimitive (PrimitiveType.Cube);
				tile.transform.parent = this.transform;
				tile.transform.position = new Vector3 (renderTarget.x, 0, renderTarget.y);
				tileMap.Add (renderTarget, tile);
			}
		}
	}
}