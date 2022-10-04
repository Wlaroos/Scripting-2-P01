using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelperScript : MonoBehaviour
{
    public void Flip()
    {
        transform.GetChild(1).GetComponent<Shoot>().FlipEnd();
        transform.parent.GetComponent<Player>().FlipEnd();
        transform.parent.GetComponent<TankController>().FlipEnd();
    }

}
