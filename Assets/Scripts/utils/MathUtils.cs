using UnityEngine;

public static class MathUtils
{
	public static float Remap(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
	{
		return targetFrom + (source - sourceFrom) * (targetTo - targetFrom) / (sourceTo - sourceFrom);
	}

	public static float snap(float value, float snapSize)
	{
		return Mathf.Round(value / snapSize) * snapSize;
	}
}
