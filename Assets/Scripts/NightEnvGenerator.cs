using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightEnvGenerator : MonoBehaviour
{
    public Material NightMaterial;

    void Start()
    {
        this.enabled = false;

        GameObject nightVer = Instantiate(gameObject);

        nightVer.GetComponent<Renderer>().material = NightMaterial;
        nightVer.GetComponent<Collider>().enabled = false;
        nightVer.layer = 7; // Night
    }
}
