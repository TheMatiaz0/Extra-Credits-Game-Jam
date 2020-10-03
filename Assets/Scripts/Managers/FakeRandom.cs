using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = System.Random;

public class FakeRandom
{
    private readonly Random random;
    private readonly SortedDictionary<int, int> concealeds = new SortedDictionary<int, int>();
    private readonly int waitingTime;

	public int Max { get; }
	public int Min { get; }
	public int WaitingTime { get; }
    /// <summary>
    /// Gets random value in range. Max is exclusive
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public int Next()
    {
        var matching = concealeds.Keys;
        int trueMax = Max - matching.Count;
        if (trueMax - Min == 0)
        {
            throw new ArgumentException("There's no number following rules of fake randomness. Waiting time is propably too small");
        }
        int result = random.Next(Min, trueMax);

        foreach (var concealedElement in matching)
        {
            if (result >= concealedElement)
            {
                result += 1;
            }
            else
            {
                break;
            }

        }
        foreach (var concealedElement in concealeds.ToArray())
        {
            concealeds[concealedElement.Key]--;
            if (concealedElement.Value == 1)
            {
                concealeds.Remove(concealedElement.Key);
            }
        }
        concealeds.Add(result, waitingTime);
        return result;

    }
    /// <summary>
    /// Max is exclusive.
    /// </summary>
    /// <param name="waitingTime"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="seed"></param>
    public FakeRandom(int waitingTime, int min, int max, int? seed = null)
    {
        if (waitingTime <= 0)
            throw new ArgumentException("waiting time has to be above 0");
        if (seed != null)
            random = new Random((int)seed);
        else
            random = new Random();
        this.Min = min;
        this.Max = max;
        this.waitingTime = waitingTime;
    }

}
