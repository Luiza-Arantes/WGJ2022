using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] Backpack backpack = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            backpack.UpdateItem(1);
            //Destroy(collision.gameobject);
        }
    }
}
