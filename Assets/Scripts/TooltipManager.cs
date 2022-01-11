using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;

    private static TooltipManager instance;
    private bool isActive = false;
    public static bool isEvent = false;
    private TextMeshProUGUI tooltipText;
    //private Text tooltipText;
    private RectTransform backgroundTransform;
    private void showTooltip(string tooltipString)
    {
        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + 2 * textPaddingSize > 200 ? 200: tooltipText.preferredWidth + 2*textPaddingSize, tooltipText.preferredHeight + 2*textPaddingSize);
        backgroundTransform.sizeDelta = backgroundSize;
        isActive = true;
        gameObject.SetActive(true);
        Update();
    }
    private void hideTooltip()
    {
        gameObject.SetActive(false);
        isActive = false;
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        backgroundTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        hideTooltip();
        //tooltipText = transform.Find("Text2").GetComponent<Text>();
        //showTooltip("YUAY3246234626426426426wrtyrteyerytertyertyert4363YASADD \nsdagasdg asfa asd sadfas asdagafasdfsdfasdfasdfadfad");
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            //transform.position = Input.mousePosition;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
            transform.localPosition = localPoint;
        }
    }

    public static void StartEvent_Static()
    {
        instance.hideTooltip();
        isEvent = true;
    }
    public static void StopEvent_Static()
    {
        isEvent = false;
        StopTooltip_Static();
    }

    //hover tooltips.  Do not function if in the middle of an event.
    public static void StartTooltip_Static(string tooltipString, bool eventInput = false)
    {
        if (isEvent && !eventInput) return;
        instance.showTooltip(tooltipString);
    }
    public static void StopTooltip_Static(bool eventInput = false)
    {
        if (isEvent && !eventInput) return;
        instance.hideTooltip();
    }
}
