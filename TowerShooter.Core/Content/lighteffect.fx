sampler s0;
texture lightMask;
sampler lightSampler=sampler_state {
  Texture=<lightMask>;
};

float4 PixelShaderLight(float4 pos: SV_POSITION, float4 color: COLOR0, float2 coords: TEXCOORD0): SV_TARGET0 {
  color=tex2D(s0, coords);
  float4 lightColor=tex2D(lightSampler, coords);
  return color * lightColor;
}
technique Technique1 {
  pass P0 {
    PixelShader=compile ps_4_0_level_9_1 PixelShaderLight();
  }
}