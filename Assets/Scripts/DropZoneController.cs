using UnityEngine;
using System.Collections;

public class DropZoneController : MonoBehaviour {

	Renderer dropzoneRenderer;
	Collider2D dropzoneCollider;

	public int id;

	// Use this for initialization
	void Start () 
	{
		Toogle ();
	}

	public void Toogle(){
		dropzoneRenderer = GetComponents<Renderer>()[0];
		dropzoneRenderer.enabled = !dropzoneRenderer.enabled;

		dropzoneCollider = GetComponents<Collider2D>()[0];
		dropzoneCollider.enabled = !dropzoneCollider.enabled;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetId(int pos){
		id = pos;
	}
		
	public int GetId(){
		return id;
	}
}
