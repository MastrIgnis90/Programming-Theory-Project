using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesDatabase", menuName = "Tutorial/Resources Database")]
public class ButtonDictionary : ScriptableObject
{
    public Dictionary<string, Sprite> dictionary = new();
    [SerializeField] private List<Sprite> test = new();

    public void Init()
    {
        foreach(Sprite i in test)
        {
            dictionary.Add(i.name, i);
        }
    }

    public Sprite GetItem(string id)
    {
        dictionary.TryGetValue(id, out Sprite value);
        return value;
    }
}
