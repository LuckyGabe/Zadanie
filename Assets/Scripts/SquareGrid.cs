using UnityEngine;

public class SquareGrid : MonoBehaviour
{
    public GameObject squarePrefab; //prefab pojedyńczej komórki

    //wymiary siatki
    public int width = 1000;
    public int height = 1000;

    //zmienne do pozycjonowania komórek
    private Vector3 pos = new Vector3(0, 0, 0);
    private Vector3 offSet = new Vector3(0, 0, 0);
    //Ile jeszcze obiektów może mieć dany kolor
    private int blueNumb = 600000, greenNumb = 100000, yellowNumb = 50000, greyNumb = 250000;

    private void Awake()
    {

        //tworzenie gridu
        for (int i = 0; i < height; i++)
        {
            //Poziom
            for (int j = 0; j < width; j++)
            {
                //stwórz komórkę
                GameObject square = Instantiate<GameObject>(squarePrefab, (pos + offSet), Quaternion.identity);
                //Złap skrypt komórki
                SquareCell cell = square.GetComponent<SquareCell>();

                //nadaj losowy kolor komórce
                square.GetComponent<SpriteRenderer>().color = cell.color = GetRandomColor();

                //Jeżeli komórka jest koloru zielonego lub żółtego zaznacz ją jako interaktywną
                if (cell.color == Color.green || cell.color == Color.yellow)
                {
                    square.tag = "Interactable";
                }

                //przypisz losowe parametry
                cell.IDx = Random.Range(1, 1001);
                cell.IDy = Random.Range(1, 1001);

                //zwiększ offset dla następnej komórki
                offSet.x += 1;

            }
            // zwiększ offset
            offSet.x = 0;
            offSet.y += 1;
        }


    }

    //Losowy kolor
    private Color GetRandomColor()
    {
        //wylosuj numer koloru (1 - niebieski, 2 - zielony, 3 - żółty, 4 - szary)
        int rand = Random.Range(1, 5);

        //Jeżeli jest już maksymalna ilość pól danego koloru, wywołaj tę funkcję jeszcze raz
        switch (rand)
        {
            case 1:

                if (blueNumb > 0)
                {
                    blueNumb--;
                    return Color.blue;
                }

                else { return GetRandomColor(); }
            case 2:
                if (greenNumb > 0)
                {

                    greenNumb--;
                    return Color.green;
                }
                else { return GetRandomColor(); }

            case 3:
                if (yellowNumb > 0)
                {

                    yellowNumb--;
                    return Color.yellow;
                }
                else { return GetRandomColor(); }

            case 4:
                if (greyNumb > 0)
                {

                    greyNumb--;
                    return Color.grey;
                }
                else { return GetRandomColor(); }

        }

        return Color.white;

    }


}
