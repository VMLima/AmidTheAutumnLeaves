using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    Dictionary<Vector2,Room> DicOfRooms;
    public Vector2 maxMapSize;

    public GameObject RoomPrefab;
    private void Awake()
    {
        DicOfRooms = new Dictionary<Vector2, Room>();
        MakeMap();
        Debug.Log(DicOfRooms.Count);
        GiveAdjacentRooms();
    }


    void Start()
    {

    }


    void MakeMap()
    {
        for(int i = 1; i <= maxMapSize.x; i++)
        {
            for(int j = 1; j <= maxMapSize.y; j++)
            {
                GameObject temp =Instantiate(RoomPrefab);
                temp.GetComponent<Room>().setLocation(i, j);
                temp.GetComponent<Room>().SetRoomController(this);
                temp.name = string.Concat("(", i, ",", j, ")");
                if (i == 1 && j == 1)
                {
                    temp.GetComponent<Room>().AddSpecialRoom(new Vector2(5, 5));
                }
                DicOfRooms.Add(new Vector2(i, j), temp.GetComponent<Room>());
            }
        }
    }


    public Room ReturnRoom(Vector2 XYcords)
    {
        if (DicOfRooms.ContainsKey(XYcords))
        {
            return DicOfRooms[XYcords];
        }
        return null ;
       
    }
    public Vector2 MaxMapSize
    {
        get { return maxMapSize; }

    }



    void GiveAdjacentRooms()
    {
        foreach(KeyValuePair<Vector2,Room> Rooms in DicOfRooms)
        {
            Rooms.Value.FillAdjacentList();
        }
    }


}
