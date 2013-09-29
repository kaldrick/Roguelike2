using UnityEngine;
using System.Collections;
using LibNoise.Unity;
using LibNoise.Unity.Generator;
using LibNoise.Unity.Operator;

public class WorldGenerator : MonoBehaviour 
{
	//height map
	private Noise2D m_noiseMap = null;
	//color map
	private Noise2D m_noiseMap2 = null;
	//noise map resolution (always +1 to segment count)
	public int resolution = 193;
	//default noise type for height map
	public NoiseType noise = NoiseType.Voronoi;
	//mesh builder for world
	MeshBuilder meshBuilder = new MeshBuilder();
	//length of one tile
	public float m_Length =	 256f;
	//width of one tile 
	public float m_Width = 256f;
	public float m_Height = 64f;
	public float fHeight = 0f;
	public int m_SegmentCount = 64;
	//Height multiplies the final noise output
	public float Height = 10.0f;
	public GameObject tree;
	//This divides the noise frequency
	public float NoiseSize = 10.0f;
	public Color[] colors, colors2;
	public int[] iColors;
	private int iStep = 0;
	//Function that inputs the position and spits out a float value based on the perlin noise
	public float PerlinNoise(float x, float y)
	{
		//Generate a value from the given position, position is divided to make the noise more frequent.
		float noise = Mathf.PerlinNoise( x / NoiseSize, y / NoiseSize );
		
		//Return the noise value
		return noise * Height;;
		
	}
	public void Generate() {	
			float offset = Random.Range (0.0f, 3.0f);
			float offset2 = 1.0f;
			float zoom = 0.5f;
			float zoom2 = 0.3f;
            // Create the module network
            ModuleBase moduleBase, moduleBase2;
            switch(noise) {
	            case NoiseType.Billow:	
            	moduleBase = new Billow();
            	break;
            	
	            case NoiseType.RiggedMultifractal:	
				RiggedMultifractal rig = new RiggedMultifractal();
				rig.Seed = Random.Range (0, 65000);
            	moduleBase = rig;
            	break;   
            	
	            case NoiseType.Voronoi:	
            	moduleBase = new Voronoi();
            	break;             	         	
            	
              	case NoiseType.Mix:            	
            	Perlin perlin = new Perlin();
            	RiggedMultifractal rigged = new RiggedMultifractal();
            	moduleBase = new Add(perlin, rigged);
            	break;
            	
            	default:
            	moduleBase = new Perlin();
            	break;
            	
            }
			Voronoi voronoi = new Voronoi();
			voronoi.Seed = Random.Range (0, 65000);
			Billow rigged2 = new Billow();
			rigged2.Seed = Random.Range (0, 65000);
			moduleBase2 = new Add(voronoi, rigged2);
            // Initialize the noise map
            this.m_noiseMap = new Noise2D(resolution, resolution, moduleBase);
			this.m_noiseMap2 = new Noise2D(resolution, resolution, moduleBase2);
            this.m_noiseMap.GeneratePlanar(
            offset + -1 * 1/zoom, 
            offset + offset + 1 * 1/zoom, 
            offset + -1 * 1/zoom,
            offset + 1 * 1/zoom);
			this.m_noiseMap2.GeneratePlanar(
            offset2 + -1 * 1/zoom2, 
            offset2 + offset2 + 1 * 1/zoom2, 
            offset2 + -1 * 1/zoom2,
            offset2 + 1 * 1/zoom2);
			colors = this.m_noiseMap.GetColors(LibNoise.Unity.Gradient.Grayscale);
			colors2 = this.m_noiseMap2.GetColors(LibNoise.Unity.Gradient.Test);
			iColors = new int[colors.Length];
			
			//the textures
            /*this.m_textures[0] = this.m_noiseMap.GetTexture(LibNoise.Unity.Gradient.Grayscale);
            this.m_textures[0].Apply();
            
			
            this.m_textures[1] = this.m_noiseMap.GetTexture(LibNoise.Unity.Gradient.Terrain);
            this.m_textures[1].Apply();
             
            this.m_textures[2] = this.m_noiseMap.GetNormalMap(3.0f);
			 this.m_textures[2].Apply();
			 
			 //display on plane
			 renderer.material.mainTexture = m_textures[0];
            

            //write images to disk
            //File.WriteAllBytes(Application.dataPath + "/../Gray.png", m_textures[0].EncodeToPNG() );
           // File.WriteAllBytes(Application.dataPath + "/../Terrain.png", m_textures[1].EncodeToPNG() );
           // File.WriteAllBytes(Application.dataPath + "/../Normal.png", m_textures[2].EncodeToPNG() );
            
            Debug.Log("Wrote Textures out to "+Application.dataPath + "/../");*/
            
        
    }
	void Start()
	{
		BuildMesh();
	}
	void BuildMesh()
	{
		Vector3 offset;
		Generate ();
		for (int i = 0; i <= m_SegmentCount; i++)
		{
			float z = m_Length * i;
			float v = (1.0f / m_SegmentCount) * i;
			for (int j = 0; j <= m_SegmentCount; j++)
			{
				Random.seed = Random.Range (0, 36500);
				float u = (1.0f / m_SegmentCount) * j;
				float x = m_Width * j;
			//	NoiseSize = Random.Range (10f, 50f);
			//	Height = Random.Range (0f, 10f);
				//fHeight = PerlinNoise( (x+Time.timeSinceLevelLoad) * Random.Range (0.5f, 1.5f), (z+Time.timeSinceLevelLoad) * Random.Range (0.5f, 1.5f));
				//float randomNoise = noise.FractalNoise2D(x, z, 8, 0.25f, 0.8f)* 10;
				//Debug.Log ("fHeight: " + fHeight + "Noise2d" + noise.FractalNoise2D (x, z, 8, Random.Range (0.115f, 0.25f), Random.Range (0.3f, 0.8f)));
				//Debug.Log (fHeight);
				//Debug.Log (	Mathf.PerlinNoise (x + Random.Range (80, 500), z + Random.Range (80, 500)));
				
				//fHeight = PerlinNoise( x+Time.timeSinceLevelLoad, z+Time.timeSinceLevelLoad);
			//	if(colors2[i*m_SegmentCount + j] == Color.red)
			//	{
			//		offset = new Vector3(x, 4086, z);
			//	}
			//	else
			//	{
				//	offset = new Vector3(x, 0, z);
			//	}
				offset = new Vector3(x, (colors[i*m_SegmentCount + j].r + colors[i*m_SegmentCount + j].g + colors[i*m_SegmentCount + j].b) * 16, z);
				
				//Debug.Log ((colors[i*m_SegmentCount + j].r + colors[i*m_SegmentCount + j].g + colors[i*m_SegmentCount + j].b) * 500);
		//		colors[i*m_SegmentCount + j] = new Color(Mathf.PerlinNoise (x,z), Mathf.PerlinNoise (x,z), Mathf.PerlinNoise (x,z), 0.0f);
				//colors[i*m_SegmentCount + j] = new Color(noise.FractalNoise3D(x, z, fHeight, 8, Random.Range (0.115f, 0.25f), Random.Range (0.3f, 0.8f)), 10.0f, 10.0f, 0.0f);
				Vector2 uv = new Vector2(u, v);
				bool buildTriangles = i > 0 && j > 0;
				BuildQuadForGrid(meshBuilder, offset, uv, buildTriangles, m_SegmentCount + 1);
			}
		}	
		//Create the mesh:
		MeshFilter filter = GetComponent<MeshFilter>();
		MeshCollider col = GetComponent<MeshCollider>();
		Mesh mesh = meshBuilder.CreateMesh ();
//		mesh.colors = colors;
		mesh.RecalculateNormals ();
		meshBuilder.TangentSolver (mesh);

		mesh.colors = colors2;
		col.sharedMesh = mesh;
		Debug.Log (mesh.vertices.Length);
		
		
		if (filter != null)
		{
			filter.sharedMesh = mesh;
			//renderer.material.SetVector("_CentrePoint", transform.position);
			renderer.enabled = true; 
		}
	}
	void BuildQuadForGrid(MeshBuilder meshBuilder, Vector3 position, Vector2 uv, bool buildTriangles, int vertsPerRow)
	{
		
		meshBuilder.Vertices.Add(position);
		if(meshBuilder.Vertices.Count != 0)
		{
		//	position += new Vector3(0, colors2[meshBuilder.Vertices.Count -1].r * 4096 + colors2[meshBuilder.Vertices.Count -1].g * 512 - colors2[meshBuilder.Vertices.Count -1].b * 1024, 0);	
			if(colors2[meshBuilder.Vertices.Count -1].g> 0.7f && colors2[meshBuilder.Vertices.Count -1].r < 0.5)
			{
				//Debug.Log ("Dzialam?1");
				if(iStep == 0 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					lookTree.transform.parent = GameObject.Find ("Trees").transform;
					iStep++;
				}
				if(iStep == 1 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees2").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 2 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees3").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 3 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees4").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 4 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees5").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 5 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees6").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 6 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees7").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 7 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees8").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 8 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees9").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 9 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees10").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 10 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees11").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 11 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees12").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 12 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees13").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 13 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees14").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 14 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees15").transform;
					iStep++;
				}
				if(iStep == 15 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees16").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 16 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees17").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 17 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees18").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 18 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees19").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep++;
				}
				if(iStep == 19 && (Random.Range (1, 6) == 3))
				{
				//	Debug.Log ("Dzialam?2");
					GameObject lookTree = GameObject.Instantiate (tree,position - new Vector3(0, 80, 0), tree.transform.rotation) as GameObject;
					lookTree.transform.parent = GameObject.Find ("Trees20").transform;
					lookTree.transform.Rotate (new Vector3(0, Random.Range (0f, 360f), 0));
					iStep = 0;
				}
			}
		}
		meshBuilder.UVs.Add(uv);
		
		if (buildTriangles)
		{
			int baseIndex = meshBuilder.Vertices.Count - 1;
	
			int index0 = baseIndex;
			int index1 = baseIndex - 1;
			int index2 = baseIndex - vertsPerRow;
			int index3 = baseIndex - vertsPerRow - 1;
	
			meshBuilder.AddTriangle(index0, index2, index1);
			meshBuilder.AddTriangle(index2, index3, index1);
		}
	}
}
