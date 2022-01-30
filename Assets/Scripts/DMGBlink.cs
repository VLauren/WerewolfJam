using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGBlink : MonoBehaviour
{
    Material[] OGMats;

    void Start()
    {
        OGMats = GetComponent<Renderer>().materials;
    }

    public void Blink()
    {
        Debug.Log("BLINK " + gameObject.name);
        StartCoroutine(DBlink());
    }

    IEnumerator DBlink()
    {
        Material[] mats = GetComponent<Renderer>().materials;
        for (int i = 0; i < mats.Length; i++)
            mats[i] = WJGame.BMat;
        GetComponent<Renderer>().materials = mats;

        yield return null;
        yield return null;

        GetComponent<Renderer>().materials = OGMats;

        yield return null;
        yield return null;

        mats = GetComponent<Renderer>().materials;
        for (int i = 0; i < mats.Length; i++)
            mats[i] = WJGame.BMat;
        GetComponent<Renderer>().materials = mats;

        yield return null;
        yield return null;

        GetComponent<Renderer>().materials = OGMats;

    }
}
