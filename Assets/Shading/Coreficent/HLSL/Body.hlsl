#ifndef BODY_INCLUDED
#define BODY_INCLUDED

#include "AutoLight.cginc"
#include "Lighting.cginc"
#include "UnityCG.cginc"

float _ShadeDarkness;
float _ShadeThreshold;
float4 _MainTex_ST;
sampler2D _MainTex;

struct appdata
{
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
    float4 pos : POSITION;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float3 worldNormal : NORMAL;
    float4 pos : SV_POSITION;

    SHADOW_COORDS(4)
    UNITY_FOG_COORDS(5)
};

v2f vert(appdata v)
{
    v2f o;

    o.pos = UnityObjectToClipPos(v.pos);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.worldNormal = UnityObjectToWorldNormal(v.normal);
    TRANSFER_SHADOW(o);
    UNITY_TRANSFER_FOG(o, o.pos);

    return o;
}

fixed4 frag(v2f i) : SV_TARGET
{
    // calculate mirrored texture coordinates
    fixed4 col = tex2D(_MainTex, 1.0 - abs(frac(i.uv * 0.5) * 2.0 - 1.0));

    UNITY_APPLY_FOG(i.fogCoord, col);

    float shadow = SHADOW_ATTENUATION(i);

    // calculate the light intensity using dot product
    return col * (dot(_WorldSpaceLightPos0, normalize(i.worldNormal)) > _ShadeThreshold ? 1 : _ShadeDarkness) * shadow;
}

#endif