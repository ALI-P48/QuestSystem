using System.Diagnostics;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UserManager : Manager
{
    public static UserManager Instance;

    private bool _isChanged;
    private UserData _data;
    
    private const string FILE_NAME = "UserData.json";
    private static string filePath => Path.Combine(Application.persistentDataPath, FILE_NAME);
    
    public override void CreateInstance()
    {
        Instance = this;
    }

    public override void Initialize()
    {
        Load();
    }

    public int GetLevel()
    {
        return _data.Level;
    }

    public int GetCash()
    {
        return _data.Cash;
    }

    public int GetGems()
    {
        return _data.Gems;
    }

    public int GetCups()
    {
        return _data.Cups;
    }

    public int GetKills()
    {
        return _data.Kills;
    }

    public QuestStatus GetQuestStatus(string id)
    {
        if(_data.QuestsStatus.ContainsKey(id))
        {
            return _data.QuestsStatus[id];
        }
        else
        {
            return (QuestStatus)0;
        }
    }

    public bool IsObjectiveDone(string id)
    {
        return _data.ObjectivesDone.Contains(id);
    }

    public void Reset()
    {
        _data = new UserData();
        Save(true);
        EventSystem.Raise(new ResetUserEvent());
    }
    
    protected override void OnGameEventReceived(GameEvent gameEvent)
    {
        if (gameEvent is EnemyKilledEvent)
        {
            _data.Kills++;
            _isChanged = true;
        }
        else if (gameEvent is AddCashEvent)
        {
            AddCashEvent evnt = (AddCashEvent)gameEvent;
            _data.Cash+=evnt.Amount;
            _isChanged = true;
        }
        else if (gameEvent is AddGemEvent)
        {
            AddGemEvent evnt = (AddGemEvent)gameEvent;
            _data.Gems+=evnt.Amount;
            _isChanged = true;
        }
        else if (gameEvent is AddCupEvent)
        {
            AddCupEvent evnt = (AddCupEvent)gameEvent;
            _data.Cups+=evnt.Amount;
            _isChanged = true;
        }
        else  if (gameEvent is LevelPassedEvent)
        {
            _data.Level++;
            _isChanged = true;
        }
        else if (gameEvent is ObjectiveCompletedEvent)
        {
            ObjectiveCompletedEvent objectiveEvent = (ObjectiveCompletedEvent)gameEvent;
            if (!_data.ObjectivesDone.Contains(objectiveEvent.ID))
            {
                _data.ObjectivesDone.Add(objectiveEvent.ID);
                _isChanged = true;
            }
        }
        else if (gameEvent is QuestUpdatedEvent)
        {
            QuestUpdatedEvent questEvent = (QuestUpdatedEvent)gameEvent;

            if (!_data.QuestsStatus.ContainsKey(questEvent.ID))
            {
                _data.QuestsStatus.Add(questEvent.ID, questEvent.Status);
                _isChanged = true;
            }
            else if (_data.QuestsStatus[questEvent.ID] != questEvent.Status)
            {
                _data.QuestsStatus[questEvent.ID] = questEvent.Status;
                _isChanged = true;
            }
        }
        Save();
    }
    
    private void Save(bool forceSave = false)
    {
        if (_isChanged || forceSave)
        {
            string json = JsonUtility.ToJson(_data, true);
            File.WriteAllText(filePath, json);
            _isChanged = false;
        }
    }

    private void Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _data = JsonUtility.FromJson<UserData>(json);
        }
        else
        {
            _data = new UserData();
            Save();
        }
        _isChanged = false;
    }
    
    
#if UNITY_EDITOR
    [MenuItem("Tools/UserData/View")]
    public static void ViewUserData()
    {
        if (File.Exists(filePath))
        {
            string directoryPath = Path.GetDirectoryName(filePath);
#if UNITY_EDITOR_WIN
            Process.Start("explorer.exe", directoryPath);
#elif UNITY_EDITOR_OSX
                Process.Start("open", directoryPath);
#endif
        }
    }
    
    [MenuItem("Tools/UserData/Reset")]
    public static void DeleteUserData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
#endif
}
