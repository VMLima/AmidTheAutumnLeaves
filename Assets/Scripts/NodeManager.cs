using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeManager : MonoBehaviour
{
    public TextMeshProUGUI textGUI;
    public GameObject buttonPanel;

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
        //populate feature array
        featureArray = Utils.GetSriptableFeatures<SO_Feature>();

        //populate node array
        nodeArray = Utils.GetSriptableNodes<SO_Node>();
        output = "Hello!";
        updateText();
    }

    void updateText()
    {
        textGUI.text = output;
    }

    SO_Node getNode(string name)
    {
        foreach(SO_Node node in nodeArray)
        {
            if(node.name == name)
            {
                return node;
            }
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //get forest node.
        currentNode = getNode("Forest");
        setNode(currentNode);
        //set up its features.
        //populate text from features
        //populate buttons from features
            //buttons need to have their methods set in script.

    }

    void setNode(SO_Node node)
    {
        //set up its features
        //preset features
        featureText = "";
        foreach (SO_Feature feature in currentNode.presetFeatures)
        {
            currentFeatures.Add(feature);
            featureText += feature.description + "\n";
            for(int i = 0; i < feature.buttons.Length; i++)
            {
                GameObject button = (GameObject)Instantiate(feature.buttons[0], transform);
                button.transform.SetParent(buttonPanel.transform);
            }
        }
        //random addition features
        int index = 0;
        for (int i = 0; i < currentNode.numRandomFeatures; i++)
        {
            //REWRITE THIS SO IT REMOVES ONES FROM LIST OF POTENTIALS INSTEAD OF BRUTE FORCE RNG.
            SO_Feature temp = currentFeatures[0];
            int j = 0;
            while((currentFeatures.Contains(temp)) && (j < 5))
            {
                index = Random.Range(0, featureArray.Length);
                temp = featureArray[index];
                j++;
            }
            currentFeatures.Add(featureArray[index]);
            featureText += featureArray[index].description + "\n";
            GameObject button = (GameObject)Instantiate(featureArray[index].buttons[0], transform);
            button.transform.SetParent(buttonPanel.transform);
        }
        output = featureText;
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
