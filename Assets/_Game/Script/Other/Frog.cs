using EZCameraShake;
using System.Collections;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [Header("===EZ CameraShaker===")]
    [SerializeField] private float magnitude;
    [SerializeField] private float roughness;
    [SerializeField] private float fadeInTime;
    [SerializeField] float fadeOutTime;

    [Header("===Other===")]
    [SerializeField] private Animator anim;

    private int MaxHP = 3;
    public bool isDead;

    public void MoveLeft()
    {
        anim.Play(CacheString.TAG_Left_Frog);
    }

    public void MoveRight()
    {
        anim.Play(CacheString.TAG_Right_Frog);
    }

    private void TakeDamge()
    {
        MaxHP--;
        if (MaxHP <= 0)
        {
            //AudioManager.Ins.PlaySFX(AudioManager.Ins.dead);
            UIManager.Ins.mainCanvas.Hit();
            Debug.Log("Die");
            StartCoroutine(IELose());
        }
        else
        {
            UIManager.Ins.mainCanvas.Hit();
            Debug.Log("Take Damge");
        }

        AudioManager.Ins.PlaySFX(AudioManager.Ins.hurt);
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }

    private IEnumerator IELose()
    {
        isDead = true;
        anim.Play(CacheString.TAG_Dead_Frog);
        SimplePool.CollectAll();
        LevelManager.Ins.curLevel.StopSpawning();

        yield return new WaitForSeconds(1.5f);

        UIManager.Ins.TransitionUI<ChangeUICanvas, MainCanvas>(0.5f,
               () =>
               {
                   UIManager.Ins.OpenUI<LooseCanvas>();
               });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = Cache.GetObstacle(collision);
        if (obstacle != null)
        {
            TakeDamge();
            SimplePool.Despawn(obstacle);
        }
    }
}
