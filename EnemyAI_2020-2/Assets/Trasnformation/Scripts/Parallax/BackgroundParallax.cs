using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public GameObject[] parallaxObjects;

    private Camera _mainCamera;
    private Transform _cameraPosition;
    private Vector2 _screenSize;

    private Vector3 _lastScreenPosition;
    
    [SerializeField] private Scriptable_Background _scriptableBackground;
    
    void Start()
    {
        _mainCamera = Camera.main;
        Vector3 screenSize = new Vector3(Screen.width, Screen.height, 0f);
        _screenSize = _mainCamera.ScreenToWorldPoint(screenSize);
        _cameraPosition = _mainCamera.gameObject.transform;

        _lastScreenPosition = _cameraPosition.position;
        CreateChildObjects();
    }

    
    void LateUpdate()
    {
        float difference = _cameraPosition.position.x - _lastScreenPosition.x;
        foreach (GameObject obj in parallaxObjects)
        {
            RepositionChild(obj);
            Vector3 v = MovementTranslation(obj, difference);
            obj.transform.Translate(v);
        }

        _lastScreenPosition = _mainCamera.transform.position;
    }

    private Vector3 MovementTranslation(GameObject obj, float difference)
    {
        Vector3 v = Vector3.right * difference;
        int i = obj.transform.GetSiblingIndex();
        switch (i)
        {
            case 0:
                v *= _scriptableBackground.scrollingSpeed[0];
                break;
            case 1:
                v *= _scriptableBackground.scrollingSpeed[1];
                break;
            case 2:
                v *= _scriptableBackground.scrollingSpeed[2];
                break;
        }
        return v;
    }
    
    private void CreateChildObjects()
    {
        foreach (GameObject obj in parallaxObjects)
        {
            float spriteWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            
            //Number of childs
            //float numOfObjects = Mathf.CeilToInt(spriteWidth / _screenSize.x) + 1;
            int numOfObjects = Mathf.CeilToInt((_screenSize.x * 2) / spriteWidth); 
            
            //Mold for the childs
            GameObject clone = Instantiate(obj);
            
            //Creating the childs
            for (int i = 0; i <= numOfObjects; i++)
            {
                Vector3 newPosition = new Vector3(
                    spriteWidth * i,
                    obj.transform.position.y,
                    obj.transform.position.z);
                GameObject c = Instantiate(clone, newPosition, Quaternion.identity, obj.transform);
                c.name = obj.name + i;
            }
            Destroy(clone);
            Destroy(obj.GetComponent<SpriteRenderer>());
        }
    }

    private void RepositionChild(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            
            if (_cameraPosition.position.x + _screenSize.x >
                lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position =
                    new Vector3(lastChild.transform.position.x + halfObjectWidth * 2,
                        lastChild.transform.position.y,
                        lastChild.transform.position.z);
                
            }else if (_cameraPosition.position.x - _screenSize.x <
                      firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position =
                    new Vector3(firstChild.transform.position.x - halfObjectWidth * 2,
                        firstChild.transform.position.y,
                        firstChild.transform.position.z);
            }
        }
    }
}
