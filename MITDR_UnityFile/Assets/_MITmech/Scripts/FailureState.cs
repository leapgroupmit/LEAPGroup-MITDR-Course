using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FailureState : MonoBehaviour
{
    public static FailureState Instance; //Comment Needed : Isa

    [Header("Manager References")]
    public GameObject instructionCanvas;
    public bool popout = true;

    [Header("Warning State Manager")]
    [Tooltip("Text Mesh Pro (TMP) gameObject which is used as variable text for 'Warning State' UI")]
    public GameObject warningCanvas; 
    public TextMeshProUGUI uiWarningText; //Comment Needed : Isa (Kachina Note : This seems duplicated)
    public TextMeshProUGUI warningText; //Comment Needed : Isa (Kachina Note : This seems duplicated)

    [Header("Failure State Manager")]
    [Tooltip("Text Mesh Pro (TMP) gameObject which is used as variable text for 'Failure State' UI")]
    public GameObject failureCanvas; 
    public TextMeshProUGUI uiFailureText; //Comment Needed : Isa (Kachina Note : This seems duplicated)
    public TextMeshProUGUI failureReasonText; //Comment Needed : Isa (Kachina Note : This seems duplicated)

    //Comment Needed : Isa
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //Comment Needed : Isa
    void Start()
    {
        failureCanvas.SetActive(false);
        warningCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }

    //Comment Needed : Isa
    public void SystemFailure(string errorMessage)
    {
        DismissWarning();
        Debug.Log("Failure: " + errorMessage);
        failureReasonText.text = errorMessage;
        failureCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
        if (Spinning.Instance.isSpinning)
        {
            Spinning.Instance.CycleSpin();
        }
        uiFailureText.text = errorMessage;

    }

    //Comment Needed : Isa
    public void DisplayWarning(string warningMessage)
    {
        Debug.Log("Warning: " + warningMessage);
        warningCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
        warningText.text = warningMessage;
        uiWarningText.text = warningMessage;
    }

    //Comment Needed : Isa
    public void DismissWarning()
    {
        warningCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }

    //Comment Needed : Isa
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Comment Needed : Isa
    public void TurnOffScreenDebug()
    {
        failureCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }

    //Comment Needed : Isa
    void Update()
    {
        
    }
}
