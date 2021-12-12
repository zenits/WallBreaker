using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntValue : MonoBehaviour
{
    #region [SerializeField]
    [SerializeField] int value = 10;
    [SerializeField] int _minValue = 0;
    [SerializeField] int _maxValue = 10;
    #endregion

    #region [Properties]
    public int MinValue { get => _minValue; set => _minValue = value; }
    public int MaxValue { get => _maxValue; set => _maxValue = value; }
    #endregion


    #region [Events]
    public event Action onChange;
    #endregion


    #region Methods
    
    public int GetValue()
    {
        return value;
    }

    public int Decrease(int decreaseValue)
    {
        int result = Mathf.Max(MinValue, decreaseValue - this.value);
        this.value = Mathf.Max(MinValue, this.value - decreaseValue);
        Notify();
        return result;
    }

    public int Increase(int increaseValue)
    {
        int result = 0;
        if (this.value + increaseValue > MaxValue)
        {
            result = MaxValue - increaseValue + this.value;
            this.value = MaxValue;
        }
        else
            this.value += increaseValue;

        Notify();
        return result;
    }
    #endregion


    #region Private Methods
    void Notify()
    {
        if (onChange != null)
            onChange();
    }
    #endregion
}
