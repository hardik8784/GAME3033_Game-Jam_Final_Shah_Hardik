///-----------------------------
///     Author          : Hardik Shah
///     Last Modified   : 2022/04/22
///     Description     : Script 
///     Revision History: Added Expanding Behaviour 
/// ----------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Land : MonoBehaviour
{
    public float ElapsedShrinkingTime;
    [Range(0.1f, 1000.0f)]
    public float TimeToShrink;

    [Header("Platform State")]
    [SerializeField] private bool isPlayerOn;
    [SerializeField] private bool isExpanding;

    // scaling properties
    private Vector3 tempScale;          
    private Vector4 originalScale;     
    float scale;                        
    private BoxCollider boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        originalScale = boxCollider.size;
        isPlayerOn = false;
        isExpanding = false;
        ElapsedShrinkingTime = TimeToShrink;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isPlayerOn)
        //{
        //    if (isExpanding)
        //    {
                Expand();
        //    }
        //}
        //else
        //{
        //    Shrink();
        //}

    }

    void Expand()
    {
        ElapsedShrinkingTime += Time.deltaTime;
        scale = ElapsedShrinkingTime / TimeToShrink;

        tempScale = transform.localScale;
        tempScale.x = scale;
        tempScale.y = 0.1f;
        tempScale.z = scale;
        transform.localScale = tempScale;


        //// when we return to the original scale, we're done expanding, change the if statement
        //if (scale >= 1.0f)
        //{
        //    scale = 1.0f;
        //    isExpanding = false;
        //}
    }

    void Shrink()
    {
        ElapsedShrinkingTime -= Time.deltaTime;
        scale = ElapsedShrinkingTime / TimeToShrink;

        tempScale = transform.localScale;
        tempScale.x = scale;
        tempScale.y = scale;
        tempScale.z = scale;
        transform.localScale = tempScale;

        // when we return to the original scale, we're done expanding, change the if statement
        if (scale <= 0.0f)
        {
            scale = 0.0f;
            Time.timeScale = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOn = true;
            isExpanding = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        isPlayerOn = false;
        isExpanding = true;
    }
}
