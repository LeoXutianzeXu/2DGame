using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestCollide : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) //Displays text and allows teleport when player approaches
    {
        print(collision.gameObject.name + " has entered");
                
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        print(collision.gameObject.name + " has exited");
    }
}

