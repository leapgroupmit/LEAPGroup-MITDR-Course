using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Spinning;

public class CycleTextObject : MonoBehaviour
{
    public TextMeshProUGUI varriableText;
        
    //public TextMeshProUGUI looseText, tightText;
    public bool isTight;
    public SnapZone quillSnapZone;

    private void Start()
    {
        //isTight = tightText.gameObject.activeSelf;
        Spinning.SpinningEvent += CheckIfTight;
        varriableText.text = "Tight";
    }

    public void CycleText()
    {
        isTight = !isTight;
        //looseText.gameObject.SetActive(!looseText.gameObject.activeSelf);
        //tightText.gameObject.SetActive(!tightText.gameObject.activeSelf);

        if (isTight)
        {
            varriableText.text = "Tight";
        }

        if (!isTight)
        {
            varriableText.text = "Loose";
        }
    }

    void CheckIfTight(bool isSpinning)
    {
        if (isSpinning)
        {
            if (!isTight && quillSnapZone.currentlySnapped == SnapZoneManager.SnapObjectType.Chuck)
            {
                FailureState.Instance.SystemFailure("You started spinning before the chuck was tightened.");
            }
        }
    }
}
