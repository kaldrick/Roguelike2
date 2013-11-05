public static class FX_StarBloom {
	
	public function run (mat : Material, rTex : Texture2D, bloomTex : Texture2D, offset : float, amount : float, radius : float) {
		
		rTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
		rTex.Apply();
		
		bloomTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
		bloomTex.Apply();
		
		mat.SetTexture("_MainTex", rTex);
		mat.SetTexture("_BlurTex", bloomTex);
		
		var tmpOffset : float = Mathf.Clamp(offset, 0.0f, 10.0f);
		mat.SetFloat("_OffsetScale", tmpOffset);
		
		var tmpAmount : float = Mathf.Clamp(amount, 0.0f, 10.0f);
		mat.SetFloat("_Amount", tmpAmount);
		
		GL.PushMatrix();
		for (var i = 0; i < radius + 2; i ++) {
			
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
			
			if (i == 0) {
				bloomTex.ReadPixels(Rect(0,0,Screen.width,Screen.height), 0, 0);
				bloomTex.Apply();
				mat.SetTexture("_BlurTex", bloomTex);
			} else if (i > 1) {
				tmpOffset += offset;
				mat.SetFloat("_OffsetScale", tmpOffset);
				if (i > 2) {
					tmpAmount -= amount / radius;
					mat.SetFloat("_Amount", tmpAmount);
				}
			}
		}
		
		GL.PopMatrix();
	}
	
}