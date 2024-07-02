using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject midPointVisual, arrowPrefab, arrowSpawnPoint;

    [SerializeField]
    private float arrowMaxSpeed = 10;

    public void PrepareArrow()
    {
        midPointVisual.SetActive(true);
    }

    public void ReleaseArrow(float strength)
    {
        midPointVisual.SetActive(false);

        // Validation de la force
        if (float.IsNaN(strength) || float.IsInfinity(strength) || strength < 0)
        {
            Debug.LogError("Invalid strength value: " + strength);
            return;
        }

        Vector3 direction = midPointVisual.transform.forward;
        if (direction == Vector3.zero || float.IsNaN(direction.x) || float.IsNaN(direction.y) || float.IsNaN(direction.z) ||
            float.IsInfinity(direction.x) || float.IsInfinity(direction.y) || float.IsInfinity(direction.z))
        {
            Debug.LogError("Invalid direction vector: " + direction);
            return;
        }

        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = arrowSpawnPoint.transform.position;
        arrow.transform.rotation = midPointVisual.transform.rotation;

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = direction * strength * arrowMaxSpeed; // Initialiser la vitesse de la flèche

        rb.AddForce(direction * strength * arrowMaxSpeed, ForceMode.Impulse);
    }

}