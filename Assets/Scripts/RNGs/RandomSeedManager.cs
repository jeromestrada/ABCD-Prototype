using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeedManager : MonoBehaviour
{
    [SerializeField] private string _currentSeed;
    public string GetCurrentSeed => _currentSeed;

    private void Awake() => GenerateRandomSeed();

    public void GenerateRandomSeed()
    {
        int tempSeed = (int)System.DateTime.Now.Ticks;
        _currentSeed = tempSeed.ToString();

        Random.InitState(tempSeed);
    }

    [ContextMenu("Generate test seed")]
    public void GenerateTestSeed()
    {
        SetRandomSeed("test");
        Debug.Log("test seed generated!");
    }

    //Select the Seed for the System
    public void SetRandomSeed(string seed = "test")
    {
        _currentSeed = seed;

        int tempSeed = 0;

        if (isNumeric(seed))
            tempSeed = System.Int32.Parse(seed);
        else
            tempSeed = seed.GetHashCode();

        Random.InitState(tempSeed);
    }
    public void SetRandomSeed(int seed)
    {
        _currentSeed = seed.ToString();
        int tempSeed = seed;
        Random.InitState(tempSeed);
    }

    //Copy Seed to Clipboard
    public void CopySeedToClipboard() => GUIUtility.systemCopyBuffer = _currentSeed;

    //Check if Seed is All numbers
    public static bool isNumeric(string s)
    {
        return int.TryParse(s, out int n);
    }

}
