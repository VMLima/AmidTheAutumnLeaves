using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class MenuItem {
    public string name;
    public GameObject menu;
    public bool isOpen;
}

[System.Serializable]
public class MenuSpot {
    public Transform spot;
    public int menuIndex;

}

public class UIController : MonoBehaviour {

    public List<MenuSpot> spots = new List<MenuSpot>();
    public GameObject MenuPrefab;

    public List<MenuItem> menus = new List<MenuItem>();

    private int currentMenu = 0;

    public void MenuButtonPressed(string key) {
        GameObject newMenu = null;
        for (int i = 0; i < menus.Count; i++) {
            if (menus[i].name == key) {
                newMenu = menus[i].menu;
                ActivateMenu(i);
            }
        }
        if (newMenu == null) {
            Debug.LogError("Invalid Menu Key: " + key);
        } 
        //GameObject newMenu = menus.Find();
    }

    private void ActivateMenu(int index) {
        //MenuSpot thisMenu = spots[currentMenu];
        // Check if the window is already open
        if (menus[index].isOpen) {
            Debug.Log(menus[index].name + " is already open");
            return;
        }

        // if this is the first menu open/there is no menu open now
        if (spots[currentMenu].menuIndex == 99) {
            Instantiate(menus[index].menu, spots[currentMenu].spot);
        } else {
            Destroy(spots[currentMenu].spot.GetChild(0).gameObject);
            menus[spots[currentMenu].menuIndex].isOpen = false;
            Instantiate(menus[index].menu, spots[currentMenu].spot);
        }
        
        menus[index].isOpen = true;
        spots[currentMenu].menuIndex = index;

        // Set the next open menu;
        currentMenu = currentMenu == 0 ? 1 : 0;
    }

}
