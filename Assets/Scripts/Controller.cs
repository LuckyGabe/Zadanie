using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //UI
    public Text IDxText, IDyText;
    public GameObject Stats;

    //Szybkość poruszania się kamery
    public float scrollSpeed = 20;


    // Update is called once per frame
    void Update()
    {
        //po naciśnięciu prawego przycisku myszy
        if (Input.GetMouseButton(1))
        {
            //Ruch kamerą
            CameraMovement();
        }

        //po naciśnięciu lewego przycisku myszy
        if (Input.GetMouseButtonDown(0))
        {

            HandleClick();
        }

    }
    void CameraMovement()
    {
        //Jeżeli kursor poruszył się w lewo
        if (Input.GetAxis("Mouse X") < 0)
        {
            //przesuń kamerę w prawo
            transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed, Space.World);

        }
        //Jeżeli kursor poruszył się w prawo
        if (Input.GetAxis("Mouse X") > 0)
        {
            //przesuń kamerę w lewo
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed, Space.World);

        }
        //Jeżeli kursor poruszył się w dół
        if (Input.GetAxis("Mouse Y") < 0)
        {
            //przesuń kamerę w dół
            transform.Translate(Vector3.down * Time.deltaTime * scrollSpeed, Space.World);

        }
        //Jeżeli kursor poruszył się w górę
        if (Input.GetAxis("Mouse Y") > 0)
        {
            //przesuń kamerę w górę
            transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed, Space.World);

        }

    }



    void HandleClick()
    {
        //wynik raycastu 
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        //Jeżeli pole jest interaktywne
        if (rayHit.collider.CompareTag("Interactable"))
        {
            //Złap skrypt
            SquareCell cell = rayHit.collider.GetComponent<SquareCell>();

            //Pokaż okno ze statystykami
            Stats.SetActive(true);
            //Ustaw tekst statystyk
            IDxText.text = "ID x: " + cell.IDx;
            IDyText.text = "ID y: " + cell.IDy;

        }
        //Jeżeli gracz kliknął na pole które nie jest interaktywne, dezaktywuj okno ze statystykami
        else { Stats.SetActive(false); }

    }

}
