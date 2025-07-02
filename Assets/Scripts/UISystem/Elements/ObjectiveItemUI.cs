using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _sliderText;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderIcon;
    [SerializeField] private Sprite _sliderIconSpriteInProgress;
    [SerializeField] private Sprite _sliderIconSpriteCompleted;

    private int _index;
    private ObjectiveBase _objective;

    public void Initialize(int index, ObjectiveBase objective)
    {
        _index = index;
        _objective = objective;
        RefreshObjective();
    }
    
    public void RefreshObjective()
    {
        _title.text = $"{_index+1}. {_objective.Description}";
        _sliderText.text = _objective.GetProgressionText();
        _slider.value = _objective.GetProgressionValue();
        _sliderIcon.sprite = _objective.IsCompleted ? _sliderIconSpriteCompleted : _sliderIconSpriteInProgress;
    }
}
