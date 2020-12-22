#ifndef UNLIT_INCLUDED
#define UNLIT_INCLUDED

#include "UnityCG.cginc"

float4 _MainTex_ST;
sampler2D _MainTex;

struct appdata
{
    float2 uv : TEXCOORD0;
    float4 vertex : POSITION;
};

struct v2f
{
    UNITY_FOG_COORDS(1)
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

v2f vert (appdata v)
{
    v2f o;

    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    UNITY_TRANSFER_FOG(o,o.vertex);

    return o;
}

fixed4 frag (v2f i) : SV_Target
{
    fixed4 col = tex2D(_MainTex, i.uv);

    UNITY_APPLY_FOG(i.fogCoord, col);

    return col;
}

#endif