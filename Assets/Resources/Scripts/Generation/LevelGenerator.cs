using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
	void Start ()
	{
		SquareRoom startRoom = gameObject.AddComponent<SquareRoom> ();
		startRoom.Initialize (12);
		var room = startRoom.AddRoom<SquareRoom> ();
		room.Initialize(5);
	}
}

