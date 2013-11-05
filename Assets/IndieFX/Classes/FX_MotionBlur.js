public static class FX_MotionBlur {
	
	public function run (mat : Material, rTex : Texture2D[], lstTex : Texture2D, Passes : int, Frame : int, amount : float) {
		rTex[Frame].ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
		rTex[Frame].Apply();
		
		GL.PushMatrix();
		var frameCount : int = 0;
		var currentFrame : int = Frame;
		while (frameCount < Passes) {
			mat.mainTexture = rTex[currentFrame];
			var fmc : float = frameCount;
			var ps : float = Passes;
			mat.SetColor("_Color", Color(1f, 1f, 1f, (1f - (fmc / ps)) * amount));
			
			mat.SetPass(frameCount);
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
			
			currentFrame = (currentFrame > 0) ? currentFrame - 1 : Passes - 1;
			frameCount ++;
		}
		GL.PopMatrix();
	}
	
}