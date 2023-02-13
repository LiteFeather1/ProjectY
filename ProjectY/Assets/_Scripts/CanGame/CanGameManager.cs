using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGameManager : MonoBehaviour
{
    [SerializeField] private Can _heavyCanPrefab;
    [SerializeField] private Can _lightCanPrefab;
    [SerializeField] private MeshRenderer _canMeshRenderer;
    [SerializeField] float xPadding;
    // Limit 10
    [SerializeField] int rows;

    private float XSpacing => _canMeshRenderer.bounds.size.x / 2 + xPadding;

    //Ainda a testar o limite de empilhar as latas sem elas cairem, #fuckColliders
    //Reempilhar quando acabar o nivel
    //spawnar bolas

    void Start()
    {
        //Time.timeScale = 0;
        int canQuantity = rows;
        float height = transform.position.y + 0.01f;
        rows = Mathf.Clamp(canQuantity, 0, 10);
        for (int x = 0; x < rows; x++)
        {
            float xPos = transform.position.x + XSpacing / 2 - (XSpacing * canQuantity/2);
            for (int i = 0; i < canQuantity; i++)
            {
                Vector3 canPosition = new Vector3(xPos, height, transform.position.z);
                if(x == 0)
                {
                    Can newCan = Instantiate(_lightCanPrefab, canPosition, Quaternion.identity);
                }
                else
                {
                    Can newCan = Instantiate(_heavyCanPrefab, canPosition, Quaternion.identity);
                }
                xPos += XSpacing;
            }
            canQuantity--;
            height += _canMeshRenderer.bounds.size.y;
        }    
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
