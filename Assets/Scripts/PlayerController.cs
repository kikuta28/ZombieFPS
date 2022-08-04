using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    float x, z;

    float speed = 3f;

    Vector3 direction;

    Rigidbody rb;

    float xRot, zRot;

    public GameObject cam;

    Quaternion cameraRot;

    Quaternion characterRot;

    float Xsensityvity = 3f,Ysensityvity = -3f;

    float minX = -90f, maxX = 90f;

    int playerHp = 100;

    public Slider hpBer;

    public GameObject gameOverText;

    public GameObject gameClearText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        hpBer.value = playerHp;

        gameOverText.SetActive(false);

        gameClearText.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        direction = transform.right * x + transform.forward * z;
        direction.y = 0;
        direction.Normalize();
        direction *= speed;

        float xRot = Input.GetAxisRaw("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxisRaw("Mouse Y") * Xsensityvity;

        characterRot *= Quaternion.Euler(0, -xRot, 0);

        Vector3 fuga = (cameraRot * Quaternion.Euler(-yRot, 0, 0)).eulerAngles;
        if (
            (fuga.x < -minX % 360 || fuga.x > -maxX % 360) &&
            fuga.y == 0 &&
            fuga.z == 0
        )
        {
            cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        }
        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.z);
    }

    public void TakeHit(float damage)
    {
        playerHp -= (int)damage;
        hpBer.value = playerHp;

        if(playerHp <= 0 ){
        gameOverText.SetActive(true);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "goal") {
            gameClearText.SetActive(true);
        }
    }

}
