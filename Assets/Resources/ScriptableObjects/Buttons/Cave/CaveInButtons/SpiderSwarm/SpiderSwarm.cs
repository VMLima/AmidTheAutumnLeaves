using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderSwarm : ButtonEffectScript
{

    public RectTransform rectTransform;
    public RectTransform spidersRecTransform;
    private bool Moving;
    public float MoveSpeed;
   public Vector3 destination;
    private bool dying;
    private void Start()
    {
        rectTransform = GameObject.Find("ButtonPanel").GetComponent<RectTransform>();
        Moving = false;
        dying = false;
        spidersRecTransform = GetComponent<RectTransform>();
        int index = Random.Range(0, 4);
        if(index==0)
        {
            setButtonText("Spider", "EEWWW");
        }
        else if(index==1)
        {
            setButtonText("Spider", "squishy?");
        }
        else
        {
            setButtonText("Spider", "*skittering*");
        }
        
    }
    public override void onStart()
    {
        StartCoroutine(SquishSpider());

    }

    private void Update()
    {
        if (!dying)
        {
            MoveSpider();
        }


    }
    IEnumerator SquishSpider()
    {
        dying = true;
        bool untoggleEvent = true;
        if (TooltipManager.isEvent) untoggleEvent = false;
        TooltipManager.StartEvent_Static();
        TooltipManager.StartTooltip_Static("SQUISH", true);
        yield return new WaitForSeconds(0.5f);
        if (untoggleEvent) TooltipManager.StopEvent_Static();
        else TooltipManager.StopTooltip_Static(true);
        deleteSelf();
    }

    Vector2 NewRandomLocation()

    {

        return new Vector3(Random.Range(0, rectTransform.rect.width), Random.Range(0, rectTransform.rect.height)*-1,3);
    }

    public void MoveSpider()
    {
        
        if (Moving)
        {
            
            print(destination);
            if (Mathf.Approximately(spidersRecTransform.anchoredPosition.x, destination.x) && Mathf.Approximately(spidersRecTransform.anchoredPosition.y, destination.y))
            {
                Moving = false;
            } else {
               //spidersRecTransform.anchoredPosition = new Vector3(Mathf.MoveTowards(transform.localPosition.x, destination.x, MoveSpeed), Mathf.MoveTowards(transform.localPosition.y, destination.y, MoveSpeed), 0);
               spidersRecTransform.anchoredPosition = Vector3.MoveTowards(spidersRecTransform.anchoredPosition, destination, MoveSpeed);
                
            }
           
        }
        else {
            destination = NewRandomLocation();
            Moving = true;
        }

       
    }
}
