using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeedManager : MonoBehaviour
{
    [SerializeField] private string _currentSeed;
    [SerializeField] private string _newSeed;
    public string GetCurrentSeed => _currentSeed;

    private void Awake() => GenerateRandomSeed();

    public void GenerateRandomSeed()
    {
        int tempSeed = (int)System.DateTime.Now.Ticks;
        _currentSeed = tempSeed.ToString();

        Random.InitState(tempSeed);
    }

    [ContextMenu("Set new seed")]
    public void SetNewSeed()
    {
        SetRandomSeed(_newSeed);
        Debug.Log("new seed set!");
    }


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

    public void CopySeedToClipboard() => GUIUtility.systemCopyBuffer = _currentSeed;

    public static bool isNumeric(string s)
    {
        return int.TryParse(s, out int n);
    }

}
