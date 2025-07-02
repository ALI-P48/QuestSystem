using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _progressionText;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _panelInProgress;
    [SerializeField] private GameObject _panelDone;
    [SerializeField] private GameObject _infoInProgress;
    [SerializeField] private GameObject _infoDone;
    [SerializeField] private Transform _objectiveListParent;
    [SerializeField] private GameObject _objectivePrefab; //TODO Replace with ObjectPool
    [SerializeField] private RectTransform _layoutTransform;

    private Quest _quest;
    private Dictionary<string, ObjectiveItemUI> _objectivesUI;
    
    public void Initialize(Quest quest)
    {
        _quest = quest;
        RefreshQuest();
        InitializeObjectives();
    }

    public void RefreshQuest()
    {
        _title.text = _quest.Description;
        _progressionText.text = $"{_quest.GetCompletedObjectivesCount()}/{_quest.GetObjectivesCount()}";
        _icon.sprite = _quest.Icon;
        _panelInProgress.SetActive(_quest.Status == QuestStatus.InProgress);
        _panelDone.SetActive(_quest.Status == QuestStatus.Completed);
        _infoInProgress.SetActive(_quest.Status == QuestStatus.InProgress);
        _infoDone.SetActive(_quest.Status == QuestStatus.Completed);
    }

    public void RefreshObjective(string id)
    {
        if(_objectivesUI.ContainsKey(id))
        {
            _objectivesUI[id].RefreshObjective();
        }
    }

    public void RefreshObjectives()
    {
        foreach (ObjectiveItemUI objectiveItemUI in _objectivesUI.Values)
        {
            objectiveItemUI.RefreshObjective();
        }
    }

    private void InitializeObjectives()
    {
        _objectivesUI = new Dictionary<string, ObjectiveItemUI>();
        for (int i = 0; i < _quest.Objectives.Count; i++)
        {
            ObjectiveItemUI objectiveItemUI = 
                Instantiate(_objectivePrefab, _objectiveListParent).GetComponent<ObjectiveItemUI>(); //TODO Replace with ObjectPool
            objectiveItemUI.Initialize(i, _quest.Objectives[i]);
            _objectivesUI.Add(_quest.Objectives[i].ID, objectiveItemUI);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutTransform);
    }
}
