using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkills : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;

        Debug.Log("===Adding skill Foraging======");
        IncManager.instance.Unlock<SkillSO>("Foraging");
        IncManager.instance.allSkillsDebugLog();
        /*
        Debug.Log("======Adding 10xp to Foraging");
        SkillManager.instance.addXP("Foraging", 10);
        SkillManager.instance.allSkillsDebugLog();

        Debug.Log("======Adding 10xp to Foraging");
        SkillManager.instance.addXP("Foraging", 10);
        SkillManager.instance.allSkillsDebugLog();

        Debug.Log("======Adding passive xp gainer of 5/s to foraging");
        SkillManager.instance.passiveXP("berries", "Foraging", 5);
        SkillManager.instance.allSkillsDebugLog();
        */
    }

    // Update is called once per frame
    void Update()
    {
        //every second update.
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            //Debug.Log("======Adding 10xp to Foraging");
            //SkillManager.instance.addXP("Foraging", 1);
            //SkillManager.instance.allSkillsDebugLog();
            timer -= 1f;
        }
    }
}