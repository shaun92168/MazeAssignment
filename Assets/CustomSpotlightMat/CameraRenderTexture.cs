using UnityEngine;

[ExecuteInEditMode]
public class CameraRenderTexture : MonoBehaviour
{
    public Material Mat1;
    public Material Mat2;
    public Shader shader1;
    public Shader shader2;
    public bool toggled = false;

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (toggled)
        {
            Graphics.Blit(source, destination, Mat1);
        }
        if (!toggled)
        {
            Graphics.Blit(source, destination, Mat2);
        }
    }

    private void Awake()
    {
        shader1 = Shader.Find("Custom/Spotlight");
        shader2 = Shader.Find("Custom/Lighting/BasicLightingPerVertex");
    }

    private void Update()
    {
        // Open Ligher shader
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("O" + Mat1.shader);
            Debug.Log("OPressed" + Mat2.shader);
            toggled = !toggled;
            // Doesn't work. Can't assign shader 2 to the current material
            Mat1.shader = shader1;
            Mat2.shader = shader2;
        }

    }
}
