using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitIndicator : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private WhiteBall _whiteBall;
    [SerializeField] private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _slider.maxValue = _whiteBall.MaxHitPower;
    }

    public void FillSlider(float value)
    {
        _slider.value = value / _slider.maxValue;
    }

    public void ToggleSliderState(bool state)
    {
        _slider.value = 0;
        _slider.gameObject.SetActive(state);
    }
}
