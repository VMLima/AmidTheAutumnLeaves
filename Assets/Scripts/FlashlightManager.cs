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

    public GameObject Darkness;
    public GameObject Light;

    private SpriteRenderer darknessSprite;

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

    IEnumerator darknessAlphaSlide(float startingAlpha, float endingAlpha, float time)
    {
        Color tempColor = darknessSprite.color;
        float timeInverval = 0.1f;
        float timeIntervalCount = time / timeInverval;
        float step = (startingAlpha - endingAlpha) / timeIntervalCount;
        //Debug.Log("darknessAlphaSlide:timeIntervalCount=" + timeIntervalCount + ": step=" + step);
        for(int i = 0;i<timeIntervalCount;i++)
        {
            tempColor.a -= step;
            darknessSprite.color = tempColor;
            //Debug.Log("darknessAlphaSlide:tempColor.a=" + tempColor.a);
            yield return new WaitForSeconds(timeInverval);
        }
        tempColor.a = endingAlpha;
        darknessSprite.color = tempColor;
        if (endingAlpha <= 0) instance.Darkness.SetActive(false);
    }

    void setDarknessAlpha(float alphaLevel)
    {
        Color tempColor = darknessSprite.color;
        tempColor.a = alphaLevel;
        darknessSprite.color = tempColor;
    }
    void startAlphaSlide(float startingAlpha, float endingAlpha, float time)
    {
        StartCoroutine(darknessAlphaSlide(startingAlpha, endingAlpha, time));
    }

    public static void SetDarknessAlpha_Static(float endAlpha, float transitionTime = 0)
    {
        instance.Darkness.SetActive(true);
        if(transitionTime == 0) instance.setDarknessAlpha(endAlpha);
        else instance.startAlphaSlide(instance.darknessSprite.color.a, endAlpha, transitionTime);
    }

    public static void ShowDarkness_Static(float timeLapse = 0)
    {
        instance.Darkness.SetActive(true);
        //instance.Darkness.GetComponent<SpriteMask>().enabled = false;
        //instance.Darkness.GetComponent<SpriteMask>().UpdateGIMaterials();
    }

    

    public static void HideDarkness_Static(float timeLapse = 0)
    {
        if (timeLapse > 0) instance.startAlphaSlide(1, 0, timeLapse);
        else instance.Darkness.SetActive(false);
    }

    public static void ShowLight_Static()
    {
        instance.isActive = true;
        instance.Light.SetActive(true);
        instance.Darkness.GetComponent<SpriteMask>().enabled = true;
        instance.Darkness.GetComponent<SpriteMask>().UpdateGIMaterials();
    }

    public static void HideLight_Static()
    {
        instance.isActive = false;
        instance.Light.SetActive(false);
        instance.Darkness.GetComponent<SpriteMask>().enabled = false;
        instance.Darkness.GetComponent<SpriteMask>().UpdateGIMaterials();
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        darknessSprite = Darkness.GetComponent<SpriteRenderer>();
        //backgroundTransform = transform.Find("Background").GetComponent<RectTransform>();
        //tooltipText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        HideDarkness_Static();
        HideLight_Static();
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
        //instance.show();
        ShowLight_Static();
    }
    public static void HideFlashlight_Static()
    {
        //instance.hide();
        HideLight_Static();
    }
}
