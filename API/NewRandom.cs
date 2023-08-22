using System;
using System.Collections.Generic;
using UnityEngine;

public class NewRandom : MonoBehaviour
{
    public static int GetRandomInAB (int minA, int maxB)
    {
        int iRandomBack = -1; maxB++;
        if (maxB > minA)
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            int Randseed = BitConverter.ToInt32(bytes, 0);

            string strTick = Convert.ToString(DateTime.Now.Ticks);
            if (strTick.Length > 8)
                strTick = strTick.Substring(strTick.Length - 8, 8);
            Randseed = Randseed + Convert.ToInt32(strTick);
            System.Random random = new System.Random(Randseed);
            iRandomBack = random.Next(minA, maxB);
        }
        return iRandomBack;
    }

    public static bool Getprobability (float probability)
    {
        probability *= 1000f;
        int temp_return = NewRandom.GetRandomInAB(1, 100000);
        if (probability <= temp_return)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 警告：无法保证该代码准确性与安全性，非必要情况禁止使用。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static T GetRandom_T<T> (T min, T max)
    where T : struct, IComparable, IFormattable, IConvertible
    {
        if (!typeof(T).IsPrimitive)
            throw new ArgumentException("T must be a non-nullable value type.");
        if (Comparer<T>.Default.Compare(max, min) <= 0)
            throw new ArgumentOutOfRangeException(nameof(max), "max must be greater than min.");

        int minInt = Convert.ToInt32(min);
        int maxInt = Convert.ToInt32(max);

        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        byte[] bytes = new byte[4];
        rng.GetBytes(bytes);
        int Randseed = BitConverter.ToInt32(bytes, 0);

        string strTick = Convert.ToString(DateTime.Now.Ticks);
        if (strTick.Length > 8)
            strTick = strTick.Substring(strTick.Length - 8, 8);
        Randseed = Randseed + Convert.ToInt32(strTick);

        System.Random random = new System.Random(Randseed);

        // 预防将变量类型转换时因为类型不同而产生错误
        if (typeof(T) == typeof(Byte))
            return (T)(object)(byte)random.Next(minInt, maxInt);
        if (typeof(T) == typeof(Int16))
            return (T)(object)(short)random.Next(minInt, maxInt);
        if (typeof(T) == typeof(Int32))
            return (T)(object)random.Next(minInt, maxInt);
        if (typeof(T) == typeof(Int64))
            return (T)(object)(long)random.Next(minInt, maxInt);
        if (typeof(T) == typeof(Single))
            return (T)(object)(float)random.Next(minInt, maxInt);
        if (typeof(T) == typeof(Double))
            return (T)(object)(double)random.Next(minInt, maxInt);

        throw new ArgumentException("Unsupported type.");
    }
}