using UnityEngine;
using Photon.Pun;

public class BillBoard2D : MonoBehaviour
{
    public bool DisableForME = false;
    public bool DisableForOthers = false;

    void LateUpdate()
    {
        // Look at the camera in 2D space
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 direction = transform.position - cameraPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Start()
    {
        PhotonView photonView = transform.root.GetComponent<PhotonView>();

        if (DisableForME)
        {
            if (photonView.IsMine)
            {
                gameObject.SetActive(false);
            }
        }

        if (DisableForOthers)
        {
            if (!photonView.IsMine)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
