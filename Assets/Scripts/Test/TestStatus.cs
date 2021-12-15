using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatus : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        Debug.Log("TestStart: health = " + Player.instance.getHealth());
        Debug.Log("TestStart: adding Sick condition");
        EffectManager.instance.startEffect("Sick", 2);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
        timer += Time.deltaTime;
        if ((timer >= 15f) && (timer <= 16f))
        {
            Debug.Log("TestStart: removing status Sick");
            EffectManager.instance.endEffect("Sick", 4);
            timer = 0f;
        }
        if ((timer >= 10f) && (timer <= 11f))
        {
            Debug.Log("TestStart: adding status Sick");
            EffectManager.instance.startEffect("Sick", 5);
            Debug.Log("TestStart: unPausing all effects");
            //EffectManager.instance.unPauseActiveEffect();
            timer++;
        }
        if ((timer >= 5f) && (timer <= 6f))
        {
            
            Debug.Log("TestStart: adding status Sick");
            EffectManager.instance.startEffect("Sick");
            Debug.Log("TestStart: Pausing all effects");
            //EffectManager.instance.pauseActiveEffect();
            timer++;
        }
        */
        
    }
    
}
