using UnityEngine;

public class ColorTransition : MonoBehaviour
{
    public Material material;
    public Color[] colors;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint;
    public float time;

    private void Start()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -1 * normals[i];
        mesh.normals = normals;
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] tris = mesh.GetTriangles(i);
            for (int j = 0; j < tris.Length; j += 3)
            {
                //swap order of tri vertices
                int temp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = temp;
            }
            mesh.SetTriangles(tris, i);
        }
    }
    void Update()
    {
        Transition();
    }

    void Transition()
    {

        targetPoint += Time.deltaTime / time;
        material.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);
        if (targetPoint >= 1f)
        {
            targetPoint = 0f;
            currentColorIndex = targetColorIndex;
            targetColorIndex++;
            if (targetColorIndex == colors.Length)
                targetColorIndex = 0;
        }

    }
}
