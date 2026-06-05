using UnityEngine;
using System;

public static class MathFunctions
{
    public static float Sigmoid(float value)
    {
        return 1.0f / (1.0f + (float)Math.Exp(-value));
    }

    public static float CheckInterval(float value, float limit)
    {
        if (value > limit)
        {
            value = limit;
        }
        if (value < -limit)
        {
            value = -limit;
        }
        return value;
    }

    public static float CheckRightSideInterval(float value, float limit)
    {
        if (value > limit)
        {
            value = limit;
        }
        if (value < 0f)
        {
            value = 0f;
        }
        return value;
    }
}
