using TMPro;
using UnityEditor;
using UnityEngine;

public class SwapTextMeshPro : Editor
{
    /*
    [MenuItem("CONTEXT/TextMeshProUGUI/Swap TextMeshPro To Locale")]
    private static void SwapTextMeshProToLocale(MenuCommand menuCommand)
    {
        TextMeshProUGUI textMeshProUGUI = menuCommand.context as TextMeshProUGUI;

        if (textMeshProUGUI != null)
        {
            GameObject selectedObject = textMeshProUGUI.gameObject;

            // Serialize properties to JSON
            string serializedProperties = JsonUtility.ToJson(textMeshProUGUI);

            // Destroy the original TextMeshProUGUI component
            Undo.DestroyObjectImmediate(textMeshProUGUI);

            // Add a TextMeshProLocale component and deserialize properties
            TextMeshProLocale newTMPLocale = selectedObject.AddComponent<TextMeshProLocale>();
            JsonUtility.FromJsonOverwrite(serializedProperties, newTMPLocale);
        }
    }
  

    [MenuItem("CONTEXT/TextMeshProUGUI/Swap TextMeshPro To Locale")]
    private static void SwapTextMeshProToLocale(MenuCommand menuCommand)
    {
        TextMeshProUGUI textMeshProUGUI = menuCommand.context as TextMeshProUGUI;

        if (textMeshProUGUI != null)
        {
            SwapTextMeshProInChildren(textMeshProUGUI.transform);
        }
    }
      */

    [MenuItem("GameObject/Swap All TextMeshPro To Locale", false, 21)]
    private static void SwapAllTextMeshProToLocale(MenuCommand menuCommand)
    {
        GameObject parentTransform = menuCommand.context as GameObject;

        if (parentTransform != null)
        {
            SwapTextMeshProInChildren(parentTransform);
        }
    }

    private static void SwapTextMeshProInChildren(GameObject parentTransform)
    {
        TextMeshProUGUI[] textMeshProUGUIs = parentTransform.GetComponentsInChildren<TextMeshProUGUI>(true);

        foreach (var textMeshProUGUI in textMeshProUGUIs)
        {
            GameObject selectedObject = textMeshProUGUI.gameObject;

            // Serialize properties to JSON
            string serializedProperties = JsonUtility.ToJson(textMeshProUGUI);

            // Destroy the original TextMeshProUGUI component
            Undo.DestroyObjectImmediate(textMeshProUGUI);

            // Add a TextMeshProLocale component and deserialize properties
            TextMeshProLocale newTMPLocale = selectedObject.AddComponent<TextMeshProLocale>();
            JsonUtility.FromJsonOverwrite(serializedProperties, newTMPLocale);
        }
    }
}
