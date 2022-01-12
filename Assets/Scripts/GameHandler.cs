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

    public GameObject mainCanvas;
    public GameObject darkness;
    GameObject darknessInstance;
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
