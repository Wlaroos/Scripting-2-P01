using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : CollectibleBase
{

    [SerializeField] float _speedMult = 1.5f;

    protected override void Collect(Player player)
    {
        // pull motor controller from player
        TankController controller = player.GetComponent<TankController>();
        if(controller != null)
        {
            controller.MoveSpeed *= _speedMult;
        }
    }

    protected override void Movement(Rigidbody rb)
    {
        //rotation
        Quaternion turnOffset = Quaternion.Euler(MovementSpeed, MovementSpeed, MovementSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

}
