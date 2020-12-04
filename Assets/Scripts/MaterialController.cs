using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public GameObject Quad;
    public GameObject Quad1;
    public GameObject Quad2;
    public GameObject Quad3;
    public GameObject Quad4;
    public GameObject Floor;
    public GameObject Gun;
    public GameObject Ball;
    public GameObject Fog;
    public Material MorningSkybox;
    public Material NightSkybox;

    private bool isMorning;
    private bool hasFog;

    // Start is called before the first frame update
    void Start()
    {
        morning();
        isMorning = true;
        Fog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isMorning)
            {
                night();
            }
            else
            {
                morning();
            }
            isMorning = !isMorning;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hasFog)
            {
                Fog.SetActive(false);
            }
            else
            {
                Fog.SetActive(true);
            }
            hasFog = !hasFog;
        }
    }

    private void night()
    {
        Quad.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.6f);
        Quad1.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.6f);
        Quad2.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.6f);
        Quad3.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.6f);
        Quad4.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.6f);
        Floor.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.6f);
        Gun.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.6f);
        Ball.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.6f);
        Fog.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.7f);
        RenderSettings.skybox = NightSkybox;
    }

    private void morning()
    {
        Quad.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        Quad1.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        Quad2.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        Quad3.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        Quad4.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        Floor.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        Gun.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        Ball.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        Fog.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 1f);
        RenderSettings.skybox = MorningSkybox;
    }
}
