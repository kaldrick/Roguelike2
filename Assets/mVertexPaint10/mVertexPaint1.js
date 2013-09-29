// mVertexPainter v1.0 - 20.08.2011 - mgear - http://unitycoder.com/blog
// 
// This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License.

// target object that we want to rotate & use for painting colors
var targetobj:Transform;

// attach this scrip to your camera
// add object to scene, with _mesh_ collider & rigidbody (no gravity)
// then click any face to paint it, pick colors from colorpicker gui

// mainloop
function Update () 
{

	// rotate object with A/D keys 
	if (Input.GetKey ("a"))
	{
		targetobj.transform.Rotate(Vector3(0,50,0) * Time.deltaTime);
	}
	if (Input.GetKey ("d"))
	{
		targetobj.transform.Rotate(Vector3(0,-50,0) * Time.deltaTime);
	}

	if (Input.GetKeyDown ("r")) // reset colors with R
	{
		var meshclear : Mesh = targetobj.GetComponent(MeshFilter).sharedMesh; // get meshobject
		var clearcolors : Color[] = new Color[meshclear.vertices.Length]; // create empty array
		meshclear.colors = clearcolors; // set empty array to mesh colors, so all will be black
	}

	// actual painting happens here, when left mousebutton is down
	if(Input.GetMouseButton(0))
	{
		// Only if we hit something, do we continue
		var hit : RaycastHit;
		if (!Physics.Raycast (camera.ScreenPointToRay(Input.mousePosition), hit))	return;
	
		// Just in case, make sure there is meshcollider
		var meshCollider = hit.collider as MeshCollider;
		if (meshCollider == null || meshCollider.sharedMesh == null)	return;
	
		// get mesh info (I guess could do this in start also)
		var mesh : Mesh = meshCollider.sharedMesh;
		var vertices = mesh.vertices;
		var triangles = mesh.triangles;
		var colors : Color[] = mesh.colors; // get current mesh colors to our colors array
	
		// get selected color from our colorpicker script
		var script : colorpicker;
		script = GetComponent("colorpicker");
		var mycolor = script.colorpick;
		
		// set colors based on which triangle vertices we hit (we set colors for all 3 vertices of the triangle, could check which vertex is closest to the hit and set that only
		colors[triangles[hit.triangleIndex * 3 + 0]] = mycolor;
		colors[triangles[hit.triangleIndex * 3 + 1]] = mycolor;
		colors[triangles[hit.triangleIndex * 3 + 2]] = mycolor;
		
		// set the colors to mesh
		mesh.colors = colors;

	}
}