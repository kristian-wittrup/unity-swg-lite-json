using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ProfessionData))]
public class ProfessionDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ProfessionData professionData = (ProfessionData)target;

        professionData.professionName = EditorGUILayout.TextField("Profession Name", professionData.professionName);

        if (professionData.skillGrid == null)
        {
            professionData.skillGrid = new List<List<Skill>>();
        }

        EditorGUILayout.LabelField("Skill Grid");

        for (int i = 0; i < professionData.skillGrid.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Column " + (i + 1), GUILayout.Width(100));

            if (GUILayout.Button("Add Skill", GUILayout.Width(100)))
            {
                professionData.skillGrid[i].Add(null);
                EditorUtility.SetDirty(professionData);
                AssetDatabase.SaveAssets();
            }

            if (GUILayout.Button("Remove Column", GUILayout.Width(100)))
            {
                professionData.skillGrid.RemoveAt(i);
                EditorUtility.SetDirty(professionData);
                AssetDatabase.SaveAssets();
                break;
            }

            EditorGUILayout.EndHorizontal();

            for (int j = 0; j < professionData.skillGrid[i].Count; j++)
            {
                professionData.skillGrid[i][j] = (Skill)EditorGUILayout.ObjectField(professionData.skillGrid[i][j], typeof(Skill), false);

                if (GUILayout.Button("Remove Skill", GUILayout.Width(100)))
                {
                    professionData.skillGrid[i].RemoveAt(j);
                    EditorUtility.SetDirty(professionData);
                    AssetDatabase.SaveAssets();
                    break;
                }
            }
        }

        if (GUILayout.Button("Add Column"))
        {
            professionData.skillGrid.Add(new List<Skill>());
            EditorUtility.SetDirty(professionData);
            AssetDatabase.SaveAssets();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(professionData);
            AssetDatabase.SaveAssets();
        }
    }
}