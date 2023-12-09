
using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs : MonoBehaviour
{

    [MenuItem("Window/Delete PlayerPrefs (All)")]
    static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
