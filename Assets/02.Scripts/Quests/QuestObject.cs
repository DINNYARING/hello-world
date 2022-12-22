using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Script that NPC can have (only have QuestID number)
public class QuestObject : MonoBehaviour
{
    private bool inTrigger = false;

    public List<int> availableQuestIDs = new List<int>();   // can get it
    public List<int> givenQuestIDs = new List<int>();       // already got it

    private readonly string playerTag = "Player";

    void Start()
    {
        
    }

    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            // quest ui manager (question marker)
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag)
        {
            inTrigger = false;
        }
    }
}
