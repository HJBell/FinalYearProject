using System.Collections.Generic;
using UnityEngine;

public static class HJBUnityUtils
{
    public static T GetRandomFrom<T>(T[] input)
    {
        if (input.Length == 0) Debug.LogError("Array empty!");
        return input[Random.Range(0, input.Length)];
    }

    public static T[] GetRandomsFrom<T>(T[] input, uint outputCount)
    {
        T[] output = new T[outputCount];
        for (int i = 0; i < outputCount; i++)
            output[i] = GetRandomFrom(input);
        return output;
    }

    public static T[] GetNonRepeatingRandomsFrom<T>(T[] input, uint outputCount)
    {
        outputCount = (uint)Mathf.Min((int)outputCount, input.Length);
        List<T> inputCopy = new List<T>(input);
        T[] output = new T[outputCount];
        for (int i = 0; i < outputCount; i++)
        {
            int index = Random.Range(0, inputCopy.Count);
            output[i] = inputCopy[index];
            inputCopy.RemoveAt(index);
        }
        return output;
    }

    public static T[] RandomiseArrayOrder<T>(T[] input)
    {
        return GetNonRepeatingRandomsFrom(input, (uint)input.Length);
    }

    public static void PrintArray<T>(T[] input)
    {
        if (input.Length == 0) return;
        string output = "";
        for (int i = 0; i < input.Length - 1; i++)
            output += input[i].ToString() + ", ";
        output += input[input.Length - 1];
        Debug.Log(output);
    }
}