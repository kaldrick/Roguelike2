@CustomEditor (IndieFX)
public class IndieFXEditor extends Editor {

	enum IFX {
		COLOR_BALANCE = 0,
		STAR_BLOOM = 1,		
		BLOOM = 2,
		BLUR = 3,
		MOTION_BLUR = 4
	}
	
    function OnInspectorGUI () {
		target.color_balance_shader = (target.color_balance_shader) ? target.color_balance_shader : Shader.Find("IndieFX/Shaders/FX ColorBalance");
		target.star_bloom_shader = (target.star_bloom_shader) ? target.star_bloom_shader : Shader.Find("IndieFX/Shaders/FX StarBloom");
		target.bloom_shader = (target.bloom_shader) ? target.bloom_shader : Shader.Find("IndieFX/Shaders/FX Bloom");
		target.blur_shader = (target.blur_shader) ? target.blur_shader : Shader.Find("IndieFX/Shaders/FX Blur");
		target.motion_blur_shader = (target.motion_blur_shader) ? target.motion_blur_shader : Shader.Find("IndieFX/Shaders/FX MotionBlur");
		
		if (target.Modules != null) {
			
			for (var fx : int = 0; fx < target.Modules.length; fx ++) {
				var ifx : IFX = target.Modules[fx];
				switch (ifx) {
				case IFX.COLOR_BALANCE:
					EditorGUILayout.BeginHorizontal();
					target.ColorBalanceEnabled = EditorGUILayout.Toggle(target.ColorBalanceEnabled, GUILayout.ExpandWidth(false), GUILayout.MinWidth(15), GUILayout.MaxWidth(15));
					target.colorBalanceOpts = EditorGUILayout.Foldout(target.colorBalanceOpts, "Color Balance:");
					EditorGUILayout.EndHorizontal();
					
					if (target.colorBalanceOpts) {
						target.Lift = EditorGUILayout.ColorField("Lift", target.Lift);
						target.LiftBright = EditorGUILayout.Slider("Brightness", target.LiftBright, 0f, 2f);
						EditorGUILayout.Space();
						
						target.Gamma = EditorGUILayout.ColorField("Gamma", target.Gamma);
						target.GammaBright = EditorGUILayout.Slider("Brightness", target.GammaBright, 0f, 2f);
						EditorGUILayout.Space();
						
						target.Gain = EditorGUILayout.ColorField("Gain", target.Gain);
						target.GainBright = EditorGUILayout.Slider("Brightness", target.GainBright, 0f, 2f);
						EditorGUILayout.Space();
						
						target.color_balance_shader = EditorGUILayout.ObjectField("Shader: ", target.color_balance_shader, Shader, false);
						EditorGUILayout.Space();
					}
					
					break;
				case IFX.BLOOM:
					EditorGUILayout.BeginHorizontal();
					target.BloomEnabled = EditorGUILayout.Toggle(target.BloomEnabled, GUILayout.ExpandWidth(false), GUILayout.MinWidth(15), GUILayout.MaxWidth(15));
					target.bloomOpts = EditorGUILayout.Foldout(target.bloomOpts, "Bloom:");
					EditorGUILayout.EndHorizontal();
					
					if (target.bloomOpts) {
						target.threshold_min_blm = EditorGUILayout.Slider("Threshold Min", target.threshold_min_blm, 0f, 1f);
						target.threshold_max_blm = EditorGUILayout.Slider("Threshold Max", target.threshold_max_blm, 0f, 1f);
						target.amount_blm = EditorGUILayout.Slider("Amount", target.amount_blm, 0f, 2f);
						EditorGUILayout.Space();
						
						target.bloom_shader = EditorGUILayout.ObjectField("Shader: ", target.bloom_shader, Shader, false);
						EditorGUILayout.Space();
					}
					
					break;
				case IFX.STAR_BLOOM:
					EditorGUILayout.BeginHorizontal();
					target.StarBloomEnabled = EditorGUILayout.Toggle(target.StarBloomEnabled, GUILayout.ExpandWidth(false), GUILayout.MinWidth(15), GUILayout.MaxWidth(15));
					target.starBloomOpts = EditorGUILayout.Foldout(target.starBloomOpts, "Star Bloom:");
					EditorGUILayout.EndHorizontal();
					
					if (target.starBloomOpts) {
						target.offset_str = EditorGUILayout.Slider("Offset", target.offset_str, 1f, 100f);
						target.threshold_min_str = EditorGUILayout.Slider("Threshold Min", target.threshold_min_str, 0f, 1f);
						target.threshold_max_str = EditorGUILayout.Slider("Threshold Max", target.threshold_max_str, 0f, 1f);
						target.amount_str = EditorGUILayout.Slider("Amount", target.amount_str, 0f, 2f);
						target.radius_str = EditorGUILayout.IntSlider("Radius", target.radius_str, 1, 100);
						EditorGUILayout.Space();
						
						target.star_bloom_shader = EditorGUILayout.ObjectField("Shader: ", target.star_bloom_shader, Shader, false);
						EditorGUILayout.Space();
					}
					
					break;
				case IFX.BLUR:
					EditorGUILayout.BeginHorizontal();
					target.BlurEnabled = EditorGUILayout.Toggle(target.BlurEnabled, GUILayout.ExpandWidth(false), GUILayout.MinWidth(15), GUILayout.MaxWidth(15));
					target.blurOpts = EditorGUILayout.Foldout(target.blurOpts, "Blur:");
					EditorGUILayout.EndHorizontal();
					
					if (target.blurOpts) {
						target.Passes = EditorGUILayout.IntSlider("Passes", target.Passes, 2 ,28);
						EditorGUILayout.Space();
						
						target.blur_shader = EditorGUILayout.ObjectField("Shader: ", target.blur_shader, Shader, false);
						EditorGUILayout.Space();
					}
					
					break;
				case IFX.MOTION_BLUR:
					EditorGUILayout.BeginHorizontal();
					target.MotionBlurEnabled = EditorGUILayout.Toggle(target.MotionBlurEnabled, GUILayout.ExpandWidth(false), GUILayout.MinWidth(15), GUILayout.MaxWidth(15));
					target.motionBlurOpts = EditorGUILayout.Foldout(target.motionBlurOpts, "Motion Blur:");
					EditorGUILayout.EndHorizontal();
					
					if (target.motionBlurOpts) {
						target.MBR_Amount = EditorGUILayout.Slider("Amount", target.MBR_Amount, 0f, 2f);
						target.Frames = EditorGUILayout.IntSlider("Frames", target.Frames, 1 ,20);
						EditorGUILayout.Space();
						
						target.motion_blur_shader = EditorGUILayout.ObjectField("Shader: ", target.motion_blur_shader, Shader, false);
						EditorGUILayout.Space();
					}
					break;
				}
			}
		}
		
        if (GUI.changed) { EditorUtility.SetDirty (target); }
    }
}
