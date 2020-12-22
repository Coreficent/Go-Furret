#ifndef BODY_INCLUDED
#define BODY_INCLUDED

#include "UnityCG.cginc"

fixed4 _Color;
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
    fixed4 col = tex2D(_MainTex, 1.0 - abs(frac(i.uv * 0.5) * 2.0 - 1.0));

    UNITY_APPLY_FOG(i.fogCoord, col);

    return col;
}

#endif