using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TombstoneTrigger : MonoBehaviour
{
    [SerializeField] string loadLevel;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject graveyardVolume;
    [SerializeField] GameObject transitionScreen;
    [SerializeField] Gradient transitionOut;


    public GameObject tombText;
    private bool allowTeleport;
    private bool transitioning;
    private float transitionTick = 0f;

    Vignette nightVignette;
    Volume volumeAccess;
    Image transitionImage;

    private void Start() //Obtain vignette of global volume.
    {
        transitionImage = transitionScreen.GetComponent<Image>();

        volumeAccess = graveyardVolume.GetComponent<Volume>();
        VolumeProfile profile = volumeAccess.sharedProfile;

        if (!profile.TryGet<Vignette>(out var internalVignette)) internalVignette = profile.Add<Vignette>(false);

        nightVignette = internalVignette;
    }

    private void OnTriggerEnter2D(Collider2D collision) //Displays text and allows teleport when player approaches
    {
        if (collision.gameObject.name == "Ghost") 
        {
            tombText.SetActive(true);
            allowTeleport = true;
        }
                
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ghost")
        {
            tombText.SetActive(false);
            allowTeleport = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && allowTeleport)
        {
            transitionScreen.SetActive(true);
            transitioning = true;
            tombText.SetActive(false);
        }

        if (transitioning) //When transitioning, increase opacity through the gradient. When the image is fully opaque, load scene.
        {
            transitionTick += 0.01f;
            transitionImage.color = transitionOut.Evaluate(transitionTick);
            if (transitionTick >= 1.0f) SceneManager.LoadScene(loadLevel);
        }

        //Dynamically increase the vignette intensity based on distance.
        float distance = Mathf.Sqrt((Mathf.Pow((playerObject.transform.position.y - transform.position.y), 2) +
        Mathf.Pow((playerObject.transform.position.x - transform.position.x), 2)));
        if (distance < 1f) distance = 1f;

        float ratioSetIntensity = 0f;
        if (distance < 5f) ratioSetIntensity = 1 / distance; //Closer to the gravestone, higher intensity.

        nightVignette.intensity.value = ratioSetIntensity;


    }
}

