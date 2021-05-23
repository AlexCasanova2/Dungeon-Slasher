using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructableTiles : MonoBehaviour
{
    public Tilemap destructableTileMap;
    public bool isOn;
    void Start()
    {
        destructableTileMap = GetComponent<Tilemap>();

        //Save system
        if (SaveManager.instance.hasLoaded)
        {
            isOn = SaveManager.instance.activeSave.destructedTiles;
        }


        if (isOn)
        {
            gameObject.GetComponent<TilemapRenderer>().enabled = false;
            gameObject.GetComponent<TilemapCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
                Vector3 hitPosition = Vector3.zero;
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    destructableTileMap.SetTile(destructableTileMap.WorldToCell(hitPosition), null);
                    //Debug.Log("Posicion x: " + hitPosition);
                    isOn = true;

                    SaveManager.instance.activeSave.destructedTiles = isOn;
                }
            
        }
    }

   



}
