using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    [SerializeField]
    private Sprite holdingEgg;
    [SerializeField]
    private Sprite noEgg;
    private GameObject hoveredObject;
    private List<GameObject> closeObjects = new();
    private bool hasEgg;
    [SerializeField]
    private float _pickupRange = 1;
    [SerializeField]
    private GameObject _eggPrefab;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = hasEgg ? holdingEgg : noEgg;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HandleOutline();

        if(!Input.GetMouseButtonUp(0)) return;

        if(hasEgg && hoveredObject) return;

        if(!hasEgg && hoveredObject) 
        {
            hasEgg = true;
            ServiceManager.Instance.Get<OnEggTaken>().Invoke();
            Destroy(hoveredObject);
            return;
        }
        // if(hasEgg && !hoveredObject) 
        // {
        //     hasEgg = false;
        //     Instantiate(_eggPrefab, mousePos, Quaternion.identity);
        //     return;
        // }
    }

    void HandleOutline() {
        closeObjects.RemoveAll(x => !x);
        if(hoveredObject) hoveredObject = null;
        
        closeObjects.ForEach(x => x.transform.localScale = Vector3.one);

        closeObjects = Physics2D.OverlapCircleAll(transform.position, _pickupRange)
                                .Where(x => x.GetComponent<EggScript>()).Select(x => x.gameObject).ToList();
        closeObjects.ForEach(x => x.transform.localScale = Vector3.one * 1.1f);

        var hit = closeObjects.Where(x => Vector2.Distance(x.transform.position, mousePos) < 0.5f)
                                .OrderBy(x => Vector2.Distance(x.transform.position, mousePos)).FirstOrDefault();
        if(!hit) return;

        hoveredObject = hit;
    }
}
