#ifndef MOUTH_INCLUDED
#define MOUTH_INCLUDED

//include useful shader functions
#include "UnityCG.cginc"

//texture and transforms of the texture
sampler2D _MainTex;
float4 _MainTex_ST;

//tint of the texture
fixed4 _Color;

//control the Expression
float _ExpressionIndex;

//the object data that's put into the vertex shader
struct appdata{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

//the data that's used to generate fragments and can be read by the fragment shader
struct v2f{
    float4 position : SV_POSITION;
    float2 uv : TEXCOORD0;
};

//the vertex shader
v2f vert(appdata v){
    v2f o;
    //convert the vertex positions from object space to clip space so they can be rendered
    o.position = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    return o;
}

//the fragment shader
fixed4 frag(v2f i) : SV_TARGET{
    // sample the texture
    // TODO need to only modify u and v independently with uniforms
    fixed4 col = tex2D(_MainTex, float2(i.uv[0] * 2.0 + 1.0 * 0.0, i.uv[1] + 0.25 * _ExpressionIndex));
    // apply fog
    UNITY_APPLY_FOG(i.fogCoord, col);
    return col;
}

#endif