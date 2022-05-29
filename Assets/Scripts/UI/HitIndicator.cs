using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitIndicator : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private WhiteBall _whiteBall;

    void Start()
    {
        _slider.maxValue = _whiteBall.MaxHoldTime;
    }

    public void FillSlider(float value)
    {
        _slider.value = value;
    }

    public void ToggleSliderState(bool state)
    {
        _slider.value = 0;
        _slider.gameObject.SetActive(state);
    }
}
