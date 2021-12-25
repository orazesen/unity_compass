using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private bool gotObject = false;

    private float scale = 1;
    public float scaleFactor = 0.2f;

    public float maxScale = 5f;
    public float minScale = 1f;

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    public bool magnified = false;

    private void LateUpdate()
    {
            if (Input.GetMouseButton(0))
            {
                if (!gotObject)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
                        {
                            gotObject = true;
                        }
                    }
                }
                else
                {
                    mPosDelta = Input.mousePosition - mPrevPos;

                    if (Vector3.Dot(transform.up, Vector3.up) >= 0)
                    {
                        transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                    }
                    else
                    {
                        transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                    }
                    transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                gotObject = false;
            }
        mPrevPos = Input.mousePosition;

        if (magnified)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                scale += scaleFactor;
                if (scale >= maxScale)
                {
                    scale = maxScale;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                scale -= scaleFactor;
                if(scale <= minScale)
                {
                    scale = minScale;
                }
            }
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void DeMagnify()
    {
        Transform parent = GameObject.FindGameObjectWithTag("Holder").GetComponent<Transform>();

        Debug.Log(parent.name);

        Vector3 v = parent.localPosition;

        transform.localScale = new Vector3(1, 1, 1);

        v.x = 3;

        parent.localPosition = v;

        magnified = false;
        
    }

    public void Magnify()
    {
        Transform parent = GameObject.FindGameObjectWithTag("Holder").GetComponent<Transform>();

        Debug.Log(parent.name);

        Vector3 v = parent.localPosition;

        transform.localScale = new Vector3(1,1,1);

        v.x = 0;

        parent.localPosition = v;

        magnified = true;
    }
}
