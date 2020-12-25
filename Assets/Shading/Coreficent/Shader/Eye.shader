Shader "Coreficent/Eye"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineDarkness ("Outline Darkness", Range(0.0, 1.0)) = 0.0
        _OutlineThickness ("Outline Thickness", Range(0.0, 1.0)) = 1.0
        _ShadingDarkness ("Shading Darkness", Range(0.0, 1.0)) = 0.5
        _ShadowThreshold ("Shadow Threshold", Range(0.0, 1.0)) = 0.5
        _ShadeThreshold ("Shade Threshold", Range(0.0, 1.0)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
            "LightMode" = "ForwardBase"
	        "PassFlags" = "OnlyDirectional"
        }

        // eye shading
        Pass
        {
            CGPROGRAM

            #include "../HLSL/Eye.hlsl"

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            ENDCG
        }

        // outline shading
        Pass
        {
            Cull front
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "../HLSL/Outline.hlsl"

            ENDCG
        }
    }

    FallBack "Standard"
}
