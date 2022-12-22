using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    
    public List<Quest> questList = new List<Quest>();           // Master Quest List
    public List<Quest> currentQuestList = new List<Quest>();    // Current Quest List (is Running)

    // private vars for our QuestObject

    void Awake()
    {
        if (questManager == null)
        {
            questManager = this;
        }
        else if (questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // INTERACTION WITH NPC
    public void QuestRequest(QuestObject NPCQuestObject)
    {
        // AVAILABLE QUEST
        if (NPCQuestObject.availableQuestIDs.Count > 0)
        {
            for (int i = 0; i > questList.Count; i++)
            {
                for (int j = 0; j > NPCQuestObject.availableQuestIDs.Count; j++)
                {
                    if (questList[i].id == NPCQuestObject.availableQuestIDs[j] 
                        && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        AcceptQuest(NPCQuestObject.availableQuestIDs[j]);
                        Debug.Log("Quest ID : " + NPCQuestObject.availableQuestIDs[j] + " " + questList[i].progress);

                        // quest ui manager
                    }
                }
            }
        }

        // ACTIVE QUEST
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.givenQuestIDs.Count; j++)
            {
                if (currentQuestList[i].id == NPCQuestObject.givenQuestIDs[j]
                    && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED
                    || currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    CompleteQuest(NPCQuestObject.givenQuestIDs[j]); // it checks double, so don't worry
                    Debug.Log("Quest ID : " + NPCQuestObject.givenQuestIDs[j] + " " + currentQuestList[i].progress);
                    // quest ui manager

                }
            }
        }
    }

    // ACCEPT QUEST
    public void AcceptQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.QuestProgress.ACCEPTED;
            }
        }
    }

    // GIVE UP QUEST
    public void GiveUpQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestProgress.AVAILABLE;
                currentQuestList[i].questObjectiveCount = 0;
                currentQuestList.Remove(currentQuestList[i]);
            }
        }
    }

    // COMPLETE QUEST
    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                currentQuestList[i].progress = Quest.QuestProgress.DONE;
                currentQuestList.Remove(currentQuestList[i]);
                
                // REWARD
            }
        }
        // check for chain quests
        CheckChainQuest(questID);
    }

    // CHECK CHAIN QUEST
    void CheckChainQuest(int questID)
    {
        int tempID = 0;
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].nextQuest > 0)
            {
                tempID = questList[i].nextQuest;
            }
        }
        if (tempID > 0)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].id == tempID && questList[i].progress == Quest.QuestProgress.NOT_AVAILABLE)
                {
                    // UNLOCK THE NEXT QUEST
                    questList[i].progress = Quest.QuestProgress.AVAILABLE;
                }
            }
        }
    }

    // ADD ITEMS
    public void AddItemQuest(string questObjective, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].questObjective == questObjective 
                && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }
            if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement 
                && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
            }
        }
    }

    // REMOVE MONSTERS
    public void RemoveMonsterQuest(string questObjective, int killAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].questObjective == questObjective
                && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].questObjectiveCount += killAmount;
            }
            if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement
                && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
            }
        }
    }
    
    // BOOLS
    public bool RequestAvailableQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }
    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                return true;
            }
        }
        return false;
    }
    public bool RequestCompletedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }

    // BOOLS 2  : 참조할 때 questList가 아닌 currentQuestList로 바꿔야 하는지 확인하기
    public bool CheckAvailableQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.availableQuestIDs[j]
                   && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckAcceptedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.givenQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.givenQuestIDs[j]
                   && questList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckCompleteQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.givenQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.givenQuestIDs[j]
                   && questList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
