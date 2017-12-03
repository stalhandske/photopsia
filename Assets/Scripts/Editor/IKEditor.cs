using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(IK))]
public class IKEditor : Editor 
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        IK myScript = (IK)target;

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();

        if (GUILayout.Button(myScript.invert ? "Inverted" : "Not Inveted"))
            myScript.invert = !myScript.invert;

        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(myScript.toggleIK?"IK ON": "IK OFF"))
            myScript.toggleIK = !myScript.toggleIK;

        if (GUILayout.Button("Update Bones"))
            myScript.UpdateBones();

        EditorGUILayout.EndHorizontal();
    }

}
#endif