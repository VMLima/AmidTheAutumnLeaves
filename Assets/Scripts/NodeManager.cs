using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeManager : MonoBehaviour
{
    public TextMeshProUGUI textGUI;
    public GameObject buttonPanel;
    public static NodeManager Instance;

    [HideInInspector]
    public RoomFeatureSO[] featureArray;
    [HideInInspector]
    public RoomSO[] nodeArray;

    private RoomSO currentNode;
    private List<RoomFeatureSO> currentFeatures;

    private string output;
    private string featureText;

    private List<GameObject> activeButtons;

    private void Awake()
    {

        Instance = this;
        //populate feature array
        activeButtons = new List<GameObject>();
        featureArray = Utils.GetAllScriptableObjects<RoomFeatureSO>();
        foreach (RoomFeatureSO feature in featureArray)
        {
            feature.reset();
            Debug.Log(feature.name);
        }
        //populate node array
        nodeArray = Utils.GetAllScriptableObjects<RoomSO>();
        foreach (RoomSO node in nodeArray)
        {
            node.reset();
            Debug.Log(node.name);
        }
        Debug.Log("done nodes");
        currentFeatures = new List<RoomFeatureSO>();
    }

    private void OnDestroy()
    {
        //clean up instantiated buttons.
        for(int i = (activeButtons.Count - 1); i>= 0;i--)
        {
            Destroy(activeButtons[i].gameObject);
        }
        activeButtons.Clear();
    }

    void updateText(string text)
    {
        output += text;
        output += " \n ";
        textGUI.text = output;
    }

    RoomSO getNode(string name)
    {
        foreach(RoomSO node in nodeArray)
        {
            if(node.nameTag == name)
            {
                return node;
            }
        }
        return null;
    }

    void TEST()
    {
        output = "";
        updateText("Hello!");
        currentNode = getNode("Forest");
        setNode(currentNode);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        TEST();
    }

    //when an unlock is called from a listener, can easily do...
    //"NodeManager.instance.unlockFeature(thisFeature)"
    public void unlockFeature(RoomFeatureSO feature)
    {
        initFeature(feature);
    }

    void setupCurrentFeatures()
    {
        currentFeatures.Clear();
        if(currentNode != null && currentNode.presetFeatures != null)
        {
            currentFeatures.AddRange(currentNode.presetFeatures);
        }
        else
        {
            Debug.LogError("UNABLE TO GET CURRENT NODE:");
            return;
        }
        int index = 0;
        for (int i = 0; i < currentNode.numRandomFeatures; i++)
        {
            //REWRITE THIS SO IT REMOVES ONES FROM LIST OF POTENTIALS INSTEAD OF BRUTE FORCE RNG.
            RoomFeatureSO temp = currentFeatures[0];
            int j = 0;
            while ((currentFeatures.Contains(temp)) && (j < 5))
            {
                index = Random.Range(0, featureArray.Length);
                temp = featureArray[index];
                j++;
            }
            currentFeatures.Add(featureArray[index]);
        }

    }

    void setupScene()
    {
        //settting up buttons and text.
        if(currentFeatures.Count > 0)
        {
            foreach (RoomFeatureSO feature in currentFeatures)
            {
                if (!feature.unlocked)
                {
                    //if it is locked, check if it shouldn't be.
                    feature.unlocked = Utils.checkUnlocked(feature.toUnlock);
                }
                if (feature.unlocked)
                {
                    initFeature(feature);
                }
            }
        }
        //LAZY.  DO NOT HAVE REAL LASTING GOOD TEXT SETUP YET.
        
    }

    void initFeature(RoomFeatureSO feature)
    {
        //setting up buttons and text.
        for (int i = 0; i < feature.buttons.Length; i++)
        {
            updateText(feature.description);
            GameObject button = (GameObject)Instantiate(feature.buttons[0], transform);
            button.transform.SetParent(buttonPanel.transform);
            button.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            activeButtons.Add(button);
        }
    }

    void setNode(RoomSO node)
    {
        //creating a list of features for this scene
        setupCurrentFeatures();
        //setting up objects for this scene.
        setupScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
