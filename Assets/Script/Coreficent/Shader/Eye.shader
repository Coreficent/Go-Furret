Shader "Coreficent/Eye" {
    //show values to edit in inspector
    Properties{
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0, 5.0)) = 1.0

        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader{
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

        //The first pass where we render the Object itself
        Pass{
            CGPROGRAM

            //include eye shader
            #include "../HLSL/Eye.hlsl"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag


            ENDCG
        }

        //The second pass where we render the outlines
        Pass{
            Cull front

            CGPROGRAM

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            //include outline shader
            #include "../HLSL/Outline.hlsl"

            ENDCG
        }
    }

    //fallback which adds stuff we didn't implement like shadows and meta passes
    FallBack "Standard"
}