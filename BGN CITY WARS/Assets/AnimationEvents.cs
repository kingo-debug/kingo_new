using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private ParticleSystem BulletTrail;

    void PlayFireTrail()
    {
        BulletTrail.Play();
    }

}
