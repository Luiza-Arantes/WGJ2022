using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Backpack", menuName = "Backpack")]
public class Backpack : ScriptableObject
{
    [SerializeField] int itens = 0;

    public int GetItem { get => itens; }
    public void UpdateItem(int numberOfItens) { itens += numberOfItens; }

    [SerializeField] List<GameObject> items = new List<GameObject>();

    public List<GameObject> GetItems { get => items; }

    public void AddItem(GameObject itemToAdd) { items.Add(itemToAdd); }
}

