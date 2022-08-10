using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{

    //let camera follow target
    public class CameraControl : MonoBehaviour
    {
        public Transform target;
        public float lerpSpeed = 1.0f;

        private Vector3 offset;

        private Vector3 targetPos;
        
        AudioSource[] sourceList;

        [SerializeField] GameObject StaticVolume;


        private void Start()
        {
            sourceList = GetComponents<AudioSource>();
            foreach (AudioSource chosenSource in sourceList) //Before the first frame, set volume of intense instruments to zero and medium instruments to 0.5f.
            {
                if (chosenSource.clip.name[0..2] == "s_") chosenSource.volume = 0f;
                if (chosenSource.clip.name[0..2] == "h_") chosenSource.volume = 0.5f;
            }

            if (target == null) return;

            offset = transform.position - target.position;

        }
        private void Update()
        {
            if (target == null) return;

            targetPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);

            if (StaticVolume.activeSelf) //If the static scene is active, increase the volume of intense instruments. Vice versa.
            {
                foreach (AudioSource chosenSource in sourceList)
                {
                    if (chosenSource.clip.name[0..2] == "s_") chosenSource.volume += 0.0025f;
                    if (chosenSource.clip.name[0..2] == "h_") chosenSource.volume += 0.00125f;
                }
            }
            else
            {
                foreach (AudioSource chosenSource in sourceList)
                {
                    if (chosenSource.clip.name[0..2] == "s_") chosenSource.volume -= 0.0025f;
                    if(chosenSource.clip.name[0..2] == "s_" && chosenSource.volume > 0.5f) chosenSource.volume -= 0.00125f;
                }
            }

        }

    }
}
