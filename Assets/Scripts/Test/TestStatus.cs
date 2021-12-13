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
        Debug.Log("TestStart: setting health 100");
        StatusManager.instance.health = 100;
        Debug.Log("TestStart: adding Sick condition");
        StatusManager.instance.addStatus("Sick");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if ((timer >= 15f) && (timer <= 16f))
        {
            Debug.Log("TestStart: removing status Sick");
            StatusManager.instance.removeStatus("Sick");
            timer = 0f;
        }
        if ((timer >= 10f) && (timer <= 11f))
        {
            Debug.Log("TestStart: adding status Sick");
            StatusManager.instance.addStatus("Sick");
            timer++;
        }
        if ((timer >= 5f) && (timer <= 6f))
        {
            Debug.Log("TestStart: adding status Sick");
            StatusManager.instance.addStatus("Sick");
            timer++;
        }
        
    }
}
