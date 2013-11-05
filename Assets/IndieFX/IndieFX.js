private static var rTex : Texture2D;
private static var Tex2 : Texture2D;
private static var rTexs : Texture2D[];
private var Mat : Material;

enum IFX {
	COLOR_BALANCE = 0,
	STAR_BLOOM = 1,		
	BLOOM = 2,
	BLUR = 3,
	MOTION_BLUR = 4
}

@SerializeField
public var Modules : IFX[] = [ IFX.STAR_BLOOM, IFX.BLOOM, IFX.MOTION_BLUR, IFX.BLUR, IFX.COLOR_BALANCE ];

//Shaders
@SerializeField
public var color_balance_shader : Shader;
@SerializeField
public var star_bloom_shader : Shader;
@SerializeField
public var bloom_shader : Shader;
@SerializeField
public var blur_shader : Shader;
@SerializeField
public var motion_blur_shader : Shader;


//Color Balance settings
public var Lift : Color = Color(1.0, 1.0, 1.0, 1.0);
public var LiftBright : float = 1.0;
public var Gamma : Color = Color(1.0, 1.0, 1.0, 1.0);
public var GammaBright : float = 1.0;
public var Gain : Color = Color(1.0, 1.0, 1.0, 1.0);
public var GainBright : float = 1.0;
@SerializeField
public var ColorBalanceEnabled = false;

//Star Bloom settings
public var offset_str : float = 1.0f;
//public var threshold_str : float = 0.85f;
public var threshold_min_str : float = 0.9f;
public var threshold_max_str : float = 1f;
public var amount_str : float = 1.0f;
public var radius_str : float = 7f;
@SerializeField
public var StarBloomEnabled = false;

//Bloom settings
//public var threshold_blm : float = 0.8f;
public var threshold_min_blm : float = 0.8f;
public var threshold_max_blm : float = 1f;
public var amount_blm : float = 0.746f;
@SerializeField
public var BloomEnabled = false;


//Blur settings
public var Passes : int = 4;
@SerializeField
public var BlurEnabled = false;

//Motion Blur settings
public var MBR_Amount : float = 0.362f;
public var Frames : int = 7;
private var CurrentFrame : int = 0;
@SerializeField
public var MotionBlurEnabled = false;
private var frames : int;

//Editor settings
@SerializeField
private var colorBalanceOpts : boolean = false;
@SerializeField
private var bloomOpts : boolean = false;
@SerializeField
private var starBloomOpts : boolean = false;
@SerializeField
private var blurOpts : boolean = false;
@SerializeField
private var motionBlurOpts : boolean = false;
@SerializeField
private var shaderOpts : boolean = false;

function Start () {
	rTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
	Mat = new Material (color_balance_shader);
	
	Tex2 = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
	
	if (MotionBlurEnabled) {
		EnableMotionBlur();
	}
}

public function EnableMotionBlur() {
	frames = Frames;
	Mat.shader = motion_blur_shader;
	if (Frames <= 0) {
		Frames = 1;
	} else if (Frames > Mat.passCount) {
		Frames = Mat.passCount;
	}
	
	rTexs = new Texture2D[Frames];
	for (var f : int = 0; f < Frames; f ++) {
		rTexs[f] = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
	}
	
	MotionBlurEnabled = true;
}

public function DisableMotionBlur() {
	rTexs = new Texture2D[0];
	
	MotionBlurEnabled = false;
}

function OnPostRender() {
	
	for (var mod : int; mod < Modules.length; mod ++) {
		switch (Modules[mod]) {
		case IFX.COLOR_BALANCE:
			if (ColorBalanceEnabled) {
				Mat.shader = color_balance_shader;
				Mat.SetColor("_Lift", Lift);
				Mat.SetFloat("_LiftB", Mathf.Clamp(LiftBright, 0.0, 2.0));
				Mat.SetColor("_Gamma", Gamma);
				Mat.SetFloat("_GammaB", Mathf.Clamp(GammaBright, 0.0, 2.0));
				Mat.SetColor("_Gain", Gain);
				Mat.SetFloat("_GainB", Mathf.Clamp(GainBright, 0.0, 2.0));
				
				FX_ColorBalance.run(Mat, rTex);
			}
			break;
		case IFX.BLOOM:
			if (BloomEnabled) {
				Mat.shader = bloom_shader;
				Mat.SetFloat("_ThresholdMin", threshold_min_blm);
				Mat.SetFloat("_ThresholdMax", threshold_max_blm);
				Mat.SetFloat("_Amount", Mathf.Clamp(amount_blm, 0.0f, 10.0f));
				
				FX_Bloom.run(Mat, rTex, Tex2);
			}
			break;
		case IFX.STAR_BLOOM:
			if (StarBloomEnabled) {
				Mat.shader = star_bloom_shader;
				Mat.SetFloat("_ThresholdMin", threshold_min_str);
				Mat.SetFloat("_ThresholdMax", threshold_max_str);
				if (radius_str > Mat.passCount - 2) { radius_str = Mat.passCount - 2; }
				if (radius_str <= 0) { radius_str = 1; }
				
				FX_StarBloom.run(Mat, rTex, Tex2, offset_str, amount_str, radius_str);
			}
			break;
		case IFX.BLUR:
			if (BlurEnabled) {
				Mat.shader = blur_shader;
				FX_Blur.run(Mat, rTex, Passes, (mod == Modules.length - 1));
			}
			break;
		case IFX.MOTION_BLUR:
			if (MotionBlurEnabled) {
				Mat.shader = motion_blur_shader;
				FX_MotionBlur.run(Mat, rTexs, rTex, frames, CurrentFrame, MBR_Amount);
				CurrentFrame = (CurrentFrame < frames - 1) ? CurrentFrame + 1 : 0;
			}
			break;
		}
	}
	
	
}