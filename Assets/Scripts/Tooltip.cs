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
        StopAllCoroutines();
        StartCoroutine(startTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tipMessage == "") return;
        hideMessage();
    }

    private void showMessage()
    {
        TooltipManager.ShowTooltip_Static(tipMessage);
    }
    private void hideMessage()
    {
        StopAllCoroutines();
        TooltipManager.HideTooltip_Static();
    }
    private IEnumerator startTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        showMessage();
    }
}
