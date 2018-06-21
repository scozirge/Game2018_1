using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ParticleManager))]
public class ParticleManager_Editor : Editor
{
    override public void OnInspectorGUI()
    {
        ParticleManager pm = target as ParticleManager;

        pm.Loop = GUILayout.Toggle(pm.Loop, "Loog");

        if (!pm.Loop)
        {
            pm.LifeTime = EditorGUILayout.FloatField("LifeTime", pm.LifeTime);
        }

    }
}
