using UnityEngine;
using UnityEditor;

public class DashEditor : EditorWindow
{
    float zValue;
    float xValue;
    int count = 5;
    GameObject parent;

    [MenuItem("Window/Dash")]
    public static void ShowWindow()
    {
        GetWindow<DashEditor>("Dash Editor");
    }
    private void OnGUI()
    {
        GUILayout.Label("Create From Selected Object", EditorStyles.boldLabel);
        GUILayout.Label("X Value", EditorStyles.boldLabel);
        xValue = EditorGUILayout.FloatField(xValue);
        GUILayout.Label("Z Value", EditorStyles.boldLabel);
        zValue = EditorGUILayout.FloatField(zValue);
        GUILayout.Label("Count", EditorStyles.boldLabel);
        count = EditorGUILayout.IntField(count);
        parent = GameObject.FindGameObjectWithTag("Parent");

        if (GUILayout.Button("Create"))
        {
            foreach(GameObject obj in Selection.gameObjects)
            {
                for(int i = 0; i < count; i++)
                {
                    GameObject go = Instantiate(obj);
                    Vector3 goPos = obj.transform.position;
                    goPos.x += xValue * (i + 1);
                    goPos.z += zValue * (i + 1);
                    go.transform.position = goPos;
                    go.transform.SetParent(parent.transform);
                }
            }
        }

    }
}
