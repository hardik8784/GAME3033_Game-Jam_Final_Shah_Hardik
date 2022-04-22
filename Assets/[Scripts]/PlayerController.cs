///-----------------------------
///     Author          : Hardik Shah
///     Last Modified   : 2022/04/22
///     Description     : Script 
///     Revision History: Added Controller for player
/// ----------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isWalking;
    public bool isJumping;
    public bool isRunning;
    public bool isFalling;

    public int CountdownTimer = 10;
    public TextMeshProUGUI CountdownTimerText;


    IEnumerator CountdownToStart()
    {
        while(CountdownTimer >0)
        {
            CountdownTimerText.text = "Timer : " + CountdownTimer.ToString();

            yield return new WaitForSeconds(1f);

            CountdownTimer--;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
    }


    //public void GoToWin()
    //{
    //    // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
    //}
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
