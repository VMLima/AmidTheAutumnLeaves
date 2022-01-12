using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    IncManager incM;
    EffectManager effectM;
    ButtonManager buttonM;
    Player player;
    public static GameHandler instance;

    public GameObject background;
    public GameObject darkness;
    GameObject darknessInstance;

    public void modUIColor(string location)
    {
        if (location == "Forest")
        {
            setDarkness(0);
            background.GetComponent<Image>().color = new Color(0.1f, 0.21f, 0f);
        }
        else if (location == "Cave")
        {
            //setDarkness(60);
            background.GetComponent<Image>().color = new Color(0.17f, 0.17f, 0.17f);
        }
        else if (location == "CaveIn")
        {
            setDarkness(255);
        }
        else Debug.LogError("GameHandler:modUIColor: could not find match for string:" + location + ":");
    }
    public void setDarkness(int opacity)
    {
        Color color = darkness.GetComponent<Image>().color;
        color.a = ((float) opacity)/255f;
        darkness.GetComponent<Image>().color = color;
    }    
    public void startDarkness(bool turnOn)
    {
        if (turnOn) darkness.GetComponent<Image>().color = Color.black;
        else darkness.GetComponent<Image>().color = Color.clear;
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        incM = IncManager.instance;
        effectM = EffectManager.instance;
        buttonM = ButtonManager.instance;
        player = Player.instance;

        //COMPILING MPORTANT DATA
        incM.compileDataStorage();  //gets all the known scriptable objects.
        effectM.loadLooseEffects(); //loads loose effects held on a central object.  UNUSED ATM.

        //SETTING UP START DATA
        incM.resetAllScriptableObjects(); //resets all the values of all scriptable objects
        player.defaultValues();
        setInitialInventory();

        //begining game.
        startGame();
    }

    void setInitialInventory()
    {
        IncrementalValuePair[] startingInventory = incM.Get<IncArraySO>("StartingInventory").IncrementableArray;
        if(startingInventory != null && startingInventory.Length>0)
        {
            foreach(IncrementalValuePair pair in startingInventory)
            {
                if(pair.incrementable != null) incM.Add(pair.incrementable, pair.amount);
            }
        }
    }

    void startGame()
    {
        modUIColor("Cave");
        buttonM.addButtonArrayToUI("StartingButtons");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        //Any instantiated objects need to be destroyed or else weird lingering data stuff can happen.
        effectM.destroyLooseEffects();
    }
}
