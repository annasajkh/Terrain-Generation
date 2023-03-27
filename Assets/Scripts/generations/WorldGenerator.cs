using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private Player player;
    public Chunk chunk;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        GenerateAroundPlayer(Global.spawnPosition.x, Global.spawnPosition.z);
    }

    public void GenerateAroundPlayer(float x, float z)
    {
        float centerX = MathUtils.snap(x - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);
        float centerZ = MathUtils.snap(z - Global.chunkFullSize * Global.renderDistance / 2, Global.chunkFullSize);

        for (int i = 0; i < Global.renderDistance; i++)
        {
            for (int j = 0; j < Global.renderDistance; j++)
            {
                if (!Global.chunkIds.Contains((int)(centerX + j * Global.chunkFullSize) + "," + (int)(centerZ + i * Global.chunkFullSize)))
                {
                    Global.chunks.Add(Instantiate(chunk, new Vector3(centerX + j * Global.chunkFullSize, 0, centerZ + i * Global.chunkFullSize), Quaternion.identity));
                    Global.chunkIds.Add(Global.chunks[Global.chunks.Count - 1].GetID());
                }
            }
        }

        Global.worldCenterX = x;
        Global.worldCenterZ = z;
    }

    private void Update()
    {
        for (int i = Global.chunks.Count - 1; i >= 0; i--)
        {
            if (Vector2.SqrMagnitude(new Vector2(Mathf.Abs(Global.chunks[i].transform.position.x - player.transform.position.x),
                                                 Mathf.Abs(Global.chunks[i].transform.position.z - player.transform.position.z))) > Global.regenerateTriggerDistance2 * 30)
            {
                Destroy(Global.chunks[i].gameObject);
                Global.chunkIds.Remove(Global.chunks[i].GetID());
                Global.chunks.Remove(Global.chunks[i]);
            }
        }

        if (Vector2.SqrMagnitude(new Vector2(Mathf.Abs(player.transform.position.x - Global.worldCenterX), 
                                             Mathf.Abs(player.transform.position.z - Global. worldCenterZ))) > Global.regenerateTriggerDistance2)
        {
            GenerateAroundPlayer(player.transform.position.x, player.transform.position.z);
        }
    }
}
