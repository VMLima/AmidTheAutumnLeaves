using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MudPlayerController : MonoBehaviour
{


    public Vector2 StartingLocations;
    public Vector2 CurrentCords;

    public Room CurrentRoom;

    public Text Description;

    public float walkSpeed;
    public Slider WalkingProgressBar;
    public float fillSpeed = 0.5f;

    public float DistanceToNextRoom;

    public Button MoveUpButton;
    public Button MoveDownButton;
    public Button MoveLeftButton;
    public Button MoveRightButton;
    public GameObject SpecialConnectionsButton0;
    public GameObject SpecialConnectionsButton1;
    public GameObject SpecialConnectionsButton2;
    private List<GameObject> SpecialConnectionsList;

   public RoomController roomController;


    bool fillBar;

    private float DistanceTraveled;

    
    /*To do 
     * Arrive At Room
     * placeButtons
     * tranisiton between rooms
     * Special Stuff in rooms?
     * Animals change rooms?
     */


    // Start is called before the first frame update
    void Start()
    {
       
        
        StartingRoom(StartingLocations);
        SpecialConnectionsList = new List<GameObject>();
        SpecialConnectionsList.Add(SpecialConnectionsButton0);
        SpecialConnectionsList.Add(SpecialConnectionsButton1);
        SpecialConnectionsList.Add(SpecialConnectionsButton2);
        DisplayButtons();

        WalkingProgressBar.maxValue = DistanceToNextRoom;
    }

    // Update is called once per frame
    void Update()
    {
        if (fillBar)
        {
            FillSlider();
        }
    }

    public void DisplayCurrentRoom()
    {
        Description.text= CurrentRoom.Description;
    }
    IEnumerator TravelTime(Vector2 Destination,float TravelTimeF)
    {

        fillBar = true;
        yield return new WaitForSeconds(TravelTimeF);
        ChangeRooms(Destination);
    }

    private void FillSlider()
    {


        DistanceTraveled += walkSpeed * Time.deltaTime;

        WalkingProgressBar.value = DistanceTraveled;


    }
    void ChangeRooms(Vector2 Destination)
    {
        if (CurrentRoom.AdjacentRooms.ContainsKey(Destination))
        {
            CurrentRoom = CurrentRoom.AdjacentRooms[Destination];
            CurrentCords = CurrentRoom.Location;
            ArriveAtRoom();
        }
        else
        {
            Debug.Log("Cant Move In that direction");
        }

    }


    void DisplayButtons()
    {
        MoveUpButton.interactable = false;
        MoveDownButton.interactable = false;
        MoveLeftButton.interactable = false;
        MoveRightButton.interactable = false;
        
        SpecialConnectionsButton0.SetActive(false);
        SpecialConnectionsButton1.SetActive(false);
        SpecialConnectionsButton2.SetActive(false);



        foreach(KeyValuePair<Vector2,Room> adjacentRoom in CurrentRoom.AdjacentRooms)
        {
            
            switch (adjacentRoom.Key)
            {
                case  Vector2 left when left.Equals(CurrentCords+ Vector2.left):
        
                    MoveLeftButton.interactable = true;
                    break;

                case Vector2 right when right.Equals(CurrentCords + Vector2.right):
                    MoveRightButton.interactable = true;
                    break;
                case Vector2 up when up.Equals(CurrentCords + Vector2.up):
                    MoveUpButton.interactable = true;
                    break;
                case Vector2 down when down.Equals(CurrentCords + Vector2.down):
                    MoveDownButton.interactable = true;
                    break;
            }
        }
        if (CurrentRoom.SpecialConnections.Count > 0)
        {
            int Button = 0;
            foreach(Vector2 SpecialConnections in CurrentRoom.SpecialConnections)
            {
                SpecialConnectionsList[Button].SetActive(true);
                SpecialConnectionsList[Button].GetComponentInChildren<Text>().text = "Take Shortcut to " + SpecialConnections;
                Button++;
            }
        }
    }


    public void ArriveAtRoom()
    {
        DisplayCurrentRoom();
        DisplayButtons();
        WalkingProgressBar.value = 0;
        fillBar = false;
        DistanceTraveled = 0;
    }

    public void LeftButton()
    {
        //ChangeRooms(Vector2.left + CurrentRoom.Location);
        StartCoroutine(TravelTime(Vector2.left + CurrentRoom.Location, DistanceToNextRoom / walkSpeed));
    }
    public void RightButton()
    {
        ChangeRooms(Vector2.right + CurrentRoom.Location);
    }
    public void UpButton()
    {
        //ChangeRooms(Vector2.up + CurrentRoom.Location);
        StartCoroutine(TravelTime(Vector2.up + CurrentRoom.Location, DistanceToNextRoom / walkSpeed));
    }

    public void BackButton()
    {
        ChangeRooms(Vector2.down + CurrentRoom.Location);
    }
    public void StartingRoom(Vector2 StartRoom)
    {
        CurrentRoom = roomController.ReturnRoom(StartRoom);
        CurrentCords = CurrentRoom.Location;
        //DisplayButtons();
    }


    public void SpecialConnections0()
    {
        ChangeRooms(CurrentRoom.SpecialConnections[0]);
    }   
    public void SpecialConnections1()
    {
        ChangeRooms(CurrentRoom.SpecialConnections[1]);
    }
    public void SpecialConnections2()
    {
        ChangeRooms(CurrentRoom.SpecialConnections[1]);
    }
}
