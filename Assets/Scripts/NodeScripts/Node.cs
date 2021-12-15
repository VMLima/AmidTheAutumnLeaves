using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private List<int> ConnectedNodes;

    public string nodeName;
    


    // Start is called before the first frame update
    void Start()
    {

        ConnectedNodes = new List<int>(); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConnectNode(int connectingNode)
    {
        if (!ConnectedNodes.Contains(connectingNode))
        {
            ConnectedNodes.Add(connectingNode);
        }
        
    }
    public void RemoveNode(int removingNode)
    {
        if (ConnectedNodes.Contains(removingNode))
        {
            ConnectedNodes.Remove(removingNode);
        }
        
    }


}
