using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    // This allows us to access this script from any other script without needing a reference (Unity Learn).
    public static UIHealthBar instance { get; private set; }

    // A public Image object that holds a mask.
    // Masking allows images to be hidden behind other images (Unity Learn).
    public Image mask;
    float originalSize;

    // Awake is called when an object is created (Unity Learn).
    private void Awake()
    {
        // This is a special keyword that means "the object that currently runs the function" (Unity Learn).
        instance = this;
    }

    void Start()
    {
        // Sets original size to the sie of the mask in the editor.
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        // This sets the size of the mask using anchors as reference (Unity Learn).
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
