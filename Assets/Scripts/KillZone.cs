using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    [SerializeField] Transform _respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.DecreaseHealth(1, true);
            player.transform.position = _respawnPoint.position;
        }
    }

}
