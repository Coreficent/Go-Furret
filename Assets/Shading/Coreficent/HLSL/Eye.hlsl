#ifndef EYE_INCLUDED
#define EYE_INCLUDED

#include "UnityCG.cginc"

float4 _MainTex_ST;
sampler2D _MainTex;

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float4 position : SV_POSITION;
    float2 uv : TEXCOORD0;
};

v2f vert(appdata v)
{
    v2f o;

    o.position = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    return o;
}

fixed4 frag(v2f i) : SV_TARGET
{
    fixed4 col = tex2D(_MainTex, float2(i.uv[0] * 2.0, i.uv[1]));

    UNITY_APPLY_FOG(i.fogCoord, col);

    return col;
}

#endif