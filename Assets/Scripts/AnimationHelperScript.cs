using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelperScript : MonoBehaviour
{
    public void Flip()
    {
        Invoke(nameof(FlipEnd), 1f);
    }

    private void FlipEnd()
    {
        transform.parent.GetComponent<Player>().FlipEnd();
        transform.parent.GetComponent<TankController>().FlipEnd();
    }
}
