using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    // Static instance af clasanum sem leyfir öðrum scriptum að komast í hana.
    public static UIHealthBar instance { get; private set; }

    // setur inn maskið sem er notað til að minnka health teljaran
    public Image mask;
    
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // upprunaleg stærð sett sem núverandi stærð fyrst þegar scriptan er keyrð
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        // minnkar teljaran
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
