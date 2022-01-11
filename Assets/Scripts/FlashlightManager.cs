using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;

    private static FlashlightManager instance;
    private bool isActive = false;

    //private TextMeshProUGUI tooltipText;
    //private Text tooltipText;
    private RectTransform backgroundTransform;
    private void show()
    {
        isActive = true;
        gameObject.SetActive(true);
        Update();
    }
    private void hide()
    {
        gameObject.SetActive(false);
        isActive = false;
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        //backgroundTransform = transform.Find("Background").GetComponent<RectTransform>();
        //tooltipText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        show();
        //hide();
        //tooltipText = transform.Find("Text2").GetComponent<Text>();
        //showTooltip("YUAY3246234626426426426wrtyrteyerytertyertyert4363YASADD \nsdagasdg asfa asd sadfas asdagafasdfsdfasdfasdfadfad");
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //transform.position = Input.mousePosition;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
            transform.localPosition = localPoint;
        }
    }
    public static void ShowFlashlight_Static()
    {
        instance.show();
    }
    public static void HideFlashlight_Static()
    {
        instance.hide();
    }
}
