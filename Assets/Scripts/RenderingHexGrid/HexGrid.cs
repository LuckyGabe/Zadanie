using UnityEngine;

public class HexGrid : MonoBehaviour
{

    public int width = 6;
    public int height = 6;
    [SerializeField]
    private Material redMaterial, greenMaterial, yellowMaterial, greyMaterial;

    public HexCell cellPrefab;
    HexCell[] cells;
    HexMesh hexMesh;
    private int redNumb = 600000, greenNumb = 100000, yellowNumb = 50000, greyNumb = 250000;

    void Awake()
    {
        cells = new HexCell[height * width];
        hexMesh = GetComponentInChildren<HexMesh>();
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
                
            }
        }

    }
    void Start()
    {
        transform.Rotate(new Vector3(90, 0, 0));
        hexMesh.Triangulate(cells);

    }
    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.color = GetRandomColor();
       
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        
        cell.IDx = Random.Range(1, 1000);
        cell.IDy = Random.Range(1, 1000);
    }


    Color GetRandomColor()
    {
        int rand = Random.Range(1, 5);
        switch (rand)
        {
            case 1:
                if (redNumb > 0)
                {

                    redNumb--;
                    Debug.Log(rand);
                    return Color.red;
                }
                else { return GetRandomColor(); }
            case 2:
                if (greenNumb > 0)
                {
                    Debug.Log(rand);
                    greenNumb--;
                    return Color.green;
                }
                else { return GetRandomColor(); }

            case 3:
                if (yellowNumb > 0)
                {
                    Debug.Log(rand);
                    yellowNumb--;
                    return Color.yellow;
                }
                else { return GetRandomColor(); }

            case 4:
                if (greyNumb > 0)
                {
                    Debug.Log(rand);
                    greyNumb--;
                    return Color.grey;
                }
                else { return GetRandomColor(); }

        }

        return Color.white;

    }

  


}