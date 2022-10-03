using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSM : StateMachineMB
{
  
    bool direction = false;
    int stateRepeats = 0;
    List<Vector3> _pos = new List<Vector3>();
    List<Quaternion> _rot = new List<Quaternion>();
    float moveTime = 2.75f;

    private void Awake()
    {
        _pos.Add(new Vector3(-22f, 1.15f, -14f));
        _pos.Add(new Vector3(-20f, 1.15f, 20f));
        _pos.Add(new Vector3(20f, 1.15f, 20f));
        _pos.Add(new Vector3(22f, 1.15f, -14f));
        _rot.Add(Quaternion.Euler(90, -90, 0));
        _rot.Add(Quaternion.Euler(90, -45, 0));
        _rot.Add(Quaternion.Euler(90, 45, 0));
        _rot.Add(Quaternion.Euler(90, 90, 0));

        StartCoroutine(State1(moveTime));
    }

    private IEnumerator State1(float time)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = new Vector3(14f, 1.15f, 22f);
        Quaternion startingRot = transform.rotation;
        Quaternion finalRot = Quaternion.Euler(90,0,0);

        if (direction == true)
        {
            finalPos.x *= -1;
        }

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            transform.rotation = Quaternion.Lerp(startingRot, finalRot, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        direction = !direction;
        stateRepeats++;
        if (stateRepeats < 4)
        {
            StartCoroutine(State1(moveTime));
        }
        else
        {
            stateRepeats = 0;
            direction = false;
            StartCoroutine(State2(moveTime));
        }

    }

    private IEnumerator State2(float time)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = _pos[stateRepeats];
        Quaternion startingRot = transform.rotation;
        Quaternion finalRot = _rot[stateRepeats];

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            transform.rotation = Quaternion.Lerp(startingRot, finalRot, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if(direction == false)
        {
            stateRepeats++;
        }
        else
        {
            stateRepeats--;
        }

        if (stateRepeats < 4 && direction == false)
        {
            StartCoroutine(State2(moveTime));
        }
        else if (stateRepeats >= 4 && direction == false)
        {
            stateRepeats = 2;
            direction = true;
            StartCoroutine(State2(moveTime));
        }
        else if (stateRepeats >= 0 && direction == true)
        {
            StartCoroutine(State2(moveTime));
        }
        else if (stateRepeats < 0 && direction == true)
        {
            stateRepeats = -1;
            StartCoroutine(State1(moveTime));
        }


    }

}
