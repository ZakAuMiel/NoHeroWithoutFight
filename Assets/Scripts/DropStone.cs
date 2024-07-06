using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject stonePrefab; // Le prefab du caillou

    [SerializeField]
    private Transform spawnPoint; // Le point de spawn du caillou

    [SerializeField]
    private InputActionReference DropStoneActionReference; // Référence à l'action d'apparition

    private void OnEnable()
    {
        // Activer l'action de spawn
        DropStoneActionReference.action.Enable();
        // S'abonner à l'événement de déclenchement de l'action
        DropStoneActionReference.action.performed += OnSpawnActionPerformed;
    }

    private void OnDisable()
    {
        // Désactiver l'action de spawn
        DropStoneActionReference.action.Disable();
        // Se désabonner de l'événement de déclenchement de l'action
         DropStoneActionReference.action.performed -= OnSpawnActionPerformed;
    }

    private void OnSpawnActionPerformed(InputAction.CallbackContext context)
    {
        // Faire apparaître le caillou à la position de spawn
        Instantiate(stonePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
