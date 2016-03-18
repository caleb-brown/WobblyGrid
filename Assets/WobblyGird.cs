using UnityEngine;
using System.Collections;

public class WobblyGird : MonoBehaviour 
{
    /// <summary>
    /// TO DO List:
    ///     1. Update colors in realtime.
    ///     2. In Game free Roam camera.
    ///     3. Some Sort of GUI to change public variables.
    /// </summary>

    public GameObject Block;
    public GameObject[] BlockList;
    public Material BlockMat;

    public int GridX, GridZ, GridDivision;
    public float BlockLength, BlockWidth, GridHeight, Spacing, Scale, Amplitude, Frequency;
    public bool MakeColors, AnimateBlocks;

    int GridSize, BlockIndex;

	// Use this for initialization
	void Start () 
    {
        GridSize = GridX * GridZ;
        BlockList = new GameObject[GridSize];
        
        for (int x = 1; x <= GridX; x++)
        {
            for (int z = 1; z <= GridZ; z++)
            {
                //instantiate objects
                GameObject BlockInstance = (GameObject)Instantiate(Block);
                BlockInstance.transform.parent = this.transform;
                BlockInstance.name = "Block " + BlockIndex;

                float tempX = 1;
                float tempZ = 1;

                if (x > GridX / GridDivision)
                    tempX = (GridX / GridDivision) - (x - (GridX / GridDivision));
                else
                    tempX = x;

                if (z > GridZ / GridDivision)
                    tempZ = (GridZ / GridDivision) - (z - (GridZ / GridDivision));
                else
                    tempZ = z;

                if (MakeColors)
                {
                    float colorPercentR = tempX / GridX;
                    float colorPercentG = (tempX * tempZ) / (GridSize / GridDivision);
                    float colorPercentB = tempZ / GridZ;

                    BlockMat.color = new Vector4(colorPercentR, colorPercentG, colorPercentB, 1);
                    BlockInstance.GetComponent<Renderer>().material.color = BlockMat.color;
                }

                BlockList[BlockIndex] = BlockInstance;
                BlockList[BlockIndex].transform.position = new Vector3((x-1) * Spacing, (tempX * tempZ) * GridHeight, (z-1) * Spacing);
                BlockList[BlockIndex].transform.localScale = new Vector3(BlockWidth, BlockLength, BlockWidth);
                BlockIndex++;
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (AnimateBlocks)
        {
            for (int i = 0; i < GridSize; i++)
            {
                float wave = Scale * Mathf.Sin(Time.fixedTime * Amplitude + (i * Frequency));
                float secondWave = Scale * Mathf.PerlinNoise(Time.fixedTime * Amplitude + (i * Frequency), Time.fixedTime * Amplitude + (i * Frequency));

                BlockList[i].transform.position = new Vector3(BlockList[i].transform.position.x, BlockList[i].transform.position.y + (wave * secondWave) * Random.Range(1, 3), BlockList[i].transform.position.z);
            }
        }
	}
}
