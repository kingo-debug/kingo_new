using UnityEngine;
using System.Collections;

public class JetPackManager : MonoBehaviour
{
    private CharacterController Charcontroller;
    public bool JetPackActive;

    [SerializeField]
    private bool CanAccel = false;
    private MainCharacterController maincont;
    public float initialJetpackSpeed = 0.5f;
    public float maxJetpackSpeed = 5f;
    public float accelerationRate = 0.1f;

    private float currentJetpackSpeed;

    private void Start()
    {
        Charcontroller = GetComponent<CharacterController>();
        maincont = GetComponent<MainCharacterController>();

    }
    private void Update()
    {

        if (Input.GetButton("Jump") && !maincont.Jumping && JetPackActive)
        {
            AccelerateJP();
        }
        else currentJetpackSpeed = Mathf.Clamp(currentJetpackSpeed - accelerationRate * Time.deltaTime, initialJetpackSpeed, maxJetpackSpeed);

    }

  void AccelerateJP()
    {
        // Gradually increase the jetpack speed
        currentJetpackSpeed = Mathf.Clamp(currentJetpackSpeed + accelerationRate * Time.deltaTime, initialJetpackSpeed, maxJetpackSpeed);

        // Move the character controller upwards using the jetpack speed
        Charcontroller.Move(Vector3.up * Time.deltaTime * currentJetpackSpeed);



    }



}
