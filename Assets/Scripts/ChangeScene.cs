using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneID; // Utiliser l'ID de la scène


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneID >= 0 && sceneID < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(sceneID);
            }
            else
            {
              Application.Quit();
            }
        }
    }
}
