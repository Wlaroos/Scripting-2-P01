using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _laserParticles;
    [SerializeField] AudioClip _laserSound;

    bool once = false;

    /*private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlayerImpact(player);
            ImpactFeedback();
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                PlayerImpact(player);
                ImpactFeedback();
            }
    }

    protected virtual void PlayerImpact(Player player)
    {
        player.DecreaseHealth(_damageAmount, false);
    }

    public void ImpactFeedback()
    {
/*      //particles
        if (_laserParticles != null)
        {
            _laserParticles = Instantiate(_laserParticles, transform.position, Quaternion.identity);
        }
        //audio -- Maybe add object pooling for performance
        if (_laserSound != null)
        {
            AudioHelper.PlayClip2D(_laserSound, 1f);
        }
*/
    }
}
