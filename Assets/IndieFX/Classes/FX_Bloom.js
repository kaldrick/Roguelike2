public static class FX_Bloom {
	
	public function run (mat : Material, rTex : Texture2D, bloomTex : Texture2D) {
		rTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
		rTex.Apply();
		
		bloomTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
		bloomTex.Apply();
		
		mat.SetTexture("_BlurTex", bloomTex);
		GL.PushMatrix();
		for (var i = 0; i < mat.passCount; i ++) {
		
			if (i == mat.passCount - 1) {
				mat.SetTexture("_MainTex", rTex);
			}
			
			mat.SetPass(i);
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
			
			if (i < mat.passCount - 1) {
				bloomTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
				bloomTex.Apply();
			}
		}
		GL.PopMatrix();
	}
	
}