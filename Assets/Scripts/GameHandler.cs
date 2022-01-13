using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    IncManager incM;
    EffectManager effectM;
    ButtonManager buttonM;
    Player player;
    public static GameHandler instance;

    public GameObject background;
    public GameObject creditsPanel;
    public GameObject creditsText;

    public Sprite underwater;
    public Sprite inCave;
    public Sprite inForest;

    IEnumerator CreditsIE()
    {
        TextMeshProUGUI textBox = creditsText.GetComponent<TextMeshProUGUI>();
        textBox.text = "You end up dying of dysentary";
        yield return new WaitForSeconds(8);
        textBox.text = "You end up dying of dysentary\n    (but worth?)";
        yield return new WaitForSeconds(2.5f);
        textBox.text = "Thank you for playing!";
        yield return new WaitForSeconds(6);
        textBox.text = "Created by Lucid Thread Games";
    }



    public void Credits()
    {

        creditsPanel.SetActive(true);
        StartCoroutine(CreditsIE());
        

        //You have died of dyssentary
        //(but worth?)
        //Thank you for playing!
        //Created by Lucid Thread Games

        //Art:Andi
        //Game Design:AsheCat
        //Peppyness and Dialogue:Sebastian
        //Fearless Leader:Spencer
        //Meeting Coordination:Maionaise.
        //Code stuff:Tim
    }

    public void modUIColor(string location)
    {
        if (location == "Forest")
        {
            //setDarkness(0);
            //StartCoroutine(changeAlpha());
            FlashlightManager.HideDarkness_Static(2);
            background.GetComponent<Image>().color = new Color(0.83f, 0.91f, 0.77f);
            StartCoroutine(McConnelManager.instance.startQuoteDelay("If I were to suggest a course of action, I think we should start a Fillibuster.",3));
        }
        else if (location == "Cave")
        {
            //setDarkness(60);
            //FlashlightManager.HidDarkness_Static();
            //FlashlightManager.HideFlashlight_Static();
            background.GetComponent<Image>().color = new Color(0.08f, 0.08f, 0.08f);
        }
        else if (location == "CaveIn")
        {
            //FlashlightManager.ShowDarkness_Static();
            //setDarkness(255);
        }
        else Debug.LogError("GameHandler:modUIColor: could not find match for string:" + location + ":");
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
