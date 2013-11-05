public static class FX_ColorBalance {
	
	public function run (mat : Material, rTex : Texture2D) {
		rTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
		rTex.Apply();
		
		mat.SetTexture("_MainTex", rTex);
		
		GL.PushMatrix();
		
		mat.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(GL.QUADS);
		GL.Color(Color(1,1,1,1));
		GL.MultiTexCoord(0,Vector3(0,0,0));
		GL.Vertex3(0,0,0);
		GL.MultiTexCoord(0,Vector3(0,1,0));
		GL.Vertex3(0,1,0);
		GL.MultiTexCoord(0,Vector3(1,1,0));
		GL.Vertex3(1,1,0);
		GL.MultiTexCoord(0,Vector3(1,0,0));
		GL.Vertex3(1,0,0);
		GL.End();
		
		GL.PopMatrix();
	}
	
}