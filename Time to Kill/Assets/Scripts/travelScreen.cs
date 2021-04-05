using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class travelScreen : MonoBehaviour
{
    public string destination;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadNextScene());
    }

    IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(destination);
    }

}
