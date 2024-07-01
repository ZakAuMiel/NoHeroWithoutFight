using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnTrigger : MonoBehaviour
{
    public UnityEditor.SceneAsset sceneToLoad; // Variable pour stocker la référence à la scène à charger

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre en collision avec le trigger
        if (other.CompareTag("Player"))
        {
            // Vérifie que la scène à charger est valide
            if (sceneToLoad != null)
            {
                // Charge la scène spécifiée
                SceneManager.LoadScene(sceneToLoad.name);
            }
            else
            {
                Debug.LogError("Scene to load is not assigned.");
            }
        }
    }
}
