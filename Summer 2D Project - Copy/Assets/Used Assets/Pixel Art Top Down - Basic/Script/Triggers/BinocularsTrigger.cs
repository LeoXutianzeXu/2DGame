using Cainos.PixelArtTopDown_Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularsTrigger : MonoBehaviour
{
    [SerializeField] Vector2 viewingPos;
    [SerializeField] GameObject viewTarget;

    GameObject viewingCamera;
    GameObject originalTarget;

    CameraControl followScript;
    TopDownCharacterController controlScript;

    private bool isViewing;

    private void Start()
    {
        viewingCamera = GameObject.Find("MainCamera");
        originalTarget = GameObject.Find("Ghost");

        followScript = viewingCamera.GetComponent<CameraControl>();
        controlScript = originalTarget.GetComponent<TopDownCharacterController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //Allows viewing when player approaches
    {
        if (collision.gameObject.name == "Ghost") isViewing = true;         
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ghost")
        {
            isViewing = false;
            followScript.target = originalTarget.transform;
            followScript.lerpSpeed = 2.5f;
        }
    }
    private void Update() //After pressing E in the trigger, instantaneously snap the camera to target position of binoculars
    {
        if (Input.GetKeyDown(KeyCode.E) && isViewing)
        {
            if (followScript.target == originalTarget.transform)
            {
                followScript.lerpSpeed = 10000f;
                followScript.target = viewTarget.transform;
                controlScript.movementEnabled = false;
                
            }
            else
            {
                followScript.target = originalTarget.transform;
                controlScript.movementEnabled = true;
            }
        }
    }
}

