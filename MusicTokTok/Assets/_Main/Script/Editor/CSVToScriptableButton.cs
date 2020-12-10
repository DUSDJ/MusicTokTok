using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;

[CustomEditor(typeof(GameManager))]
public class CSVToScriptableButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate Stage By CSV"))
        {
            GameManager gm = (GameManager)target;
            ScriptableStage ss = gm.Stage;

            List<Dictionary<string, object>> dic = CSVReader.Read("CSVData/" + ss.name);
            List<NoteData> noteDataList = new List<NoteData>();

            // noteData List 생성
            for (int i = 0; i < dic.Count; i++)
            {
                Debug.Log(dic[i]["ArrivalTime"].ToString());
                Debug.Log(dic[i]["LineNumber"].ToString());

                float arrival = float.Parse(dic[i]["ArrivalTime"].ToString());
                int lineNumber = int.Parse(dic[i]["LineNumber"].ToString());

                NoteData nd = new NoteData(lineNumber, arrival);
                noteDataList.Add(nd);
            }

            // noteDataList 정렬
            NoteData[] nArray = noteDataList.ToArray();
            Array.Sort(nArray, delegate (NoteData a, NoteData b) {
                return a.ArrivalTime.CompareTo(b.ArrivalTime);
            });
            noteDataList = nArray.ToList();

            ss.NoteList = noteDataList;
        }
        
    }
}
