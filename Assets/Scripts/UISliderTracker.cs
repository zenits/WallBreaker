using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderTracker : MonoBehaviour
{

    [SerializeField] Slider UISlider;
    [SerializeField] IntValue intValue;
    

    private void Start() {
        intValue.onChange += onHealthChange;
        UISlider.maxValue = intValue.GetValue();
        onHealthChange();
    }

    void onHealthChange()
    {
        UISlider.value = intValue.GetValue();
    }

}
