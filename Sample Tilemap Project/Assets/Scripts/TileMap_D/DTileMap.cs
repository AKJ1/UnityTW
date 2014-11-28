using UnityEngine;
using System.Collections.Generic;

public class DTileMap 
{
		
	protected class DRoom
	{
		public int left;
		public int top;
		public int width;
		public int height;
		
		public bool isConnected = false;
		
		public int right
		{
			get {return left + width - 1;}
		}
		
		public int bottom
		{
			get { return top + height - 1; }
		}
		
		public int center_x
		{
			get { return left + width / 2; }
		}
		
		public int center_y
		{
			get { return top + height / 2; }
		}
		
		public bool CollidesWith(DRoom other)
		{
			if (left > other.right - 1) 
			{
				return false;
			}
				
			
			if (top > other.bottom - 1)
			{
				return false;
			}
			
			if( right < other.left+1 )
			{
				return false;
			}
			
			if( bottom < other.top+1 )
			{
				return false;
			}

			else
			{
				return true;
			}

		}
		
	}
	
	int size_x;
	int size_y;
	
	int[,] map_data;
	
	List<DRoom> rooms;
	
	/*
	 * 0 = unknown
	 * 1 = floor
	 * 2 = wall
	 * 3 = stone
	 */
	
	public DTileMap(int size_x, int size_y)
	{
		DRoom currRoom;
		this.size_x = size_x;
		this.size_y = size_y;
		
		map_data = new int[size_x,size_y];
		
		for(int x = 0;x < size_x; x++)
		{
			for(int y=0; y < size_y; y++)
			{
				map_data[x,y] = 3;
			}
		}
		
		rooms = new List<DRoom>();
		
		int maxFails = 10;
		
		while(rooms.Count < 10)
		{
			int rsx = Random.Range(4,14);
			int rsy = Random.Range(4,10);
			
			currRoom = new DRoom();
			currRoom.left = Random.Range(0, size_x - rsx);
			currRoom.top = Random.Range(0, size_y-rsy);
			currRoom.width = rsx;
			currRoom.height = rsy;
			
			if(!RoomCollides(currRoom))
			{			
				rooms.Add (currRoom);
			}
			else
			{
				maxFails--;
				if (maxFails <= 0) 
				{
					break;
				}
			}
			
		}
		
		foreach(DRoom room in rooms)
		{
			MakeRoom(room);
		}
		

		for(int i = 0; i < rooms.Count; i++)
		{
			if(!rooms[i].isConnected)
			{
				int j = Random.Range(1, rooms.Count);
				MakeCorridor(rooms[i], rooms[(i + j) % rooms.Count ]);
			}
		}
		
		MakeWalls();
		
	}
	
	bool RoomCollides(DRoom currRoom)
	{
		foreach(DRoom room in rooms)
		{
			if(currRoom.CollidesWith(room))
			{
				return true;
			}
		}
		
		return false;
	}
	
	public int GetTileAt(int x, int y)
	{
		return map_data[x,y];
	}
	
	void MakeRoom(DRoom currRoom)
	{
		
		for(int x = 0; x < currRoom.width; x++)
		{
			for(int y = 0; y < currRoom.height; y++)
			{
				if(x == 0 || x == currRoom.width-1 || y == 0 || y == currRoom.height-1)
				{
					map_data[currRoom.left+x,currRoom.top+y] = 2;
				}
				else
				{
					map_data[currRoom.left+x,currRoom.top+y] = 1;
				}
			}
		}
		
	}
	
	void MakeCorridor(DRoom room1, DRoom room2)
	{
		int x = room1.center_x;
		int y = room1.center_y;
		
		while( x != room2.center_x)
		{
			map_data[x,y] = 1;
			
			x += x < room2.center_x ? 1 : -1;
		}
		
		while( y != room2.center_y )
		{
			map_data[x,y] = 1;
			
			y += y < room2.center_y ? 1 : -1;
		}
		
		room1.isConnected = true;
		room2.isConnected = true;
		
	}
	
	void MakeWalls()
	{
		for(int x=0; x< size_x;x++)
		{
			for(int y=0; y< size_y;y++)
			{
				if(map_data[x,y]==3 && HasAdjacentFloor(x,y))
				{
					map_data[x,y]=2;
				}
			}
		}
	}
	
	bool HasAdjacentFloor(int x, int y) {
		if( x > 0 && map_data[x-1,y] == 1 )
			return true;

		if( x < size_x-1 && map_data[x+1,y] == 1 )
			return true;

		if( y > 0 && map_data[x,y-1] == 1 )
			return true;

		if( y < size_y-1 && map_data[x,y+1] == 1 )
			return true;

		if( x > 0 && y > 0 && map_data[x-1,y-1] == 1 )
			return true;

		if( x < size_x-1 && y > 0 && map_data[x+1,y-1] == 1 )
			return true;

		if( x > 0 && y < size_y-1 && map_data[x-1,y+1] == 1 )
			return true;

		if( x < size_x-1 && y < size_y-1 && map_data[x+1,y+1] == 1 )
			return true;
		
		else 
		{
			return false;
		}

	}
}
