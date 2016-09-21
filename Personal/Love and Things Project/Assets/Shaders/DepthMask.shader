Shader "Masked/Mask" {//Use with a material shader to make this invisible and render before terrain
 
	SubShader {
		// Render the mask after regular geometry, but before masked geometry and
		// transparent things.
 
		Tags {"Queue" = "Geometry-110" }
		Lighting Off
		ZTest LEqual
 
		// Don't draw in the RGBA channels; just the depth buffer
 
		ColorMask 0
		ZWrite On
 
		// Do nothing specific in the pass:
 
		Pass {}
	}
}