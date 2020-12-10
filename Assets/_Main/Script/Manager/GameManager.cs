using Maeul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region SingleTon
	/* SingleTon */
	private static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
				if (!instance)
				{
					Debug.LogError("GameManager Not Exsist In This Scene");
				}
			}

			return instance;
		}
	}

	#endregion


	#region Player Data

	public int TotalHP;

	[SerializeField]
	private int hp;
	public int HP
	{
		get
		{
			return hp;
		}
		set
		{
			if(value <= 0)
			{
				hp = 0;

				UIManager.Instance.UIUpdateLifeBar((float)hp / TotalHP);
				Dead();

				return;
			}

			hp = value;
			UIManager.Instance.UIUpdateLifeBar((float)hp / TotalHP);
		}
	}

	#endregion


	#region Values

	public ScriptableStage Stage;

	public float NoteArriveTime = 3.0f;

	public List<Note>[] ListNoteList = new List<Note>[4];

	#endregion

	private void Awake()
	{
		#region SingleTone

		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(this);
		}

		#endregion


		Init();
	}
	
	public void Init()
	{
		hp = TotalHP;

		UIManager.Instance.Init();
		AudioManager.Instance.Init();

		for (int i = 0; i < ListNoteList.Length; i++)
		{
			ListNoteList[i] = new List<Note>();
		}

		StartStage();
	}


	#region RythmGame

	public void StartStage()
	{
		StartCoroutine(GenerateNote(Stage.NoteList));
	}

	IEnumerator GenerateNote(List<NoteData> noteDatas)
	{
		int index = 0;
		float GenTimeNow = 0;

		AudioManager.Instance.StartBGM();

		while (index < noteDatas.Count)
		{
			NoteData nd = noteDatas[index];

			if(nd.ArrivalTime - NoteArriveTime <= GenTimeNow)
			{
				UIManager.Instance.SetNote(index, nd.LineNumber);
				index += 1;

				continue;
			}

			GenTimeNow += Time.deltaTime;

			yield return null;
		}

	}

	

	public void CheckNote(int lineNumber)
	{
		if(ListNoteList[lineNumber].Count == 0)
		{
			return;
		}

		Note n = ListNoteList[lineNumber][0];

		n.CheckNote();
	}

	#endregion






	public void Dead()
	{
		SceneManager.LoadScene(0);
	}
}
