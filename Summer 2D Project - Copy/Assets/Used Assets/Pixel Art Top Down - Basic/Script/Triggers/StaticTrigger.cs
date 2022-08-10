using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StaticTrigger : MonoBehaviour
{
    [SerializeField] GameObject staticOneAppearParent;
    [SerializeField] GameObject staticOneDisappearParent;
    [SerializeField] GameObject globalStaticVolume;
    [SerializeField] GameObject globalRealtimeVolume;
    [SerializeField] GameObject accidentObject;
    [SerializeField] GameObject playerObject;
    [SerializeField] float maxDistance;

    private bool allowTransition;
    private bool objectsAppeared;
    private float vignetteTick;

    Volume volumeAccess;
    Vignette vignette;

    private void Start()
    {
        volumeAccess = globalStaticVolume.GetComponent<Volume>();
        VolumeProfile profile = volumeAccess.sharedProfile;

        if (!profile.TryGet<Vignette>(out var internalVignette)) internalVignette = profile.Add<Vignette>(false);

        vignette = internalVignette;
        vignette.intensity.value = 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision) //Displays text and allows teleport when player approaches
    {
        if (collision.gameObject.name == "Ghost") allowTransition = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ghost") allowTransition = false;
    }
    private void Update()
    {
        /* If the player is in the trigger, the respective key is pressed and the objects tied to the static 1 
         are not active, set their state to active. Reallow the object switch the moment the player releases the 
         respective key. */

        if (Input.GetKey(KeyCode.Alpha1) && allowTransition && !objectsAppeared) //The switch incorporates two different global volume profiles.
        {
            if (staticOneAppearParent.activeSelf == false)
            {
                staticOneAppearParent.SetActive(true);
                staticOneDisappearParent.SetActive(false);
                globalStaticVolume.SetActive(true);
                globalRealtimeVolume.SetActive(false);
            }
            else if (staticOneAppearParent.activeSelf == true)
            {
                staticOneAppearParent.SetActive(false);
                staticOneDisappearParent.SetActive(true);
                globalStaticVolume.SetActive(false);
                globalRealtimeVolume.SetActive(true);
            }
            objectsAppeared = true;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1) && allowTransition) objectsAppeared = false;

        if (globalStaticVolume.activeSelf) //If static objects are loaded, change vignette intensity speed with respect to distance to the death. Closer = higher speed. 
        {
            if (vignetteTick % (2 * Mathf.PI) > Mathf.PI) vignetteTick += Mathf.PI; //Skip the negative part of the sine function to avoid getting a negative intensity.

            float intensityEqual = (Mathf.Sin(vignetteTick) * 0.625f); //Intensity is modeled with the sine function and depends on vignetteTick.

            if (intensityEqual < 0.4f) vignette.intensity.value = 0.4f; //vignette intensity has a minimum value set.
            
            else vignette.intensity.value = intensityEqual; 

            float distance = Mathf.Sqrt((Mathf.Pow((playerObject.transform.position.y - accidentObject.transform.position.y), 2) + 
                Mathf.Pow((playerObject.transform.position.x - accidentObject.transform.position.x), 2)));
            float ratio = 1 - (distance / maxDistance); //maxDistance should be set to the maximum distance away from the death that the player can reach in the static.
            vignetteTick += (2f * ratio * Time.deltaTime);

        }
        else vignetteTick = 0.000f;
    }
}

