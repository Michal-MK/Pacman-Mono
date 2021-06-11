sampler s0;
float time;

float4 MainPS(float2 coords : TEXCOORD0) : COLOR {
	float PI = 3.14159;
	float4 color = tex2D(s0, coords);
	color.r *= sin(time * PI / 3 * 0.01f) * 0.4f + 0.6f;
	color.g *= sin(time * 0.005f) * 0.4f + 0.6f;
	color.b *= sin(time + 2 * PI / 3 * 0.015f) * 0.4f + 0.6f;
	return color;
}

technique BasicColorDrawing {
	pass P0 {
		PixelShader = compile ps_3_0 MainPS();
		AlphaBlendEnable = TRUE;
		DestBlend = INVSRCALPHA;
		SrcBlend = SRCALPHA;
	}
};