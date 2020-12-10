using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {        
            UIManager.Instance.LineEffect(0);

            GameManager.Instance.CheckNote(0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.LineEffect(1);

            GameManager.Instance.CheckNote(1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            UIManager.Instance.LineEffect(2);

            GameManager.Instance.CheckNote(2);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            UIManager.Instance.LineEffect(3);

            GameManager.Instance.CheckNote(3);
        }
    }
}
