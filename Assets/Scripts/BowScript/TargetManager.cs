using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject teleporter; // Le téléporteur à activer

    private List<MovingTarget> targets;

    private void Start()
    {
        // Trouvez toutes les cibles dans la scène
        targets = new List<MovingTarget>(FindObjectsOfType<MovingTarget>());

        // Assurez-vous que le téléporteur est désactivé au début
        teleporter.SetActive(false);
    }

    public void CheckTargets()
    {
        // Vérifiez si toutes les cibles sont touchées
        foreach (MovingTarget target in targets)
        {
        
        }

        // Si toutes les cibles sont touchées, activez le téléporteur
        teleporter.SetActive(true);
    }
}
