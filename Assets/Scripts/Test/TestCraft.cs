using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCraft : MonoBehaviour
{
    bool doOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doOnce)
        {
            doOnce = false;

            ResourceSO berry = IncManager.instance.Get<ResourceSO>("Berry");
            CraftSO BreedBerries = Utils.GetScriptableObjects<CraftSO>("BreedBerries");
            //IncManager.instance.Unlock(berry);

            Debug.Log("Test: num berries = " + berry.getAmount());
            Debug.Log("Test: CRAFTING 1 BreedBerry");
            ButtonManager.instance.craft(BreedBerries);
            Debug.Log("Test: num berries = " + berry.getAmount());

            Debug.Log("Test: ADDING 2 berry");
            IncManager.instance.AddAmount(berry, 3);
            Debug.Log("Test: num berries = " + berry.getAmount());

            Debug.Log("Test: CRAFTING 1 BreedBerry");
            ButtonManager.instance.craft(BreedBerries);
            Debug.Log("Test: num berries = " + berry.getAmount());

            Debug.Log("Test: CRAFTING 2 BreedBerry");
            ButtonManager.instance.craft(BreedBerries, 2);
            Debug.Log("Test: num berries = " + berry.getAmount());
        }
    }
}
