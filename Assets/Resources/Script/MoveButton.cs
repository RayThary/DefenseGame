using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool isRightButton = false;
    private bool _check = false;


    public void OnPointerDown(PointerEventData eventData)
    {
        _check = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _check = false;
    }

    private Button _btn;
    [SerializeField] private float speed;

    private Transform _camera;
    void Start()
    {
        _camera = Camera.main.transform;
    }

    void Update()
    {
        if (isRightButton)
        {
            if (_check)
            {

                if (_camera.position.x <= 5)
                {
                    _camera.position += Vector3.right * Time.deltaTime * speed;
                }
            }
        }
        else
        {
            if (_check)
            {
                if (_camera.position.x >= 0)
                {
                    _camera.position += Vector3.left * Time.deltaTime * speed;
                }
            }
        }
    }
}
