using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fill;

    public static HealthBarUI Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateFill(int curHp, int maxHp)
    {
        fill.fillAmount = (float)curHp / (float)maxHp;
    }
}
