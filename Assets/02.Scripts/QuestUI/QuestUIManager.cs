using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager uiManager;

    // BOOLS
    public bool questAvailable = false;
    public bool questRunning = false;

    private bool NPCquestPanelActive = false;
    private bool questPanelActive = false;
    private bool questLogPanelActive = false;

    // PANELS
    public GameObject NPCquestPanel;
    public GameObject questPanel;
    public GameObject questLogPanel;

    // QUEST OBJECT
    private QuestObject currentQuestObject;

    // QUEST LISTS
    public List<Quest> availableQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();     // running quest

    // BUTTONS
    public GameObject qButton;      // key for Log Panel Activation
    public GameObject qLogButton;
    private List<GameObject> qButtons = new List<GameObject>();

    private GameObject acceptButton;
    private GameObject giveUpButton;
    private GameObject completeButton;

    // SPACER
    public Transform qButtonSpacer1;    // qButton for available
    public Transform qButtonSpacer2;    // running qButton
    public Transform qLogButtonSpacer;  // running in qLog

    // QUEST INFOS
    public Text questTitle;
    public Text questDescription;
    public Text questSummary;

    public Text questLogTitle;
    public Text questLogDescription;
    public Text questLogSummary;

    private void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        else if (uiManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questPanelActive = !questPanelActive;   // switch
            //ShowQuestLogPanel();
        }
    }

    // CALLED FROM QUEST OBJECT
    public void CheckQuest(QuestObject questObject)
    {
        currentQuestObject = questObject;
        QuestManager.questManager.QuestRequest(questObject);
        if ((questRunning || questAvailable) && !questPanelActive)
        {
            ShowQuestPanel();
        }
        else
        {
            Debug.Log("No Quests Available");   // Default talking should be in.
        }
    }

    // SHOW PANEL
    public void ShowQuestPanel()
    {
        questPanelActive = true;
        questPanel.SetActive(questPanelActive);
        // FILL IN DATA
    }

    // QUEST LOG



    // FILL BUTTONS FOR QUEST PANEL
    void FillQuestButtons()             // ¡â I don't need a Layout Group.
    {
        foreach (Quest availableQuest in availableQuests)
        {
            GameObject questButton = Instantiate(qButton);
        }
    }
}
