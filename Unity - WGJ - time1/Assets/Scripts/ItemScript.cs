using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public IemImagem Im;

    public void OnTriggerEnter2D(Collider2D other) // Nao esta funcionando
    {
        if (other.gameObject.tag == "Player")
        { 
            gameObject.SetActive(false);
            Im.Imagem();
        }
    }
}
