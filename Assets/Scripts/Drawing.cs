using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    private readonly Color c_color = new Color(0, 0, 0, 0);
    public Texture2D m_texture;
    public Material m_material;
    public int textureWidth, textureHeight;
    //public int newX, newY;
    public float alpha;
    public Color color, targetColor, existingColor;
    bool isEnabled;
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        m_material = renderer.material;
        if (null == renderer)
            return;

        if (null != m_material)
        {
            m_texture = new Texture2D(textureWidth, textureHeight);
            for (int i = 0; i < textureWidth; i++)
            {
                for (int j = 0; j < textureHeight; j++)
                {
                    m_texture.SetPixel(i, j, color);
                }
            } 

            m_texture.Apply();

            m_material.SetTexture("_DrawingTex", m_texture);
            isEnabled = true;
        }

    }

    public void PaintOn(Vector2 textureCoord, Texture2D splashTexture)
    {
        if (isEnabled)
        {
            int x = (int)(textureCoord.x * textureWidth) - (splashTexture.width / 2);
            int y = (int)(textureCoord.y * textureHeight) - (splashTexture.height / 2);
          
            for (int i = 0; i < splashTexture.width; i++)
                for (int j = 0; j < splashTexture.height; j++)
                {
                    existingColor = m_texture.GetPixel(x+i,y+j);
                    alpha = splashTexture.GetPixel(i, j).a;
                    Color result = Color.Lerp(existingColor, targetColor, alpha);
                    result.a = existingColor.a + alpha;
                    m_texture.SetPixel(x + i, y +j, result);
                }
        }

        m_texture.Apply();
    }

}
