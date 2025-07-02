using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatItemUI : MonoBehaviour
{
    [SerializeField] private StatItemUIType _type;
    [SerializeField] private int _step;
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private Button _addButton;

    public void Initialize()
    {
        RefreshValues();
        _addButton.onClick.RemoveAllListeners();
        _addButton.onClick.AddListener(OnClickAdd);
    }

    public void RefreshValues()
    {
        if (_type == StatItemUIType.Cash)
        {
            _valueText.text = UserManager.Instance.GetCash().ToString();
        }
        else if (_type == StatItemUIType.Gems)
        {
            _valueText.text = UserManager.Instance.GetGems().ToString();
        }
        else if (_type == StatItemUIType.Cups)
        {
            _valueText.text = UserManager.Instance.GetCups().ToString();
        }
        else if (_type == StatItemUIType.Level)
        {
            _valueText.text = UserManager.Instance.GetLevel().ToString();
        }
        else if (_type == StatItemUIType.Kills)
        {
            _valueText.text = UserManager.Instance.GetKills().ToString();
        }
    }

    private void OnClickAdd()
    {
        if (_type == StatItemUIType.Cash)
        {
            EventSystem.Raise(new AddCashEvent(_step));
        }
        else if (_type == StatItemUIType.Gems)
        {
            EventSystem.Raise(new AddGemEvent(_step));
        }
        else if (_type == StatItemUIType.Cups)
        {
            EventSystem.Raise(new AddCupEvent(_step));
        }
        else if (_type == StatItemUIType.Level)
        {
            EventSystem.Raise(new LevelPassedEvent());
        }
        else if (_type == StatItemUIType.Kills)
        {
            EventSystem.Raise(new EnemyKilledEvent());
        }
    }
}
