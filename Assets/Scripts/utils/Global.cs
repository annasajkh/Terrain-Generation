using System.Collections.Generic;
using UnityEngine;


public static class Global
{
    public static int chunkSize = 50;
    public static float chunkResolution = 1;
    public static float chunkFullSize = chunkSize * chunkResolution;

    public static int renderDistance = 5;

    public static int worldNoiseOctaves = 3;
    public static float worldNoiseLacunarity = 2f;
    public static float worldNoisePersistance = 0.25f;
    public static float worldNoiseScale = 10;

    public static float worldMaxHeight = 50;


    public static List<Chunk> chunks = new List<Chunk>(renderDistance * renderDistance);
    public static List<string> chunkIds = new List<string>(renderDistance * renderDistance);

    public static Vector3 spawnPosition = new Vector3(0, 50, 0);

    public static float worldCenterX = spawnPosition.x;
    public static float worldCenterZ = spawnPosition.z;

    public static float regenerateTriggerDistance2 = (chunkFullSize * renderDistance * 0.25f) *
                                                     (chunkFullSize * renderDistance * 0.25f);

}
