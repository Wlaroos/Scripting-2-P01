using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFall : MonoBehaviour
{

    private Rigidbody _rb;
    private SpriteRenderer _sr;
    private Vector3 _pos;

    private Color flashColor = Color.red;
    private float warningTime = 3f;
    private float flashTime = .5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _pos = transform.position;
    }

    private void Start()
    {
        //StartFall();
    }

    public void StartFall()
    {
        StartCoroutine("FlashInput", _sr);
        Invoke(nameof(Reset), warningTime * 3);
    }

    public void Reset()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;
        transform.position = _pos - new Vector3(0,8,0);
        transform.rotation = Quaternion.identity;
        StartCoroutine("MoveUp");
    }

    IEnumerator MoveUp()
    {
        while (transform.position.y <= _pos.y)
        {
            transform.position += new Vector3(0, 0.025f, 0);
            yield return new WaitForEndOfFrame();
        }
    }

        IEnumerator FlashInput(SpriteRenderer sr)
    {
        // Save the default color
        Color defaultColor = sr.color;
        float elaspedTime = 0f;
        float flashElaspedTime = 0f;
        bool flip = false;

        while (elaspedTime < warningTime)
        {
            elaspedTime += Time.deltaTime;

            if(flashElaspedTime < flashTime && flip == false)
            {
                flashElaspedTime += Time.deltaTime;

                sr.color = Color.Lerp(defaultColor, flashColor, flashElaspedTime / flashTime);
            }
            else if(flashElaspedTime < flashTime && flip == true)
            {
                flashElaspedTime += Time.deltaTime;

                sr.color = Color.Lerp(flashColor, defaultColor, flashElaspedTime / flashTime);
            }

            yield return null;

            if (sr.color == defaultColor)
            {
                flashElaspedTime = 0;
                flip = false;
            }
            else if (sr.color == flashColor)
            {
                flashElaspedTime = 0;
                flip = true;
            }
        }
        _sr.color = defaultColor;
        _rb.useGravity = true;
        _rb.isKinematic = false;
        yield return new WaitForSeconds(.75f);
        _rb.AddTorque(new Vector3(Random.Range(-500,500), Random.Range(-500, 500), Random.Range(-500, 500)));
    }

}