using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System;

public class ShowKeyBoard : MonoBehaviour
{
    private TMP_InputField inputField;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        if (inputField != null)
        {
            inputField.onSelect.AddListener(OpenKeyBoard);
        }
        else
        {
            Debug.LogError("TMP_InputField component not found on the GameObject.");
        }
    }

    private void OpenKeyBoard(string arg0)
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
    }
}
