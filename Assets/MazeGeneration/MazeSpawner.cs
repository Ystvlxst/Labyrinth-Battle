using UnityEditor;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
#if UNITY_EDITOR
    
    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);
    public Vector2 Size = new Vector2(1,1);
    public HintRenderer HintRenderer;

    public Maze maze;

    [ContextMenu("Generate")]
    private void Generate()
    {
        int childCount = transform.childCount;
        
        for (int i = 0; i < childCount; i++) 
            DestroyImmediate(transform.GetChild(0).gameObject);
        
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze((int) Size.x, (int) Size.y);

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell cell = (Cell) PrefabUtility.InstantiatePrefab(CellPrefab, transform);

                cell.transform.localPosition = new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z);
                cell.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                cell.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }

        HintRenderer?.DrawPath();
    }
#endif
}