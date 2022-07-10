using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    //Texture resolution - number of pixels
    public int width = 256;
    public int height = 256;
    public float scale = 20;
    Renderer rendererComponent;

    public float offsetX = 100f;
    public float offsetY = 100f;
    private void Start()
    {
        rendererComponent = GetComponent<Renderer>();
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);
    }

    private void Update()
    {
        //change texture : get mesh renderer, access material, change texture
        rendererComponent.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        //Genreate perlin noise map for texture
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xPerlinCoord = (float)x / width * scale + offsetX;
        float yPerlinCoord = (float)y / height * scale + offsetY;

        float sample =  Mathf.PerlinNoise(xPerlinCoord, yPerlinCoord);
        return new Color(sample, sample, sample);
    }
}
