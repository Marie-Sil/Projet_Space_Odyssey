using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void allerChezGhil()
    {
        SceneManager.LoadScene("ChezGhil");
    }

    public void allerMarcheNoir()
    {
        SceneManager.LoadScene("MarcheNoir");
    }

    public void allerHubCentral()
    {
        SceneManager.LoadScene("HubCentral");
    }

}
