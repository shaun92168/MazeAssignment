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

    public AudioSource dayMusic;
    public AudioSource nightMusic;
    public bool musicPlaying = false;

    public GameObject player;
    public GameObject enemy;
    public float distanceFromEnemy;

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
                if (musicPlaying)
                {
                    nightMusic.Play();
                    dayMusic.Pause();
                }
            }
            else
            {
                morning();
                if (musicPlaying)
                {
                    nightMusic.Pause();
                    dayMusic.Play(); 
                }
            }
            isMorning = !isMorning;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hasFog)
            {
                Fog.SetActive(false);
                nightMusic.volume = 1.0f;
                dayMusic.volume = 1.0f;
            }
            else
            {
                Fog.SetActive(true);
                nightMusic.volume = 0.5f;
                dayMusic.volume = 0.5f;
            }
            hasFog = !hasFog;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (musicPlaying)
            {
                musicPlaying = false;
                dayMusic.Pause();
                nightMusic.Pause();
            }
            else
            {
                musicPlaying = true;
                if (isMorning)
                {
                    dayMusic.Play();
                    nightMusic.Pause();
                }
                else if (!isMorning)
                {
                    nightMusic.Play();
                    dayMusic.Pause();
                }
            }
        }

        if (GameObject.FindWithTag("Enemy") != null)
        {
            enemy = GameObject.FindWithTag("Enemy");
            distanceFromEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (!hasFog)
            {
                musicModulate(); 
            }
            else if (hasFog)
            {
                musicModulateFog();
            }
        }
        else if (GameObject.FindWithTag("Enemy") == null)
        {
            enemy = null;
            distanceFromEnemy = 100;
            if (!hasFog)
            {
                musicModulate();
            }
            else if (hasFog)
            {
                musicModulateFog();
            }
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

    private void musicModulate()
    {
        if (distanceFromEnemy > 11)
        {
            nightMusic.volume = 0.1f;
            dayMusic.volume = 0.1f;
        }
        else if (distanceFromEnemy >= 10)
        {
            nightMusic.volume = 0.2f;
            dayMusic.volume = 0.2f;
        }
        else if (distanceFromEnemy >= 9)
        {
            nightMusic.volume = 0.3f;
            dayMusic.volume = 0.3f;
        }
        else if (distanceFromEnemy >= 8)
        {
            nightMusic.volume = 0.4f;
            dayMusic.volume = 0.4f;
        }
        else if (distanceFromEnemy >= 7)
        {
            nightMusic.volume = 0.5f;
            dayMusic.volume = 0.5f;
        }
        else if (distanceFromEnemy >= 6)
        {
            nightMusic.volume = 0.6f;
            dayMusic.volume = 0.6f;
        }
        else if (distanceFromEnemy >= 5)
        {
            nightMusic.volume = 0.7f;
            dayMusic.volume = 0.7f;
        }
        else if (distanceFromEnemy >= 4)
        {
            nightMusic.volume = 0.8f;
            dayMusic.volume = 0.8f;
        }
        else if (distanceFromEnemy >= 3)
        {
            nightMusic.volume = 0.9f;
            dayMusic.volume = 0.9f;
        }
        else if (distanceFromEnemy >= 2)
        {
            nightMusic.volume = 1.0f;
            dayMusic.volume = 1.0f;
        }
    }

    private void musicModulateFog()
    {
        if (distanceFromEnemy > 11)
        {
            nightMusic.volume = 0.05f;
            dayMusic.volume = 0.05f;
        }
        else if (distanceFromEnemy >= 10)
        {
            nightMusic.volume = 0.1f;
            dayMusic.volume = 0.1f;
        }
        else if (distanceFromEnemy >= 9)
        {
            nightMusic.volume = 0.15f;
            dayMusic.volume = 0.15f;
        }
        else if (distanceFromEnemy >= 8)
        {
            nightMusic.volume = 0.2f;
            dayMusic.volume = 0.2f;
        }
        else if (distanceFromEnemy >= 7)
        {
            nightMusic.volume = 0.25f;
            dayMusic.volume = 0.25f;
        }
        else if (distanceFromEnemy >= 6)
        {
            nightMusic.volume = 0.3f;
            dayMusic.volume = 0.3f;
        }
        else if (distanceFromEnemy >= 5)
        {
            nightMusic.volume = 0.35f;
            dayMusic.volume = 0.35f;
        }
        else if (distanceFromEnemy >= 4)
        {
            nightMusic.volume = 0.4f;
            dayMusic.volume = 0.4f;
        }
        else if (distanceFromEnemy >= 3)
        {
            nightMusic.volume = 0.45f;
            dayMusic.volume = 0.45f;
        }
        else if (distanceFromEnemy >= 2)
        {
            nightMusic.volume = 0.5f;
            dayMusic.volume = 0.5f;
        }
    }
}
