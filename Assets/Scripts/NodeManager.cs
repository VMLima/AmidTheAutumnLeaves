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
    public SO_Feature[] featureArray;
    [HideInInspector]
    public SO_Node[] nodeArray;

    private SO_Node currentNode;
    private List<SO_Feature> currentFeatures = new List<SO_Feature>();

    private string output;
    private string featureText;

    private void Awake()
    {

        Instance = this;
        //populate feature array
        featureArray = Utils.GetAllFeatures<SO_Feature>();
        foreach (SO_Feature feature in featureArray)
        {
            feature.reset();
        }
        //populate node array
        nodeArray = Utils.GetAllNodes<SO_Node>();
        foreach (SO_Node node in nodeArray)
        {
            node.reset();
        }

    }

    void updateText(string text)
    {
        output += text;
        output += " \n ";
        textGUI.text = output;
    }

    SO_Node getNode(string name)
    {
        foreach(SO_Node node in nodeArray)
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
    public void unlockFeature(SO_Feature feature)
    {
        initFeature(feature);
    }

    void setupCurrentFeatures()
    {
        currentFeatures.Clear();
        currentFeatures.AddRange(currentNode.presetFeatures);
        int index = 0;
        for (int i = 0; i < currentNode.numRandomFeatures; i++)
        {
            //REWRITE THIS SO IT REMOVES ONES FROM LIST OF POTENTIALS INSTEAD OF BRUTE FORCE RNG.
            SO_Feature temp = currentFeatures[0];
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
            foreach (SO_Feature feature in currentFeatures)
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

    void initFeature(SO_Feature feature)
    {
        //setting up buttons and text.
        for (int i = 0; i < feature.buttons.Length; i++)
        {
            updateText(feature.description);
            GameObject button = (GameObject)Instantiate(feature.buttons[0], transform);
            button.transform.SetParent(buttonPanel.transform);
        }
    }

    void setNode(SO_Node node)
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
