using UnityEngine;

public class MainMapTargetCenter : MonoBehaviour
{
    private MainMapWindow mainmap;
    [SerializeField] Transform PlayerTarget;
    private void OnEnable()
    {
        if(mainmap==null)
        {
            mainmap = GameObject.Find("SCENE MAPS").transform.GetChild(1).transform.GetChild(3).GetComponent<MainMapWindow>();
            mainmap.playerTarget = PlayerTarget;
        }
        else
        {
            mainmap.playerTarget = PlayerTarget;
        }



    }

}
