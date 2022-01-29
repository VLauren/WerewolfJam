using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightEnvGenerator : MonoBehaviour
{
    public Material NightMaterial;

    public Material[] NightMaterials;

    void Start()
    {
        this.enabled = false;

        GameObject nightVer = Instantiate(gameObject);

        nightVer.GetComponent<Renderer>().material = NightMaterial;
        if (NightMaterials.Length > 0)
            nightVer.GetComponent<Renderer>().materials = NightMaterials;

        nightVer.GetComponent<Collider>().enabled = false;
        nightVer.layer = 7; // Night
    }
}
