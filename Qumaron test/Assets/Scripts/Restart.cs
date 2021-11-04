using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartScena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
