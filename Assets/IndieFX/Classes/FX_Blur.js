public static class FX_Blur {
	
	public function run (mat : Material, rTex : Texture2D, Passes : int, lastMod : boolean) {
		rTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
		rTex.Apply();
		
		mat.mainTexture = rTex;
		
		GL.PushMatrix();
		for (var i = 0; i < Passes; i ++) {
			
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
			
			if (i < mat.passCount - 1 || !lastMod) {
				rTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
				rTex.Apply();
			}
		}
		GL.PopMatrix();
	}
	
}