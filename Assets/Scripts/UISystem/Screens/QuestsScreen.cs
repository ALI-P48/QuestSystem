using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsScreen : BaseScreen
{
    public override ScreenID ID => ScreenID.Quests;
    
    [SerializeField] private Transform _questListParent;
    [SerializeField] private GameObject _questPrefab; //TODO Replace with ObjectPool
    [SerializeField] private RectTransform _layoutTransform;
    [SerializeField] private StatItemUI[] _statItemsUI;
    [SerializeField] private Button _resetButton;


    private Dictionary<string, QuestItemUI> _questsUI;

    public override void Initialize()
    {
        _questsUI = new Dictionary<string, QuestItemUI>();
        for (int i = 0; i < QuestManager.Instance.Quests.Count; i++)
        {
            Quest quest = QuestManager.Instance.Quests[i];
            QuestItemUI questItemUI = 
                Instantiate(_questPrefab, _questListParent).GetComponent<QuestItemUI>(); //TODO Replace with ObjectPool
            questItemUI.Initialize(quest);
            _questsUI.Add(quest.ID, questItemUI);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutTransform);

        for (int i = 0; i < _statItemsUI.Length; i++)
        {
            _statItemsUI[i].Initialize();
        }
        _resetButton.onClick.RemoveAllListeners();
        _resetButton.onClick.AddListener(OnClickResetButton);
    }

    public override void Show()
    {
        
    }

    public override void OnGameEventReceived(GameEvent gameEvent)
    {
        if (gameEvent is ResetUserEvent)
        {
            foreach (QuestItemUI questItemUI in _questsUI.Values)
            {
                questItemUI.RefreshObjectives();
                questItemUI.RefreshQuest();
            }
            for (int i = 0; i < _statItemsUI.Length; i++)
            {
                _statItemsUI[i].RefreshValues();
            }
        }
        else if (gameEvent is ObjectiveUpdatedEvent)
        {
            ObjectiveUpdatedEvent objectiveEvent = (ObjectiveUpdatedEvent)gameEvent;
            foreach (QuestItemUI questItemUI in _questsUI.Values)
            {
                questItemUI.RefreshObjective(objectiveEvent.ID);
            }
        }
        else if (gameEvent is ObjectiveCompletedEvent)
        {
            ObjectiveCompletedEvent objectiveEvent = (ObjectiveCompletedEvent)gameEvent;
            foreach (QuestItemUI questItemUI in _questsUI.Values)
            {
                questItemUI.RefreshObjective(objectiveEvent.ID);
            }
        }
        else if (gameEvent is QuestUpdatedEvent)
        {
            QuestUpdatedEvent questEvent = (QuestUpdatedEvent)gameEvent;
            _questsUI[questEvent.ID].RefreshQuest();
        } else if (gameEvent is EnemyKilledEvent
                   || gameEvent is AddCashEvent
                   || gameEvent is AddGemEvent
                   || gameEvent is AddCupEvent
                   || gameEvent is LevelPassedEvent)
        {
            for (int i = 0; i < _statItemsUI.Length; i++)
            {
                _statItemsUI[i].RefreshValues();
            }
        }
    }

    private void OnClickResetButton()
    {
        UserManager.Instance.Reset();
    }
}