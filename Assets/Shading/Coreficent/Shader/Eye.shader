Shader "Coreficent/Eye"
{
    Properties
    {
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineDarkness ("Outline Darkness", Range(0.0, 2.0)) = 0.0
        _OutlineThickness ("Outline Thickness", Range(0.0, 2.0)) = 1.0
        _ShadeDarkness ("Shade Darkness", Range(0.0, 1.0)) = 0.5
        _ShadeThreshold ("Shade Threshold", Range(-1.0, 1.0)) = 0.0
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
