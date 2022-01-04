using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public GameObject NodePrefab;
    private List<Node> ListOfNodes;
    
    void Start()
    {
        ListOfNodes = new List<Node>();
        NodeDataJson nodeDataJson = new NodeDataJson();

        Object[] jsonFileArry = Resources.LoadAll(("NodeJsonFiles"));
       // Debug.Log(jsonFileArry.Length);
      

        foreach(Object file in jsonFileArry)
        {
            string Json = file.ToString();
            nodeDataJson = JsonUtility.FromJson<NodeDataJson>(Json);
         //   Debug.Log(nodeDataJson.NodeName);
          //  Debug.Log(nodeDataJson.ConnectedNodes[0]);
        }






    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewNode(Node newNode)
    {
        if (!ListOfNodes.Contains(newNode))
        {
            ListOfNodes.Add(newNode);
        }
        
    }
    public void RemoveNode(Node removeNode)
    {
        if (ListOfNodes.Contains(removeNode))
        {
            ListOfNodes.Add(removeNode);
        }

    }

    void CreateNewNode()
    {
        Node tempNode= Instantiate(NodePrefab).GetComponent<Node>();
        
        
    }

    private class NodeDataJson
    {
        public string NodeName;
        public string[] ConnectedNodes;
    }

    
    

    
}
