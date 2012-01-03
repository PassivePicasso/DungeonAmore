using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
	private readonly static Queue<Room> additionQueue = new Queue<Room> ();
	
	void Start ()
	{
		SquareRoom startRoom = gameObject.AddComponent<SquareRoom> ();
		startRoom.Initialize (12);
		additionQueue.Enqueue (startRoom);
		int repeat = Random.Range (0, 3);
		while (repeat-- > 0) {
			additionQueue.Enqueue (startRoom);
		}
	}
	
	void Update ()
	{
		if (additionQueue.Count > 0) {
			var room = additionQueue.Dequeue ();
			if (room != null && room.IsRendered ()) {
				var room2 = room.AddRoom<SquareRoom> ();
				if (room2 != null) {
					room2.Initialize (Random.Range (4, 12));
					int repeat = Random.Range (0, room2.Exits.Count);
					while (repeat-- > 0) 
						additionQueue.Enqueue (room2);
				}
			} else if (room != null) {
				additionQueue.Enqueue (room);
			}
		}
	}
}

