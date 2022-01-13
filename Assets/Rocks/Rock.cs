using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    bool Moving;
    Vector3 destination;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
       
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
            MoveTheRock();
        }
    }
    public void MoveRock(Vector3 endPosition)
    {
        Moving = true;
        destination = endPosition;
        
    }

    private void MoveTheRock()
    {
        rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, destination, 30);

        Destroy(gameObject, Random.Range(5,30));

    }

    
}
