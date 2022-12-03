using System;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Nightmare"))
        {
            var nightmare = col.GetComponent<Nightmare>();
            nightmare.BounceBack();
        }
    }
}
