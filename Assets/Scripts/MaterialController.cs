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
        Quad.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0);
        Quad1.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0);
        Quad2.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.2f);
        Quad3.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.4f);
        Quad4.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.4f);
        Floor.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0f);
        Gun.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0f);
        Ball.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0f);
        RenderSettings.skybox = NightSkybox;
    }

    private void morning()
    {
        Quad.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.15f);
        Quad1.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.3f);
        Quad2.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.5f);
        Quad3.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.8f);
        Quad4.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.8f);
        Floor.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.3f);
        Gun.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.2f);
        Ball.GetComponent<Renderer>().sharedMaterial.SetFloat("_Ambient", 0.2f);
        RenderSettings.skybox = MorningSkybox;
    }
}
