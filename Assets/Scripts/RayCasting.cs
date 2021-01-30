using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    void Update()
    {
        RaycastHit2D hit;
        float distan = 10;
        hit = Physics2D.Raycast(this.transform.position, this.transform.forward, distan);
        Debug.DrawRay(this.transform.position, this.transform.forward * distan, Color.blue);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            Debug.Log(hit.collider.transform.position);
        }
    }
}
