using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script stores every dialogue conversation in a public Dictionary.*/

public class Dialogue : MonoBehaviour
{

    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Start()
    {
        //Falkenberg grave dialogue
        dialogue.Add("Grave1", new string[] {
            "Grandma, grandma! How was he like?_2",
            "I remember that moment of him...vividly still to this day._1",
            "It is a pity...his fearlessness shone well over it, though._1",
            "But...What did he do? Why are we coming here?_2",
            "He he...you'd have to grow older to understand._1",
            "You promised, grandma, no more secrets from me!_2",
            "All I can say is, what he did is the biggest unreturned favor to our family._1",
            "..._3",
            "...Hush now from now on. We should respect the others here._1"
        });
    }
}
