using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_ : MonoBehaviour
{
    public static SceneManager_ Instance { get => instance; set => instance = value; }
    private static SceneManager_ instance;

    public AudioSource[] sources;
    public ParticleSystem particle;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
