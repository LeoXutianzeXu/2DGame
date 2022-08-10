using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionIn : MonoBehaviour
{
    [SerializeField] Gradient transitionOut;

    Image transitionImage;
    private float transitionTick;

    // Start is called before the first frame update
    void Start()
    {
        transitionImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transitionTick += 0.005f;
        transitionImage.color = transitionOut.Evaluate(transitionTick);
        if (transitionTick >= 1.0f) Destroy(this);
    }
}
