using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public Dictionary<Vector2, Room> AdjacentRooms;

     Vector2 location;

     RoomController roomController;

    string description;

    public List<Vector2> SpecialConnections;

    

    void Awake()
    {

        AdjacentRooms = new Dictionary<Vector2, Room>();

        roomController = FindObjectOfType<RoomController>();
        SpecialConnections = new List<Vector2>();
    }

    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {

    }
    public void SetRoomController(RoomController InroomController)
    {
        roomController = InroomController;
    }

    public Vector2 Location
    {
        get { return location; }
        set
        {
            location.x = value.x;
            location.y = value.y;
        }
    }
    
    public void setLocation(int Incomingx, int Incomingy)
    {
        Location = (new Vector2(Incomingx, Incomingy));
    }

    public void FillAdjacentList()
    {

        List<Vector2> HelperCords = new List<Vector2>();
        HelperCords.Add(Vector2.up);
        HelperCords.Add(Vector2.down);
        HelperCords.Add(Vector2.left);
        HelperCords.Add(Vector2.right);


        foreach(Vector2 helpers in HelperCords)
        {
            if(roomController.ReturnRoom(helpers + Location) != null)
            {
                AdjacentRooms.Add(helpers + Location, roomController.ReturnRoom(helpers + Location));
            }
            
        }


        

         
        
        Debug.Log("Filled "+ AdjacentRooms.Count + " " + name);
        
    }
    public void AddSpecialRoom(Vector2 SpecialRoomCoods)
    {
        SpecialConnections.Add(SpecialRoomCoods);
    }
    public string Description
    {
        get { return description; }
        set
        {
            description = value;
        }
    }
}
