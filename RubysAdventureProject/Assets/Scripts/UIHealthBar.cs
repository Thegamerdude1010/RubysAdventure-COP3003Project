using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    // This allows us to access this script from any other script without needing a reference (Unity Learn Tutorial)
    public static UIHealthBar instance { get; private set; }

    public Image mask;
    float originalSize;

    private void Awake()
    {
        // this is a special keyword that means "the object that currently runs the function" (Unity Learn Tutorial)
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
