#ifndef OUTLINE_INCLUDED
#define OUTLINE_INCLUDED

#include "UnityCG.cginc"

float _OutlineDarkness;
float _OutlineThickness;
float4 _MainTex_ST;
sampler2D _MainTex;

struct appdata
{
    float2 uv : TEXCOORD0;
    #ifdef USE_PRECALCULATED_NORMAL
        float3 normal : TEXCOORD1;
    #else
        float3 normal : NORMAL;
    #endif
    float4 vertex : POSITION;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 pos : SV_POSITION;
};

v2f vert(appdata v)
{
    v2f o;

    o.pos = UnityObjectToClipPos(normalize(v.normal) * _OutlineThickness + v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    return o;
}

fixed4 frag(v2f i) : SV_TARGET
{
    fixed4 col = tex2D(_MainTex, i.uv);

    UNITY_APPLY_FOG(i.fogCoord, col);

    return col * _OutlineDarkness;
}

#endif