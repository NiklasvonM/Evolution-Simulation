using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ReplaceWithPrefab : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;

    [MenuItem("GameObject/Replace With Prefab", false, 0)]
    static void Replace()
    {
        var replaceWithPrefab = CreateInstance<ReplaceWithPrefab>();
        replaceWithPrefab.Show();
    }

    void Show()
    {
        var replaceWithPrefab = this;
        EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "", EditorGUIUtility.GetControlID(FocusType.Passive));
    }

    void OnGUI()
    {
        string commandName = Event.current.commandName;
        if (commandName == "ObjectSelectorUpdated")
        {
            prefab = EditorGUIUtility.GetObjectPickerObject() as GameObject;
        }
        else if (commandName == "ObjectSelectorClosed")
        {
            foreach (var gameObject in Selection.gameObjects)
            {
                if (prefab != null)
                {
                    GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    newObject.transform.SetParent(gameObject.transform.parent, true);
                    newObject.transform.localPosition = gameObject.transform.localPosition;
                    newObject.transform.localRotation = gameObject.transform.localRotation;
                    newObject.transform.localScale = gameObject.transform.localScale;
                    Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefab");
                    Undo.DestroyObjectImmediate(gameObject);
                }
            }
        }
    }
}
#endif
