using System.Collections.Generic;
using UnityEngine;

public static class PerlinNoiseUtils
{
    private static List<float> noises = new List<float>(Global.worldNoiseOctaves);

    public static float GetFractalNoise(float x, float y)
    {
        float worldNoiseLacunarity = Global.worldNoiseLacunarity;
        float worldNoisePersistance = Global.worldNoisePersistance;

        float maxSum = 0;

        for (int i = 0; i < Global.worldNoiseOctaves; i++)
        {
            noises.Add(Mathf.PerlinNoise(x / Global.worldNoiseScale * worldNoiseLacunarity,
                                         y / Global.worldNoiseScale * worldNoiseLacunarity) * worldNoisePersistance);

            maxSum += 1 * worldNoisePersistance;

            worldNoiseLacunarity *= worldNoiseLacunarity;
            worldNoisePersistance *= worldNoisePersistance;
        }

        float sum = 0;

        for (int i = 0; i < noises.Count; i++)
        {
            sum += noises[i];
        }

        noises.Clear();

        sum = MathUtils.Remap(sum , -maxSum, maxSum, -1, 1);

        return sum;
    }
}
