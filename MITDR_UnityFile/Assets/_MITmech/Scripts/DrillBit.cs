using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spinning;
using static ClampMovement;
using static FailureState;

public class DrillBit : MonoBehaviour
{
    public Transform rotatingWorldZero;
    public Transform entryPoint;
    public float drillThickness;
    Drillable drill;

    public MouseRotate zRotator;
    float storedAngleBounds;

    public bool isCenterPointDrill;
    public float minDepthToCenterPoint = -.1f;
    public float depthTooDangerous = -10f;

    public AudioSource drillingIntoMachineSource;

    bool newHoleStarted;
    bool justSnappedIn;

    public bool hasSpace;
    public float scaleAdjuster = 1;


    // Start is called before the first frame update
    void Start()
    {
        //justSnappedIn = false;
        newHoleStarted = false;
        drillThickness = drillThickness * scaleAdjuster;
        minDepthToCenterPoint = minDepthToCenterPoint * scaleAdjuster;
        depthTooDangerous = depthTooDangerous * scaleAdjuster;
        zRotator = GameObject.Find("Quill Rotate [ Button ]").GetComponent<MouseRotate>(); // temp

}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drillable"))
        {
            drill = other.gameObject.GetComponent<Drillable>();
            if (ClampMovement.Instance.isClamped)
            {
                if (Spinning.Instance.isSpinning)
                {

                    if (Spinning.Instance.spinSpeed < drill.minSpinSpeed)
                    {
                        FailureState.Instance.SystemFailure("Your drill was spinning too slowly!");
                    }
                    else if (Spinning.Instance.spinSpeed > drill.maxSpinSpeed)
                    {
                        FailureState.Instance.SystemFailure("Your drill was spinning too quickly!");
                    }
                    else
                    {
                        drill.NewHole(drillThickness, rotatingWorldZero.InverseTransformPoint(entryPoint.position), isCenterPointDrill);
                        newHoleStarted = true;
                    }

                }
                else if (Jogging.Instance.isCurrentJogging)
                {
                    FailureState.Instance.SystemFailure("Do not use the jogging controls to center your drill, you can break the machine.");
                }
                else if (justSnappedIn)
                {
                    // remove the drill!
                    Debug.Log("other object is " + other.name + " of " + other.transform.parent.gameObject.name);
                    GameObject snapzone = GameObject.Find("DrillBitRemoveButton");
                    Debug.Log(snapzone.name + "here is the name :)");
                    snapzone.GetComponent<RemoveFromSnap>().RemoveCurrentObject();
                    FailureState.Instance.SystemFailure("You didn't have enough space to put this drill in.");
                }
                else
                {
                    Debug.Log("Just snapped in is: " + justSnappedIn);
                    Debug.Log("Locking z rotation");
                    //storedAngleBounds = zRotator.minAngle;
                    //zRotator.minAngle = zRotator.storedRotation;
                    //Debug.Log("YOUR DRILL IS NOW BROKEN.");
                    //FailureState.Instance.SystemFailure("Your drill wasn't spinning when it touched the drillable piece.");
                }
            }
            else
            {
                FailureState.Instance.SystemFailure("Your block wasn't clamped when you touched it with your drill.");
            }

        }
        else if (other.gameObject.CompareTag("Untagged"))
        {
            if (Spinning.Instance.isSpinning)
            {
                Debug.Log("You should not be drilling that! Stop it now! It was the: " + other.gameObject.name);
                FailureState.Instance.SystemFailure("Your spinning drill touched something it shouldn't. Did you forget the parallels?");
                drillingIntoMachineSource?.Play();
            }
            else if (justSnappedIn)
            {
                // remove the drill!
                Debug.Log("other object is " + other.name + " of "+ other.transform.parent.gameObject.name);
                GameObject snapzone = GameObject.Find("DrillBitRemoveButton");
                snapzone.GetComponent<RemoveFromSnap>().RemoveCurrentObject();

                FailureState.Instance.SystemFailure("You didn't have enough space to put this drill in.");
            }
            else
            {
                //Debug.Log("Locking z rotation");
                //storedAngleBounds = zRotator.minAngle;
                //zRotator.minAngle = zRotator.storedRotation;
                Debug.Log("Your drill bit hit: " + other.gameObject.name);
                FailureState.Instance.DisplayWarning("Your drill bit hit something, be careful.");
            }

        }
        else if (other.gameObject.CompareTag("Controller"))
        {
            if (Spinning.Instance.isSpinning)
            {
                FailureState.Instance.SystemFailure("You touched a spinning drill with your hands.");
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(WeJustSnappedIn());
    }

    IEnumerator WeJustSnappedIn()
    {

        justSnappedIn = true;
        Debug.Log("We just snapped in!" + justSnappedIn);
        yield return new WaitForSeconds(1f);
        justSnappedIn = false;
        Debug.Log("The time has passed." + justSnappedIn);
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Drillable"))
        {
            if (Spinning.Instance.isSpinning)
            {
                if (!newHoleStarted)
                {
                    drill.NewHole(drillThickness, rotatingWorldZero.InverseTransformPoint(entryPoint.position), isCenterPointDrill);
                }
                drill.UpdateHoleDepth(rotatingWorldZero.InverseTransformPoint(entryPoint.position), this, isCenterPointDrill);
            }
            /*
            else
            {
                Debug.Log("Locking z rotation");
                storedAngleBounds = zRotator.angleBounds;
                zRotator.angleBounds = Mathf.Abs(zRotator.storedRotation);
            }
            */

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Drillable"))
        {
            newHoleStarted = false;
            if (Spinning.Instance.isSpinning)
            {
                other.gameObject.GetComponent<Drillable>().DisplayHoleData();
                other.gameObject.GetComponent<Drillable>().SilenceSound();
                //Just in case, because of a bug
                Debug.Log("Resetting angle bounds to " + -zRotator.angleBounds);
                //zRotator.minAngle = -zRotator.angleBounds;
            }
            else
            {
                Debug.Log("Resetting angle bounds to " + -zRotator.angleBounds);
                //zRotator.minAngle = -zRotator.angleBounds;
                other.gameObject.GetComponent<Drillable>().SilenceSound();
            }

        }
        else if (other.gameObject.CompareTag("Untagged"))
        {
            Debug.Log("Resetting angle bounds to " + -zRotator.angleBounds);
            //zRotator.minAngle = -zRotator.angleBounds;
        }

    }

    // Update is called once per frame
    void Update()
    {
            //Debug.Log("just snapped in is: " + justSnappedIn);
    }
}
