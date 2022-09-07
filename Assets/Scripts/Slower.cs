using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{

    [SerializeField] float _slowAmount = 0.05f;
    bool _timer = false;

    protected override void PlayerImpact(Player player)
    {
        TankController controller = player.GetComponent<TankController>();
        if (controller != null && _timer == false)
        {
            controller.MoveSpeed -= _slowAmount;
            _timer = true;
            Debug.Log("Speed: " + controller.MoveSpeed);
            StartCoroutine(SlowReset(controller));
        }
    }

    IEnumerator SlowReset(TankController controller)
    {
        yield return new WaitForSeconds(5f);
        controller.MoveSpeed += _slowAmount;
        _timer = false;
        Debug.Log("Speed: " + controller.MoveSpeed);
    }

}
