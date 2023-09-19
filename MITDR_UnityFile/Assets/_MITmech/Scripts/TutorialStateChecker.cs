using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEngine.Video;

public class TutorialStateChecker : MonoBehaviour
{
    public TutorialStep[] tutorialSteps;
    public TextMeshProUGUI currentStepText;
    int currentStep;
    public GameObject EnableOnComplete;

    public bool inTutorial = true;

    //public VideoPlayer videoPlayer;


    // Start is called before the first frame update
    void Start()
    {

    }
    public void Update() {
        UpdateSteps();
    }

    public void UpdateSteps()
    {
        if (inTutorial)
        {
            Debug.Log("Updating Steps.");

            for (int i = 0; i < tutorialSteps.Length; i++)
            {
                TutorialStep step = tutorialSteps[i];
                if (!step.isCurrentlyComplete && !step.canBeIgnoredInCheck)
                {
                    Debug.Log("currently showing: " + step.gameObject.name);
                    currentStepText.text = /*"Next Step: " + "\n" +*/ step.promptText;
                    step.shown = true;

                    // showing for each step
                    if (currentStep != i)
                    {
                        if (tutorialSteps[currentStep].showGameObject)
                        {
                            tutorialSteps[currentStep].showOnStep.SetActive(false);
                            //tutorialSteps[currentStep].activateVideoPlayer.SetActive(false);
                            
                        }

                        currentStep = i;

                        if (tutorialSteps[currentStep].showGameObject)
                        {
                            tutorialSteps[currentStep].showOnStep.SetActive(true);
                            

                        }
                        /*
                        if (tutorialSteps[currentStep].showVideo) {
                            tutorialSteps[currentStep].activateVideoPlayer.SetActive(true);
                            //videoPlayer.clip = tutorialSteps[currentStep].tutorialVideoStep;

                        }
                        */
                    }

                    // showing on complete
                    if (currentStep == (tutorialSteps.Length - 1))
                    {
                        Debug.Log("showing enable on complete");
                        //EnableOnComplete.SetActive(true);
                        inTutorial = false;
                    }
                    return;
                }
            }
        }

    }

    public bool lastStepComplete(string s) {

        for (int i = 0; i < tutorialSteps.Length; i++)
        {
            if (s == tutorialSteps[i].gameObject.name) {
                return i == currentStep;
            }
        }
        Debug.Log("This message should not be reached");
        return false;
    }
    


    // Update is called once per frame
    public void RestartTutorial() {
        inTutorial = true;
        for (int i = 1; i < tutorialSteps.Length; i++) // do not reset step 0
        {
            TutorialStep step = tutorialSteps[i];
            step.shown = false;
            step.canBeIgnoredInCheck = false;
            step.isCurrentlyComplete = false;
        }
    }

    public void StartTutorial() {
        RestartTutorial();
        // find the earliest incomplete step
        for (int i = 0; i < tutorialSteps.Length; i++)
        {
            TutorialStep step = tutorialSteps[i];
            if (!step.isCurrentlyComplete && !step.canBeIgnoredInCheck)
            {
                Debug.Log("currently showing: " + step.gameObject.name);
                currentStepText.text = /*"Next Step: " + "\n" +*/ step.promptText;
                step.shown = true;
                currentStep = i;

                if (tutorialSteps[currentStep].showGameObject)
                {
                    tutorialSteps[currentStep].showOnStep.SetActive(true);
                }

                // showing on complete
                if (currentStep == (tutorialSteps.Length - 1))
                {
                    Debug.Log("showing enable on complete");
                    EnableOnComplete.SetActive(true);
                    inTutorial = false;
                }
                return;
            }
            }




    }

}
