using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentStatusBool : MonoBehaviour
{
    public TextMeshProUGUI defaultStateText;
    public TextMeshProUGUI interactedStateText;
    public bool interactedState;

    private void Start()
    {
        interactedState = false;
        defaultStateText.enabled = true;
        interactedStateText.enabled = false;
        Debug.Log("current status bool " + name);
    }

    public void CycleInteracted()
    {
        interactedState = !interactedState;
        defaultStateText.enabled = !interactedState;
        interactedStateText.enabled = interactedState;
    }

    public void SetTightened()
    {
        Debug.Log("current status bool tightened");
        interactedState = true;
        defaultStateText.enabled = false;
        interactedStateText.enabled = true;
    }

    public void SetLoosened()
    {
        Debug.Log("current status bool loosened");
        interactedState = false;
        defaultStateText.enabled = true;
        interactedStateText.enabled = false;
    }
}
