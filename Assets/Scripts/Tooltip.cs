using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string tipMessage = "";
    private float timeToWait = 1.0f;

    public void setTooltip(string message)
    {
        tipMessage = message;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tipMessage == "") return;
        showMessage();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tipMessage == "") return;
        hideMessage();
    }

    public void clickReset(float _timeToWait = -1)
    {
        if (_timeToWait == -1) _timeToWait = timeToWait;
        StopAllCoroutines();
        TooltipManager.HideTooltip_Static();
        StartCoroutine(startTimer(_timeToWait));
        
    }

    public void setHoverDelay(float _timeToWait)
    {
        timeToWait = _timeToWait;
    }

    public void showMessage(float _timeToWait = -1)
    {
        if (_timeToWait == -1) _timeToWait = timeToWait;
        StopAllCoroutines();
        StartCoroutine(startTimer(_timeToWait));
    }
    public void hideMessage()
    {
        StopAllCoroutines();
        TooltipManager.HideTooltip_Static();
    }

    void managerDisplay()
    {
        TooltipManager.ShowTooltip_Static(tipMessage);
    }
    private IEnumerator startTimer(float _timeToWait)
    {
        yield return new WaitForSeconds(_timeToWait);
        managerDisplay();
    }
}
