#ifndef BODY_INCLUDED
#define BODY_INCLUDED

#include "UnityCG.cginc"

fixed4 _Color;
float _ShadeDarkness;
float4 _MainTex_ST;
sampler2D _MainTex;

struct appdata
{
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
    float4 vertex : POSITION;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float3 worldNormal : NORMAL;
    float4 position : SV_POSITION;
};

v2f vert(appdata v)
{
    v2f o;

    o.position = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.worldNormal = UnityObjectToWorldNormal(v.normal);

    return o;
}

fixed4 frag(v2f i) : SV_TARGET
{
    // calculate mirrored texture coordinates
    fixed4 col = tex2D(_MainTex, 1.0 - abs(frac(i.uv * 0.5) * 2.0 - 1.0));

    UNITY_APPLY_FOG(i.fogCoord, col);

    // calculate the light intensity using dot product
    return col * (dot(_WorldSpaceLightPos0, normalize(i.worldNormal)) > 0 ? 1 : _ShadeDarkness);
}

#endif