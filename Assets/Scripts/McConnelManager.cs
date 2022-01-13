using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class McConnelManager: MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;

    public static McConnelManager instance;
    private bool isActive = false;
    public static bool isEvent = false;
    private TextMeshProUGUI tooltipText;
    //private Text tooltipText;
    private RectTransform backgroundTransform;
    public List<string> quotes;
    public ItemSO mcTurtle;
    float timer;
    private void showTooltip(string tooltipString)
    {
        

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.position = mcTurtle.UIInstance.transform.position;
        if (tooltipString == "" || tooltipString == " ") return;
        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + 2 * textPaddingSize > 200 ? 200 : tooltipText.preferredWidth + 2 * textPaddingSize, tooltipText.preferredHeight + 2 * textPaddingSize);
        backgroundTransform.sizeDelta = backgroundSize;
        isActive = true;

        backgroundTransform.gameObject.SetActive(true);
        tooltipText.gameObject.SetActive(true);
        //gameObject.SetActive(true);
        //Update();
    }
    private void hideTooltip()
    {
        backgroundTransform.gameObject.SetActive(false);
        tooltipText.gameObject.SetActive(false);
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
    private void Start()
    { 
        //mcTurtle = IncManager.instance.Get<ItemSO>("mcTurtle");
        //IncManager.instance.Add(mcTurtle, 1);
        //startQuote("mc turtle got stuff to say");
        timer = 10;
        startQuotes();
    }

    void startQuotes()
    {
        isActive = true;
    }

    void stopQuote()
    {
        backgroundTransform.gameObject.SetActive(false);
        tooltipText.gameObject.SetActive(false);
    }

    void startQuote(string quote)
    {
        Debug.Log("McConnelManager:startQuote");
        if (mcTurtle.getAmount() > 0)
        {
            timer = 25;
            instance.showTooltip(quote);
            StartCoroutine(stopIn(6));
        }
    }

    public void startRandomQuoteDelay(float delay = 0)
    {
        Debug.Log("McConnelManager:start random quote delay");
        if (delay <= 0.0001) startQuote(quotes[Random.Range(0, quotes.Count - 1)]);
        else startQuoteDelay(quotes[Random.Range(0, quotes.Count - 1)], delay);
    }

    public IEnumerator startQuoteDelay(string quote, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        startQuote(quote);
    }

    IEnumerator stopIn(float time)
    {
        yield return new WaitForSeconds(time);
        instance.stopQuote();
    }

    // Update is called once per frame
    void Update()
    {
        //every 30s if incremental manager
        
        if (isActive)
        {
            timer += Time.deltaTime;
            if(timer>45)
            {
                //transform.position = Input.mousePosition;

                startRandomQuoteDelay(0);
                timer = 0;
            }
            
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
