///-----------------------------
///     Author          : Hardik Shah
///     Last Modified   : 2022/04/22
///     Description     : Script 
///     Revision History: Added Win Condition
/// ----------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //Debug.Log(other.gameObject);
            //print("You Won");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
            //Destroy(other.gameObject);
        }
    }
}
