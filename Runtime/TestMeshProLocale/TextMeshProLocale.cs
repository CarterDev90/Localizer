using System;
using TMPro;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

public class TextMeshProLocale : TextMeshProUGUI
{
    private string originalText;

    protected override void Awake()
    {     
        base.Awake();
        originalText = base.text;

        if (!Application.isPlaying)
            return;

        this.Localize();
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += OnPlayModeStateChange1d;
#endif
    }

    [ContextMenu("Swap Components")]
    private void SwapComponentsContextMenu()
    {
       // SwapComponents(this);
    }


#if UNITY_EDITOR

    private void OnPlayModeStateChange1d(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {

            ResetText();
        }
    }

    private void ResetText()
    {
        base.text = originalText;
    }

#endif
}

