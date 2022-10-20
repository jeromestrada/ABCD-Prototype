using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeedManager : MonoBehaviour
{
    private string _currentSeed;
    [SerializeField] private string _seed;
    public bool setMySeed;
    public bool copySeedToClipboard;
    public string GetCurrentSeed => _currentSeed;

    private void Awake() => GenerateRandomSeed();

    public void GenerateRandomSeed()
    {
        if (!setMySeed)
        {
            int tempSeed = (int)System.DateTime.Now.Ticks;
            _currentSeed = tempSeed.ToString();

            Random.InitState(tempSeed);
        }
        else SetSeed();
        if(copySeedToClipboard) CopySeedToClipboard();
    }

    [ContextMenu("Set seed")]
    public void SetSeed()
    {
        SetRandomSeed(_seed);
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
