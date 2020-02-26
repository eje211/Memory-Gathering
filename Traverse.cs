using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Traverse : MonoBehaviour
{

    public void NextStage()
    {
        Debug.Log("next stage!");
        SceneManager.LoadScene(1);
    }
}
