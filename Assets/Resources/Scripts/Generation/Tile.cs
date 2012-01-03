using System;

public class Tile
{
	public enum TileType
	{
		Solid,
		Floor,
		StairUp,
		StairDown
	}
	public int x, y;
	public TileType type;
		
	public Tile (int x, int y, TileType type)
	{
		this.x = x;
		this.y = y;
		this.type = type;
	}
		
	public static bool operator== (Tile left, Tile right)
	{
		if (left == null)
			return false;
		if (right == null)
			return false;
		if (left.x != right.x)
			return false;
		if (left.y != right.y)
			return false;
		return true;
	}
	
	public static bool operator!= (Tile left, Tile right)
	{
		return !(left == right);
	}
	
	public override bool Equals (object obj)
	{
		return base.Equals (obj);
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}
}

