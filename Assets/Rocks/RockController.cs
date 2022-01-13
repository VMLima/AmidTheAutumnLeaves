using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockController : MonoBehaviour
{
    public GameObject RockPrefab;
    RectTransform pageRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        pageRectTransform = GetComponentInParent<RectTransform>();
       // SpawnRockSlide();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRockSlide()
    {
        for(int i = 0; i < 100; i++)
        {
            GameObject NewRock = Instantiate(RockPrefab,transform);
            Vector3 starPosition = new Vector3(Random.Range(-pageRectTransform.rect.width / 2, pageRectTransform.rect.width / 2), pageRectTransform.rect.height / 2 + 20, 0);
            NewRock.GetComponent<RectTransform>().anchoredPosition = starPosition;

            Vector3 endPosition = new Vector3(starPosition.x, Random.Range(pageRectTransform.rect.height / 2, pageRectTransform.rect.height / -2), 0);
            //float endPosition =  Random.Range(pageRectTransform.rect.height / 4, pageRectTransform.rect.height / 2);
            NewRock.GetComponent<Rock>().MoveRock(endPosition);
        }
    }
}
