using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EasingTypes
{
    Linear,
    InQuad,
    OutQaud,
    InOutQuad,
    InCubic,
    OutCubic,
    InOutCubic,
    InQuart,
    OutQuart,
    InOutQuart,
    InQuint,
    OutQuint,
    InOutQuint,
    InSine,
    OutSine,
    InOutSine,
    InExpo,
    OutExpo,
    InOutExpo,
    InCirc,
    OutCirc,
    InOutCirc,
    InElastic,
    OutElastic,
    InOutElastic,
    InBack,
    OutBack,
    InOutBack,
    InBounce,
    OutBounce,
    InOutBounce
}

public static class Easing
{
    public static Vector3 DoEasing(EasingTypes types, Vector3 start, Vector3 end, float t)
    {
        float x = start.x + (Easing.DoEasing(types, t) * (end.x - start.x));
        float y = start.y + (Easing.DoEasing(types, t) * (end.y - start.y));
        float z = start.z + (Easing.DoEasing(types, t) * (end.z - start.z));

        return new Vector3(x, y, z);
    }

    public static Color DoEasing(EasingTypes types, Color start, Color end, float t)
    {
        float r = start.r + (Easing.DoEasing(types, t) * (end.r - start.r));
        float g = start.g + (Easing.DoEasing(types, t) * (end.g - start.g));
        float b = start.b + (Easing.DoEasing(types, t) * (end.b - start.b));
        float a = start.a + (Easing.DoEasing(types, t) * (end.a - start.a));

        return new Color(r, g, b, a);
    }

    public static float DoEasing(EasingTypes types, float start, float end, float t)
    {
        return start + (Easing.DoEasing(types, t) * (end - start));
    }

    public static float DoEasing(EasingTypes type, float t)
    {
        switch (type)
        {
            case EasingTypes.Linear:
                return Linear(t);
            case EasingTypes.InQuad:
                return inQuad(t);
            case EasingTypes.OutQaud:
                return outQuad(t);
            case EasingTypes.InOutQuad:
                return inOutQuad(t);
            case EasingTypes.InCubic:
                return inCubic(t);
            case EasingTypes.OutCubic:
                return outCubic(t);
            case EasingTypes.InOutCubic:
                return inOutCubic(t);
            case EasingTypes.InQuart:
                return inQuart(t);
            case EasingTypes.OutQuart:
                return outQuart(t);
            case EasingTypes.InOutQuart:
                return inOutQuart(t);
            case EasingTypes.InQuint:
                return inQuint(t);
            case EasingTypes.OutQuint:
                return outQuint(t);
            case EasingTypes.InOutQuint:
                return inOutQuint(t);
            case EasingTypes.InSine:
                return inSine(t);
            case EasingTypes.OutSine:
                return outSine(t);
            case EasingTypes.InOutSine:
                return inOutSine(t);
            case EasingTypes.InExpo:
                return inExpo(t);
            case EasingTypes.OutExpo:
                return outExpo(t);
            case EasingTypes.InOutExpo:
                return inOutExpo(t);
            case EasingTypes.InCirc:
                return inCirc(t);
            case EasingTypes.OutCirc:
                return outCirc(t);
            case EasingTypes.InOutCirc:
                return inOutCirc(t);
            case EasingTypes.InElastic:
                return inElastic(t);
            case EasingTypes.OutElastic:
                return outElastic(t);
            case EasingTypes.InOutElastic:
                return inOutElastic(t);
            case EasingTypes.InBack:
                return inBack(t);
            case EasingTypes.OutBack:
                return outBack(t);
            case EasingTypes.InOutBack:
                return inOutBack(t);
            case EasingTypes.InBounce:
                return inBounce(t);
            case EasingTypes.OutBounce:
                return outBounce(t);
            case EasingTypes.InOutBounce:
                return inOutBounce(t);
            default:
                return 0;
        }
    }

    public static float Linear(float t)
    {
        return t;
    }

    public static float inQuad(float t)
    {
        return t * t;
    }

    public static float outQuad(float t)
    {
        return t * (2 - t);
    }

    public static float inOutQuad(float t)
    {
        return t < .5 ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }

    public static float inCubic(float t)
    {
        return t * t * t;
    }

    public static float outCubic(float t)
    {
        return (--t) * t * t + 1;
    }

    public static float inOutCubic(float t)
    {
        return t < .5 ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
    }

    public static float inQuart(float t)
    {
        return t * t * t * t;
    }

    public static float outQuart(float t)
    {
        return 1 - (--t) * t * t * t;
    }

    public static float inOutQuart(float t)
    {
        return t < .5 ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;
    }

    public static float inQuint(float t)
    {
        return t * t * t * t * t;
    }

    public static float outQuint(float t)
    {
        return 1 + (--t) * t * t * t * t;
    }

    public static float inOutQuint(float t)
    {
        return t < .5 ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;
    }

    public static float inSine(float t)
    {
        return -1 * Mathf.Cos(t / 1f * (Mathf.PI * 0.5f)) + 1; ;
    }

    public static float outSine(float t)
    {
        return Mathf.Sin(t / 1f * (Mathf.PI * 0.5f));
    }

    public static float inOutSine(float t)
    {
        return -1 / 2 * (Mathf.Cos(Mathf.PI * t) - 1);
    }

    public static float inExpo(float t)
    {
        return (t == 0) ? 0 : Mathf.Pow(2, 10 * (t - 1));
    }

    public static float outExpo(float t)
    {
        return (t == 1) ? 1 : (-Mathf.Pow(2, -10 * t) + 1);
    }

    public static float inOutExpo(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        if ((t /= 1 / 2) < 1) return 1 / 2 * Mathf.Pow(2, 10 * (t - 1));
        return 1 / 2 * (-Mathf.Pow(2, -10 * --t) + 2);
    }

    public static float inCirc(float t)
    {
        return -1 * (Mathf.Sqrt(1 - t * t) - 1);
    }

    public static float outCirc(float t)
    {
        return Mathf.Sqrt(1 - (t = t - 1) * t);
    }

    public static float inOutCirc(float t)
    {
        if ((t /= 1 / 2) < 1) return -1 / 2 * (Mathf.Sqrt(1 - t * t) - 1);
        return 1 / 2 * (Mathf.Sqrt(1 - (t -= 2) * t) + 1);
    }

    public static float inElastic(float t)
    {
        float s = 1.70158f;
        float p = 0;
        var a = 1;
        if (t == 0) return 0;
        if (t == 1) return 1;
        if (p == 0) p = (0.3f * 1.5f);

        if (a < 1)
        {
            a = 1;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(1 / a);
        }

        return -(a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t - s) * (2 * Mathf.PI) / p));
    }

    public static float outElastic(float t)
    {
        float s = 1.70158f;
        float p = 0;
        var a = 1;
        if (t == 0) return 0;
        if (t == 1) return 1;
        if (p == 0) p = 0.3f;
        if (a < 1)
        {
            a = 1;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(1 / a);
        }

        return a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t - s) * (2f * Mathf.PI) / p) + 1;
    }

    public static float inOutElastic(float t)
    {
        float s = 1.70158f;
        float p = 0;
        var a = 1;
        if (t == 0) return 0;
        if ((t /= 1 / 2) == 2) return 1;
        if (p == 0) p = (0.3f * 1.5f);
        if (a < 1)
        {
            a = 1;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(1 / a);
        }

        if (t < 1) return -.5f * (a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t - s) * (2 * Mathf.PI) / p));
        return a * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t - s) * (2 * Mathf.PI) / p) * 0.5f + 1;
    }

    public static float inBack(float t)
    {
        float s = 1.70158f;
        return 1 * t * t * ((s + 1) * t - s);
    }

    public static float outBack(float t)
    {
        float s = 1.70158f;
        return 1 * ((t = t / 1 - 1) * t * ((s + 1) * t + s) + 1);
    }

    public static float inOutBack(float t)
    {
        float s = 1.70158f;
        if ((t /= 1 / 2) < 1) return 1 / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s));
        return 1 / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2);
    }

    public static float inBounce(float t)
    {
        return 1 - outBounce(1 - t);
    }

    public static float outBounce(float t)
    {
        if ((t /= 1) < (1 / 2.75))
        {
            return (7.5625f * t * t);
        }
        else if (t < (2 / 2.75))
        {
            return (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f);
        }
        else if (t < (2.5 / 2.75))
        {
            return (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f);
        }
        else
        {
            return (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f);
        }
    }

    public static float inOutBounce(float t)
    {
        if (t < 1 / 2) return inBounce(t * 2) * 0.5f;
        return outBounce(t * 2f - 1f) * 0.5f + 0.5f;
    }

}
