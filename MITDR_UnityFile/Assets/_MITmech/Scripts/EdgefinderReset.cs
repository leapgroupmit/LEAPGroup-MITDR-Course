using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgefinderReset : MonoBehaviour
{
    public EdgeFinding ef;
    public EdgeFinderPopOut efp;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Drillable") && ef.isPopped)
        {
            efp.Reset();
        }
    }
}
