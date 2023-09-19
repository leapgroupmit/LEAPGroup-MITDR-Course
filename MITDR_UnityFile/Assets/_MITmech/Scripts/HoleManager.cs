using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public GameObject bottomhole;
    public GameObject counterTophole;
    public GameObject counterBottomhole;
    public bool isCounter = false;
    bool drilledThrough = false;
    public bool deburred = false;

    public Material holeMaterial;
    public Material deburredHoleMaterial;
    public Material selectHoleMaterial;

    public TutorialStepButtonPressed p;
    // adjust hole debur status


    private void Start()
    {
        p = GameObject.Find("Step 38").GetComponent<TutorialStepButtonPressed>();
    }
    public void UpdateHoleDebur()
    {
        if (!deburred) {
            p.Press();
            deburred = true;
            GetComponent<Renderer>().material = deburredHoleMaterial;
            if (isCounter)
            {
                counterTophole.GetComponent<Renderer>().material = deburredHoleMaterial;
            }
            if (drilledThrough)
            {
                bottomhole.GetComponent<Renderer>().material = deburredHoleMaterial;
                if (isCounter)
                {
                    counterBottomhole.GetComponent<Renderer>().material = deburredHoleMaterial;
                }
            }
            Destroy(GetComponent<CapsuleCollider>());
        }
    }
    // adjust hole dimension
    public void adjustDimension(float d) {
        transform.localScale *= d;
        if (isCounter)
        {
            // do the ame for counter top hole
            counterTophole.transform.localScale *= d;
        }

        //do the same for bottom hole
        if (drilledThrough)
        {
            bottomhole.transform.localScale *= d;

            if (isCounter)
            {
                // do the same for counter bottom hole
                counterBottomhole.transform.localScale *= d;
            }
        }
    }

    public void DrillThrough() {
        drilledThrough = true;
        if (deburred) {
            bottomhole.GetComponent<Renderer>().material = deburredHoleMaterial;
            if (isCounter)
            {
                counterBottomhole.GetComponent<Renderer>().material = deburredHoleMaterial;
            }
        }
    }

    public void Select() {
        GetComponent<Renderer>().material = selectHoleMaterial;
        if (isCounter)
        {
            counterTophole.GetComponent<Renderer>().material = selectHoleMaterial;
        }
    }

    public void Unselect() {
        GetComponent<Renderer>().material = holeMaterial;
        if (isCounter)
        {
            counterTophole.GetComponent<Renderer>().material = holeMaterial;
        }
    }
}
