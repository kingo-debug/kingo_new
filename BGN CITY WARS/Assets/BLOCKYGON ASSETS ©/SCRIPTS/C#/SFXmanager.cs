using UnityEngine;
using Photon.Pun;

public class SFXmanager : MonoBehaviour
{
    private AudioSource AS;
    private PhotonView PV;

    [SerializeField]
    private AudioClip[] StepClips;

    [SerializeField]
    private AudioClip[] punchClips;

    [SerializeField]
    private AudioClip[] WaterSplash;

    [SerializeField]
    private AudioClip[] LadderClimb;

    public AudioClip[] ReloadPart ;
    [SerializeField]
    private AudioClip RollSFX;


    public AudioClip NewMessageItem;

    private void Start()
    {
        AS = this.GetComponent<AudioSource>();
        PV = this.GetComponent<PhotonView>();
        Invoke("DelayStart", 0.16f);


        #region SpatialBlend Specify
        if (PV.IsMine)
        {
            AS.spatialBlend = 0;
        }
        else AS.spatialBlend = 1;

        #endregion spatial blend 

    }
    #region AnimationEventFunctions
    public void PlayRandomPunchSFX()
    {
        if (punchClips.Length > 0)
        {
            // Choose a random index from the array
            int randomIndex = Random.Range(0, punchClips.Length);

            // Play the selected punch sound
            AS.PlayOneShot(punchClips[randomIndex]);
        }
        else
        {
            Debug.LogError("No punch clips available in the array.");
        }
    }

    public void PlayRandomStepSFX()
    {
        if (StepClips.Length > 0)
        {
            // Choose a random index from the array
            int randomIndex = Random.Range(0, StepClips.Length);

            // Play the selected punch sound
            AS.PlayOneShot(StepClips[randomIndex]);
           
        }
        else
        {
            Debug.LogError("No StepClips clips available in the array.");
        }
    }

    public void PlayRandomSplashSFX()
    {
        if (WaterSplash.Length > 0)
        {
            // Choose a random index from the array
            int randomIndex = Random.Range(0, WaterSplash.Length);

            // Play the selected punch sound
            AS.PlayOneShot(WaterSplash[randomIndex]);

        }
        else
        {
            Debug.LogError("No WaterSplash clips available in the array.");
        }
    }

    public void PlayLadderClimb()
    {
        if (LadderClimb.Length > 0)
        {
            // Choose a random index from the array
            int randomIndex = Random.Range(0, LadderClimb.Length);

            // Play the selected punch sound
            AS.PlayOneShot(LadderClimb[randomIndex]);

        }
        else
        {
            Debug.LogError("No LadderClimb clips available in the array.");
        }
    }
    //Weapons
    public void ReloadFirstPart()
    {
        AS.PlayOneShot(ReloadPart.GetValue(0) as AudioClip);
    }
    public void ReloadSecondPart()
    {
        AS.PlayOneShot(ReloadPart.GetValue(1) as AudioClip);
    }
    public void ReloadLastPart()
    {
        AS.PlayOneShot(ReloadPart.GetValue(2) as AudioClip);
    }

    // action
    public void RollingSFX()
    {
        AS.PlayOneShot(RollSFX);
    }
        #endregion


    [PunRPC]
    public void PlayMessageNotification()
    {
        AS.PlayOneShot(NewMessageItem);
    }
    void DelayStart()
    {
        RefreshVolumeSettings();
    }
    public void RefreshVolumeSettings()
    {
        AS.volume = ES3.Load<float>("SFX");
        WeaponStatus weapon = GetComponent<WeaponStatus>();
        weapon.CurrentWeapon.GetComponent<AudioSource>().volume = AS.volume;
    }



    }






