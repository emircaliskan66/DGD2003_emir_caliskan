using UnityEngine;

public class TreePlaneFixer : MonoBehaviour
{
    [Range(0.1f, 1f)]
    public float whiteThreshold = 0.9f; // Beyazlýk eþiði (Hangi tonlar silinsin?)

    void Start()
    {
        MakeDoubleSided();
        RemoveWhiteBackground();
    }

    void MakeDoubleSided()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = mesh.uv;
        int[] triangles = mesh.triangles;
        Vector3[] normals = mesh.normals;

        int nV = vertices.Length;
        int nT = triangles.Length;

        Vector3[] newVertices = new Vector3[nV * 2];
        Vector2[] newUV = new Vector2[nV * 2];
        Vector3[] newNormals = new Vector3[nV * 2];
        int[] newTriangles = new int[nT * 2];

        for (int i = 0; i < nV; i++)
        {
            newVertices[i] = vertices[i];
            newVertices[i + nV] = vertices[i];
            newUV[i] = uv[i];
            newUV[i + nV] = uv[i];
            newNormals[i] = normals[i];
            newNormals[i + nV] = -normals[i];
        }

        for (int i = 0; i < nT; i += 3)
        {
            // Ön yüz
            newTriangles[i] = triangles[i];
            newTriangles[i + 1] = triangles[i + 1];
            newTriangles[i + 2] = triangles[i + 2];
            // Arka yüz
            newTriangles[nT + i] = triangles[i] + nV;
            newTriangles[nT + i + 1] = triangles[i + 2] + nV;
            newTriangles[nT + i + 2] = triangles[i + 1] + nV;
        }

        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.normals = newNormals;
        mesh.triangles = newTriangles;
    }

    void RemoveWhiteBackground()
    {
        Renderer renderer = GetComponent<Renderer>();
        Texture2D originalTex = (Texture2D)renderer.material.mainTexture;

        if (originalTex == null) return;

        // Yeni bir texture oluþturuyoruz (þeffaflýk destekleyen)
        Texture2D newTex = new Texture2D(originalTex.width, originalTex.height, TextureFormat.RGBA32, false);
        Color[] pixels = originalTex.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            // Eðer RGB deðerleri belirlediðimiz eþiðin üzerindeyse (yani beyazsa)
            if (pixels[i].r >= whiteThreshold && pixels[i].g >= whiteThreshold && pixels[i].b >= whiteThreshold)
            {
                pixels[i] = new Color(0, 0, 0, 0); // Tamamen þeffaf yap
            }
        }

        newTex.SetPixels(pixels);
        newTex.Apply();

        // Yeni texture'ý materyale ata
        renderer.material.mainTexture = newTex;

        // Materyali "Cutout" veya "Fade" moduna almamýz lazým ki þeffaflýk gözüksün
        SetupTransparentMaterial(renderer.material);
    }

    void SetupTransparentMaterial(Material mat)
    {
        // Standart Shader kullanýyorsan Cutout moduna alýr
        mat.SetFloat("_Mode", 1);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat.SetInt("_ZWrite", 1);
        mat.EnableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 2450;
    }
}