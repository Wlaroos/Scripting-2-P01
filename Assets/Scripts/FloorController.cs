using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{

    [SerializeField] GameObject[] floorTiles;
    [SerializeField] GameObject respawnPoint;

    bool allowed = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(allowed) StartCoroutine(FallOrder(2, 4, 5, 6));
        }
    }

    IEnumerator FallOrder(int respawnNum, int num1, int num2, int num3)
    {
        allowed = false;

        MoveRespawn(respawnNum);

        Fall(num1);

        yield return new WaitForSeconds(.25f);
        Fall(num2);

        yield return new WaitForSeconds(.25f);
        Fall(num3);

        yield return new WaitForSeconds(10f);
        allowed = true;
        MoveRespawn(5);
    }

    public void Fall(int index)
    {
        floorTiles[index -1].GetComponent<FloorFall>().StartFall();
    }

    public void MoveRespawn(int index)
    {
        respawnPoint.transform.position = floorTiles[index -1].transform.position + new Vector3(0,2.5f,0);
    }    

}
