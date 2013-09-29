// color picker source from : http://answers.unity3d.com/questions/49779/i-am-trying-to-make-this-color-picker-script-work.html
// assign your colorpicker texture to this variable
var colorPicker : Texture2D;
var colorpick:Color; // picked color
var guitext:GUIText; // displays selected color

function OnGUI() {    
    if (GUI.RepeatButton (Rect (0,0,256,256), colorPicker)) 
	{
        var pickpos : Vector2 = Event.current.mousePosition;
        var pixelPosX : float = pickpos.x+3;
        var pixelPosY : float = 256-pickpos.y-1;
        var col : Color = colorPicker.GetPixel(pixelPosX,pixelPosY);
		colorpick = col;
		
		guitext.material.color = col;
		guitext.text = guitext.material.color.ToString();
    }
	
}