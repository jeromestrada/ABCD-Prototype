using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSync : MonoBehaviour
{
    [SerializeField] private GameObject[] terrains;
    public float setDelay = 0.5f;
    int index = 0;
    bool IsSet = false;
    // Start is called before the first frame update
    void Awake()
    {
        terrains[index++].SetActive(true);
        IsSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSet)
        {
            IsSet = false;
            Invoke(nameof(SetTerrain), setDelay);
        }
    }

    void SetTerrain()
    {
        terrains[index++].SetActive(true);
        IsSet = true;
        Debug.Log($"INDEX {index}");
    }
}
