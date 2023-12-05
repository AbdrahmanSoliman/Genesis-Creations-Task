using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SequencingSystem))]
public class UpdateButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SequencingSystem sequencingSystem = (SequencingSystem)target;


        GUILayout.Space(25);
        GUILayout.TextArea("==============================================\nFollowing Buttons are used to add steps in a given transform!\n==============================================");
        GUILayout.Space(10);


        GUILayout.TextArea("Adds new ones only if found in the Sequence SO");
        if(GUILayout.Button("Update New Ones Only"))
        {
            sequencingSystem.HandleUpdateNewOnesButtonPressed();
        }

        GUILayout.Space(5);

        GUILayout.TextArea("Removes all childs and add all steps in the Sequence SO");
        if(GUILayout.Button("Update All"))
        {
            sequencingSystem.HandleUpdateAllButtonPressed();
        }
    }
}
