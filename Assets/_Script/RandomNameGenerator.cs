using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGenerator
{
    private static string[] firstNames = { "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Riley", "Quinn", "Jamie", "Avery", "Parker" };
    private static string[] surNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };

    public static string GenerateRandomName()
    {
        int randomIndex1 = Random.Range(0, firstNames.Length);
        int randomIndex2 = Random.Range(0, surNames.Length);
        return firstNames[randomIndex1] + " " + surNames[randomIndex2];
    }
}




