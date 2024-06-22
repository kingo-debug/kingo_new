using UnityEngine;

public class VehicleCoolDown : MonoBehaviour
{//sc
[HideInInspector]
public GameObject Player;
public float SpawnTimeValue;
public bool Ready;





 private void Update() 
{
if(!Ready)
 {
   SpawnTimeValue -= 1 * Time.deltaTime;
  }
   if(SpawnTimeValue<=0)

        {
            Ready = true;
        }
}


}//ec
