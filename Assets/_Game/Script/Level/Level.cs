using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Canvas cv;
    [SerializeField] private Spawner spawner;

    private void OnEnable()
    {
        cv.renderMode = RenderMode.ScreenSpaceCamera;
        cv.worldCamera = Camera.main;
    }

    private void Start()
    {
        UIManager.Ins.mainCanvas.ResetUI();
    }

    public void StopSpawning()
    {
        spawner.StopCor();
    }
}
