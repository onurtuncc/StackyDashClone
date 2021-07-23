using UnityEngine;
using UnityEditor;

public class PlatformEditor : EditorWindow
{
    //Down Layer Size Values
    float zValue;
    float xValue;
    float yValue;
    GameObject downLayer;
    GameObject upLayer;
    GameObject zParent;
    GameObject platformParent;
    string downLayerName = "DownPlatform";
    string upLayerName = "UpCube";
    //Upper Layer Size Values
    float upX = 1f;
    float upY = .5f;
    float upZ = 1f;
    Color downColor;
    Color upColor;

    [MenuItem("Window/Platform")]
    public static void ShowWindow()
    {
        GetWindow<PlatformEditor>("Platform Editor").minSize = new Vector2(500, 300);
        
    }

    private void OnGUI()
    {

        GUILayout.Label("Platform X Value", EditorStyles.boldLabel);
        xValue = EditorGUILayout.FloatField(xValue);
        GUILayout.Label("Platform Y Value", EditorStyles.boldLabel);
        yValue = EditorGUILayout.FloatField(yValue);
        GUILayout.Label("Platform Z Value", EditorStyles.boldLabel);
        zValue = EditorGUILayout.FloatField(zValue);
        GUILayout.Label("Down Layer Color", EditorStyles.boldLabel);
        downColor = EditorGUILayout.ColorField(downColor);
        GUILayout.Label("Up Layer Color", EditorStyles.boldLabel);
        upColor = EditorGUILayout.ColorField(upColor);


        //In order to create platform we need to create primitive objects first
        if (GUILayout.Button("Create Primitive Objects"))
        {
            //Setting materials for Up Layer and Down Layer
            Material mat1 = new Material(Shader.Find("Standard"));
            Material mat2 = new Material(Shader.Find("Standard"));
            
            downLayer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            downLayer.GetComponent<Renderer>().material = mat1;
            upLayer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            upLayer.GetComponent<Renderer>().material = mat2;
            
            Vector3 upLayerScale = new Vector3(upX, upY, upZ);
            upLayer.transform.localScale = upLayerScale;
            zParent = new GameObject("zParent");
            platformParent = new GameObject("Platform Parent");
            downLayer.name = downLayerName;
            upLayer.name = upLayerName;
            upLayer.GetComponent<BoxCollider>().size = new Vector3(1f, 10, 1f);
            upLayer.GetComponent<BoxCollider>().center = new Vector3(0, 4.5f, 0);
            downLayer.transform.SetParent(platformParent.transform);
            zParent.transform.SetParent(platformParent.transform);
        }

        if (GUILayout.Button("Create Platform"))
        {


            //GameObject downGo = Instantiate(downLayer);    
            //GameObject zPar = Instantiate(zParent);
            zParent.transform.localPosition = Vector3.zero;
            Vector3 newScale = new Vector3(xValue, yValue, zValue);
            Vector3 newPosition = new Vector3(0, -(yValue / 2 + upY/2), 0);
            Vector3 upLayerPos = new Vector3(-xValue / 2 + upX/2, 0, -zValue / 2 + upZ/2);
            downLayer.transform.localScale = newScale;
            downLayer.transform.localPosition = newPosition;
            downLayer.GetComponent<Renderer>().sharedMaterial.color = downColor;
            upLayer.GetComponent<Renderer>().sharedMaterial.color = upColor;

            for (int i = 0; i < zValue/upZ; i++)
            {

                GameObject go = Instantiate(upLayer);
                go.transform.localPosition = upLayerPos;
                upLayerPos.z += upZ;
                go.transform.SetParent(zParent.transform);
            }

            DestroyImmediate(GameObject.Find(upLayerName));
            for (int i = 1; i < xValue/upX; i++)
            {
                GameObject go = Instantiate(zParent);
                go.transform.localPosition = new Vector3(i * upX, 0, 0);
                go.transform.SetParent(platformParent.transform);

            }

        }

    }
}

    



