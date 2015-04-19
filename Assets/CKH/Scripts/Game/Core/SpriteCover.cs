using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utils;

[ExecuteInEditMode]
public class SpriteCover : MonoBehaviour {

    private static IDictionary<int, Sprite[]> spriteCache = new Dictionary<int, Sprite[]>();

    [SerializeField]
    public Material material;

    public Material Material
    {
        get { return material; }
        set
        {
            if (value == null || value == material)
            {
                return;
            }

            material = value;
            SetSliceMaterials();
        }
    } 

    [SerializeField]
	private Sprite sprite;

    public Sprite Sprite
    {
        get { return sprite; }
        set
        {
            if (value == null || value == sprite)
            {
                return;
            }

            sprite = value;
            CreateSliceSprites();
        }
    }

	[SerializeField]
	private Color color;

	public Color Color
	{
		get { return color; }
		set
		{
			if (value == null || value == color)
			{
				return;
			}
			
			color = value;
			PaintSliceSprites();
		}
	}

	private IDictionary<string, Transform> slices;
    private Transform meshObject;

    [Range(0, 1)]
    public float topMargin = 0.3f;

    [Range(0, 1)]
    public float bottomMargin = 0.3f;

    [Range(0, 1)]
    public float leftMargin = 0.3f;
    
    [Range(0, 1)]
    public float rightMargin = 0.3f;

    private void Awake()
    {
        meshObject = transform.parent.FindChild("Mesh");

#if UNITY_EDITOR
        slices = new Dictionary<string, Transform>();

        CreateSlices(); 
#endif
        CreateSliceSprites();
        SetSliceMaterials();
        UpdateSlices();
    }

    private void CreateSlices()
    {
        slices.Clear();

        while (transform.childCount > 0)
        {
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }

        var keys = new[] { "tr", "tl", "br", "bl", "t", "r", "b", "l", "c" };

        foreach (var key in keys)
        {
            var go = new GameObject(key);
            var value = go.transform;

            slices.Add(key, value);

            value.parent = transform;
            value.localScale = Vector3.one;
            go.AddComponent<SpriteRenderer>();
        }
    }

    private int GetSpriteSetHashCode(Sprite sprite, float top, float right, float bottom, float left)
    {
        return string.Format("{0}.{1}.{2}.{3}.{4}", sprite.GetInstanceID(), top, right, bottom, left).GetHashCode();
    }

    private void CreateSliceSprites()
    {
		int hash = 0;

#if UNITY_EDITOR
		if(Application.isPlaying) {
#endif

        hash = GetSpriteSetHashCode(sprite, topMargin, rightMargin, bottomMargin, leftMargin);
        if (spriteCache.ContainsKey(hash))
        {
            var cache = spriteCache[hash];

            slices["tr"].GetComponent<SpriteRenderer>().sprite = cache[0];
            slices["br"].GetComponent<SpriteRenderer>().sprite = cache[1];
            slices["tl"].GetComponent<SpriteRenderer>().sprite = cache[2];
            slices["bl"].GetComponent<SpriteRenderer>().sprite = cache[3];
            slices["t"].GetComponent<SpriteRenderer>().sprite = cache[4];
            slices["l"].GetComponent<SpriteRenderer>().sprite = cache[5];
            slices["b"].GetComponent<SpriteRenderer>().sprite = cache[6];
            slices["r"].GetComponent<SpriteRenderer>().sprite = cache[7];
            slices["c"].GetComponent<SpriteRenderer>().sprite = cache[8];

            return;
        }

#if UNITY_EDITOR
		}
#endif

        SpriteRenderer sr;
        Rect rect;

        var size = new Vector2(sprite.texture.width, sprite.texture.height);
        var bodyWidth = 1 - (leftMargin + rightMargin);
        var bodyHeight = 1 - (topMargin + bottomMargin);

        // corners
        sr = slices["tr"].GetComponent<SpriteRenderer>();
        rect = new Rect(size.x * (1 - rightMargin), size.y * (1 - topMargin), size.x * rightMargin, size.y * topMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(1, 1), 1000);

        sr = slices["br"].GetComponent<SpriteRenderer>();
        rect = new Rect(size.x * (1 - rightMargin), 0, size.x * rightMargin, size.y * bottomMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(1, 0), 1000);

        sr = slices["tl"].GetComponent<SpriteRenderer>();
        rect = new Rect(0, size.y * (1 - topMargin), size.x * leftMargin, size.y * topMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(0, 1), 1000);

        sr = slices["bl"].GetComponent<SpriteRenderer>();
        rect = new Rect(0, 0, size.x * leftMargin, size.y * bottomMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(0, 0), 1000);

        sr = slices["t"].GetComponent<SpriteRenderer>();
        rect = new Rect(size.x * leftMargin, size.y * (1 - topMargin), size.x * bodyWidth, size.y * topMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(.5f, 1), 1000);

        sr = slices["r"].GetComponent<SpriteRenderer>();
        rect = new Rect(size.x * (1 - rightMargin), size.y * bottomMargin, size.x * rightMargin, size.y * bodyHeight);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(1, .5f), 1000);

        sr = slices["b"].GetComponent<SpriteRenderer>();
        rect = new Rect(size.x * leftMargin, 0, size.x * bodyWidth, size.y * bottomMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(.5f, 0), 1000);

        sr = slices["l"].GetComponent<SpriteRenderer>();
        rect = new Rect(0, size.y * bottomMargin, size.x * leftMargin, size.y * bodyHeight);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(0, .5f), 1000);

        // center
        sr = slices["c"].GetComponent<SpriteRenderer>();
        rect = new Rect(size.x * leftMargin, size.y * bottomMargin, size.x * bodyWidth, size.y * bodyHeight);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(.5f, .5f), 1000);

#if UNITY_EDITOR
		if(Application.isPlaying) {
#endif
        
		var newCache = new [] {
            slices["tr"].GetComponent<SpriteRenderer>().sprite,
            slices["br"].GetComponent<SpriteRenderer>().sprite,
            slices["tl"].GetComponent<SpriteRenderer>().sprite,
            slices["bl"].GetComponent<SpriteRenderer>().sprite,
            slices["t"].GetComponent<SpriteRenderer>().sprite,
            slices["l"].GetComponent<SpriteRenderer>().sprite,
            slices["b"].GetComponent<SpriteRenderer>().sprite,
            slices["r"].GetComponent<SpriteRenderer>().sprite,
            slices["c"].GetComponent<SpriteRenderer>().sprite
        };
			
			spriteCache.Add(hash, newCache);

			Debug.Log("created new cache entry " + sprite.GetInstanceID() + " " + topMargin + " "+leftMargin+" "+bottomMargin+" "+rightMargin);
#if UNITY_EDITOR
		}
#endif
    }

	private void PaintSliceSprites()
	{
		foreach (var slice in slices.Values) {
			slice.GetComponent<SpriteRenderer>().color = color;
		}
	}

    private void SetSliceMaterials()
    {
        foreach (var slice in slices.Values)
        {
            slice.GetComponent<SpriteRenderer>().material = material;
        }
    }

    private void UpdateSlices()
    {
        var mesh = meshObject.GetComponent<MeshFilter>().sharedMesh;

        var bounds = mesh.bounds;
        bounds.size = Vector3.Scale(bounds.size, meshObject.transform.localScale);
        
        var bodyWidth = 1 - (leftMargin + rightMargin);
        var bodyHeight = 1 - (topMargin + bottomMargin);

        // corners dont scale, we update their position only
        slices["tr"].localPosition = new Vector3(bounds.max.x, bounds.max.y, -1);
        slices["br"].localPosition = new Vector3(bounds.max.x, bounds.min.y, -1);
        slices["tl"].localPosition = new Vector3(bounds.min.x, bounds.max.y, -1);
        slices["bl"].localPosition = new Vector3(bounds.min.x, bounds.min.y, -1);

        // sides
        slices["t"].transform.localPosition = new Vector3((leftMargin - rightMargin) / 2, bounds.max.y, -1);
        slices["t"].transform.localScale = new Vector3((meshObject.localScale.x - (leftMargin + rightMargin)) * (1 / bodyWidth), 1, 1);

        slices["r"].transform.localPosition = new Vector3(bounds.max.x, (bottomMargin - topMargin) / 2, -1);
        slices["r"].transform.localScale = new Vector3(1, (meshObject.localScale.y - (topMargin + bottomMargin)) * (1 / bodyHeight), 1);

        slices["b"].transform.localPosition = new Vector3((leftMargin - rightMargin) / 2, bounds.min.y, -1);
        slices["b"].transform.localScale = new Vector3((meshObject.localScale.x - (leftMargin + rightMargin)) * (1 / bodyWidth), 1, 1);

        slices["l"].transform.localPosition = new Vector3(bounds.min.x, (bottomMargin - topMargin) / 2, -1);
        slices["l"].transform.localScale = new Vector3(1, (meshObject.localScale.y - (topMargin + bottomMargin)) * (1 / bodyHeight), 1);

        // center
        slices["c"].transform.localPosition = new Vector3((leftMargin - rightMargin) / 2, (bottomMargin - topMargin) / 2, -1);
        slices["c"].transform.localScale = new Vector3((meshObject.localScale.x - (leftMargin + rightMargin)) * (1 / bodyWidth), (meshObject.localScale.y - (topMargin + bottomMargin)) * (1 / bodyHeight), 1);
    }


#if UNITY_EDITOR
    Sprite lastSprite;
    Material lastMaterial;
    int marginHash;
#endif

    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            if (meshObject == null)
            {
                Awake();
            }

            if (lastMaterial != material)
            {
                lastMaterial = material;
                SetSliceMaterials();
            }

            if (lastSprite != sprite)
            {
                lastSprite = sprite;
                CreateSliceSprites();
            }

            var hash = string.Format("{0}|{1}|{2}|{3}", topMargin, rightMargin, bottomMargin, leftMargin).GetHashCode();
            if (hash != marginHash)
            {
                marginHash = hash;
                CreateSliceSprites();
            }

            UpdateSlices();
        }
#endif
    }

}
