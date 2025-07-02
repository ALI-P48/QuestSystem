using System.Collections.Generic;

public class UIManager : Manager
{
    public static UIManager Instance;

    public ScreenID ActiveScreenID
    {
        get;
        private set;
    }
    
    public BaseScreen ActiveScreen => _screens[ActiveScreenID];

    private Dictionary<ScreenID, BaseScreen> _screens;

    private void Awake()
    {
        _screens = new Dictionary<ScreenID, BaseScreen>();
        BaseScreen[] screenList = GetComponentsInChildren<BaseScreen>();
        for (int i = 0; i < screenList.Length; i++)
        {
            _screens.Add(screenList[i].ID, screenList[i]);
        }
        ActiveScreenID = ScreenID.None;
    }
    
    public override void CreateInstance()
    {
        Instance = this;
    }

    public override void Initialize()
    {
        foreach (ScreenID id in _screens.Keys)
        {
            _screens[id].Initialize();
            _screens[id].gameObject.SetActive(false);
        }
        ShowScreen(ScreenID.Quests);
    }

    public void ShowScreen(ScreenID screenID)
    {
        if (ActiveScreenID != ScreenID.None)
        {
            _screens[ActiveScreenID].gameObject.SetActive(false);
        }
        _screens[screenID].gameObject.SetActive(true);
        _screens[screenID].Show();
        ActiveScreenID = screenID;
    }

    protected override void OnGameEventReceived(GameEvent gameEvent)
    {
        if (ActiveScreenID != ScreenID.None)
        {
            ActiveScreen.OnGameEventReceived(gameEvent);
        }
    }
}
