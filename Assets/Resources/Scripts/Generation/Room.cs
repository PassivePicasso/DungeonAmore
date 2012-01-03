using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room<T> : MonoBehaviour where T : Room<T>
{
	public int renderCount = 10;
	private List<Tile> cells = new List<Tile> ();
	private List<Tile> exits = new List<Tile> ();
	private Dictionary<Tile, GameObject> tileMap = new Dictionary<Tile, GameObject> ();
	private Tile parent;
	private readonly static Queue<Tile> renderQueue = new Queue<Tile> ();

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
		exits.Add (tile);
	}
	
	public X AddRoom<X> () where X : Room<X>
	{
		Debug.Log (exits.Count);
		var exit = exits [UnityEngine.Random.Range (0, exits.Count)];
		if (tileMap.ContainsKey (exit)) {
			Debug.Log("Found Object");
			var room = tileMap [exit].AddComponent<X> ();
			return room;
		}
		return null;
	}
	
	public List<Tile> Exits {
		get { return exits; }
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
				tile.transform.localPosition = new Vector3 (renderTarget.x, -1, renderTarget.y);
				tileMap.Add (renderTarget, tile);
			}
		}
	}
}