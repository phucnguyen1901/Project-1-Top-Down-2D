using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField]
    private Material material;

    private SpriteRenderer spriteRenderer;

    private Material materialDefault;

    public float TimeFlash = .2f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        materialDefault = spriteRenderer.material;
    }

    public void Flashing(){
        spriteRenderer.material = material;
        StartCoroutine(ResetMaterialFlash());
    }

    private IEnumerator ResetMaterialFlash(){
        yield return new WaitForSeconds(TimeFlash);
        spriteRenderer.material = materialDefault;
    }
}
