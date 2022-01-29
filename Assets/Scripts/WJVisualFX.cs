using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJVisualFX : MonoBehaviour
{
    public static WJVisualFX Instance { get; private set; }

    public List<GameObject> EffectsPrefabs;

    void Awake()
    {
        Instance = this;
    }


    public static void Effect(int _index, Vector3 _position, Quaternion _rotation = new Quaternion())
    {
        if (Instance.EffectsPrefabs.Count > _index)
        {
            Instantiate(Instance.EffectsPrefabs[_index], _position, _rotation);
        }
        else
            Debug.Log("No existe el efecto " + _index);
    }
}