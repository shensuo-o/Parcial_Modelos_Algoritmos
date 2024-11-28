using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void OnButtonPress(string sceneName)
    {
        if(GameManager.instance != null)
        {
            Debug.Log("Chage Scene");
            GameManager.instance.ChangeScene(sceneName);
        }
    }
}
