using UnityEngine;

public class TrocarArma : MonoBehaviour
{
    public GameObject arma1;
    public GameObject arma2;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arma1.SetActive(true);
            arma2.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arma1.SetActive(false);
            arma2.SetActive(true);
        }
    } 
}