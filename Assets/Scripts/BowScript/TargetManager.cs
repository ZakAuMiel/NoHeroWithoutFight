using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject teleporter; // Le téléporteur à activer

    public List<MovingTarget> targets; // Liste publique de cibles

    private void Start()
    {
        // Assurez-vous que le téléporteur est désactivé au début
        teleporter.SetActive(false);
    }

    public void CheckTargets()
    {
        // Vérifiez si toutes les cibles sont touchées
        foreach (MovingTarget target in targets)
        {
            if(target.stopped == false)
            {
                return;
            }
        }

        // Si toutes les cibles sont touchées, activez le téléporteur
        teleporter.SetActive(true);
    }
}
