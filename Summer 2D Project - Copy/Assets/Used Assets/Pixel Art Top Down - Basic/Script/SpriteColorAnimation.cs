using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //animate the sprite color base on the gradient and time
    public class SpriteColorAnimation : MonoBehaviour
    {
        [SerializeField] GameObject playerObject;

        public Gradient gradientClose;

        private SpriteRenderer sr;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (sr)
            {

                //Dynamically change the alpha of the appearing rune wall based on distance.
                float distance = Mathf.Sqrt((Mathf.Pow((playerObject.transform.position.y - transform.position.y), 2) +
                Mathf.Pow((playerObject.transform.position.x - transform.position.x), 2)));
                if (distance < 1f) distance = 1f;

                float ratioSetAlpha = 1 / distance; //Closer to the wall, more opaque it will be.
                sr.color = gradientClose.Evaluate(ratioSetAlpha);
            }

        }
    }
}
