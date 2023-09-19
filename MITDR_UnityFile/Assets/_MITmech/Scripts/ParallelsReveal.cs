using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParallelsReveal : MonoBehaviour
{
    public MeshRenderer parallel1, parallel2;
    public GameObject parallel1Slide, parallel2Slide;
    public Quaternion parallel1SlideRot, parallel2SlideRot;

    public GameObject parallel1SecureSlide, parallel2SecureSlide;
    public Quaternion parallel1SecureSlideRot, parallel2SecureSlideRot;

    public GameObject parallelSnapZone;
    public bool revealed = false;


    public TextMeshProUGUI parallelStatus;
    public GameObject parallelstep;

    // Start is called before the first frame update
    void Start()
    {
        parallelSnapZone.SetActive(true);
        parallel1SlideRot = parallel1Slide.transform.rotation;
        parallel2SlideRot = parallel2Slide.transform.rotation;
        parallel1SecureSlideRot = parallel1SecureSlide.transform.rotation;
        parallel2SecureSlideRot = parallel2SecureSlide.transform.rotation;
    }

    private void OnEnable()
    {
        revealed = true;
        parallel1.enabled = true;
        parallel2.enabled = true;

    }

    private void OnDisable()
    {
        revealed = false;
        parallel1.enabled = false;
        parallel2.enabled = false;

        parallel1Slide.SetActive(false);
        parallel2Slide.SetActive(false);

        parallel1SecureSlide.SetActive(false);
        parallel2SecureSlide.SetActive(false);
        //parallelSnapZone.SetActive(false);
    }

    public void SlideParallel(){
        parallel1.enabled = false;
        parallel2.enabled = false;
        parallel1Slide.SetActive(true);
        parallel2Slide.SetActive(true);
        parallelStatus.text = "Tight";
    }

    public void SlideSecuredParallel() {
        parallel1.enabled = false;
        parallel2.enabled = false;
        parallel1SecureSlide.SetActive(true);
        parallel2SecureSlide.SetActive(true);
        parallelStatus.text = "Tight";
    }

    public void ReverseParallel() {

        parallel1Slide.transform.rotation = parallel1SlideRot;
        parallel2Slide.transform.rotation = parallel2SlideRot;
        parallel1Slide.SetActive(false);
        parallel2Slide.SetActive(false);

        parallel1SecureSlide.transform.rotation = parallel1SecureSlideRot;
        parallel2SecureSlide.transform.rotation = parallel2SecureSlideRot;
        parallel1SecureSlide.SetActive(false);
        parallel2SecureSlide.SetActive(false);

        parallel1.enabled = true;
        parallel2.enabled = true;
        parallelstep.SetActive(false);
    }



    

}
