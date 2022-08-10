using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] string specifiedDialogue;

    [SerializeField] Vector3 dialoguePos1;
    [SerializeField] Vector3 dialoguePos2;
    [SerializeField] Vector3 dialoguePos3;

    private bool dialoguePlaying;
    private string[] dialogueList;
    private int waitTick = 0;
    private int currentReadLine = 0;

    GameObject textObject;
    TextMeshPro textComponent;
    RectTransform rectComponent;


    private void Start()
    {
        //Browses public dictionary of object UniversalDialogue and finds the dialogue as specified
        Dialogue dialogueScript = GameObject.Find("UniversalDialogue").GetComponent<Dialogue>();
        dialogueList = dialogueScript.dialogue[specifiedDialogue];
        textObject = GameObject.Find("ConversationText");
        textComponent = textObject.GetComponent<TextMeshPro>();
        rectComponent = textObject.GetComponent<RectTransform>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ghost") 
        {
            dialoguePlaying = true;
        }
                
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ghost")
        {
            dialoguePlaying = false;
            textComponent.text = "";
            waitTick = 0;
        }
    }

    private void Update()
    {
        if (dialoguePlaying)
        {
            if (waitTick % 10 == 0) //Dialogue update every 10th tick
            {

                if (waitTick % 10000 == 0) //Activate next line every 10000th tick
                {
                    if ((waitTick / 10000) > (dialogueList.Length - 1)) //If waitTick exceeds specified dialogue list, stop playing
                    {
                        dialoguePlaying = false;
                        textComponent.text = "";
                        waitTick = 0;
                    }
                    currentReadLine = waitTick / 10000; //Current reading line increments every 10000 ticks
                }

                // maxCharLine is how many 10's have passed in the current 10000 interval. Displays 1 more char every 10 ticks.
                int maxCharLine = (waitTick - (currentReadLine * 10000)) / 10;

                if (maxCharLine > dialogueList[currentReadLine].Length + 8) //If line finished displaying, skip to next line.
                {
                    waitTick = ((currentReadLine + 1) * 10000) - 1;
                    currentReadLine += 1;
                }
                else
                {
                    if (dialogueList[currentReadLine][^2..] == "_1") //Reads end of dialogue line to determine dialoguePosition of dialogue text.
                    {
                        rectComponent.anchoredPosition = dialoguePos1;
                    }
                    else
                    {
                        if (dialogueList[currentReadLine][^2..] == "_2")
                        {
                            rectComponent.anchoredPosition = dialoguePos2;
                        }
                        else
                        {
                            if (dialogueList[currentReadLine][^2..] == "_3")
                            {
                                rectComponent.anchoredPosition = dialoguePos3;
                            }
                        }
                    }


                    if (maxCharLine > dialogueList[currentReadLine].Length - 2) //maxCharLine cannot exceed length of current line.
                    {
                        textComponent.text = dialogueList[currentReadLine][..(dialogueList[currentReadLine].Length - 2)];
                    }
                    else
                    {
                        textComponent.text = dialogueList[currentReadLine][..(maxCharLine)]; //Displays the first maxCharLine - 1 characters of the current line.
                    }
                }
            }
            waitTick++;
        }
    }
}

