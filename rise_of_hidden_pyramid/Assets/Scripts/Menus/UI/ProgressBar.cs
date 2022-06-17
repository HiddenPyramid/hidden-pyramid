using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private int minimum = 0;
    [SerializeField] private int maximum = 100;
    [SerializeField] private int current = 0;


    public void UpdateCurrent(int value)
    {
        this.current = value;
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        slider.value = fillAmount;
    }
}
