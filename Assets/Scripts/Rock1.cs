using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock1 : MonoBehaviour
{
    Rigidbody2D rb;

    public float FallSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * -1 * FallSpeed;

        Invoke("Destroy", 10f);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

}
