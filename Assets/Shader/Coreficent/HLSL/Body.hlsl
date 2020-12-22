#ifndef BODY_INCLUDED
#define BODY_INCLUDED

//include useful shader functions
#include "UnityCG.cginc"

//texture and transforms of the texture
sampler2D _MainTex;
float4 _MainTex_ST;

//tint of the texture
fixed4 _Color;

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
    float2 uv = 1.0 - abs(frac(i.uv * 0.5) * 2.0 - 1.0);
    fixed4 col = tex2D(_MainTex, uv);
    return col;
}

#endif