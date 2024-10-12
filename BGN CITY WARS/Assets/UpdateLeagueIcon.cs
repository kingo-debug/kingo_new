using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
public class UpdateLeagueIcon : MonoBehaviour
{
    [SerializeField] private PhotonView PV;
    [SerializeField] private PhotonSerializerBGN serializerBGN;
    [SerializeField] private string LeagueName;
    void OnEnable()
    {
        PV = transform.root.GetComponent<PhotonView>();
        if (PV!= null&& !PV.IsMine)   // load for others
        {
            Invoke("LoadOtherPlayerLeague", 0.25f);
        }
        else
        {
            LeagueName = ES3.Load<string>("League");

            if(serializerBGN!=null) // sync data to others if available
            {
                serializerBGN.LeagueLogoPath = LeagueName;
            }

            // Load all sprites from the sprite sheet
            Sprite[] sprites = Resources.LoadAll<Sprite>("league ranking badges");

            // Find the specific sprite by name
            Sprite specificSprite = System.Array.Find(sprites, sprite => sprite.name == ES3.Load<string>("League"));

            // Check if the sprite was found
            if (specificSprite != null)
            {
                // Set the sprite to a SpriteRenderer (or use it as needed)
                GetComponent<Image>().sprite = specificSprite;
            }
 
            else
            {
                Debug.LogError("Sprite not found in the sprite sheet!");
            }

        
        } // load for player menu

    }


    void LoadOtherPlayerLeague()
    {
        LeagueName = serializerBGN.LeagueLogoPath;
        // Load all sprites from the sprite sheet
        Sprite[] sprites = Resources.LoadAll<Sprite>("league ranking badges");

        // Find the specific sprite by name
        Sprite specificSprite = System.Array.Find(sprites, sprite => sprite.name == LeagueName);

        // Check if the sprite was found
        if (specificSprite != null)
        {
            // Set the sprite to a SpriteRenderer (or use it as needed)
            GetComponent<Image>().sprite = specificSprite;
        }

        else
        {
            Debug.LogError("Sprite not found in the sprite sheet!");
        }
    }



}
