using TMPro;
using UnityEngine;

public class LineComponent : Element
{
    [SerializeField] private TextMeshPro ValueField;

    public GameObject StartObject;
    public GameObject EndObject;

    public override void Start()
    {
        var model = TwinApplication.GetModel<WorkspaceModel>();

        base.Start();
        var textObject = Instantiate(model.LinkTextFieldPrefab);
        textObject.transform.parent = transform.parent;
        textObject.transform.position = transform.position;
        textObject.transform.position += new Vector3(Random.Range(0,1), 0.1f, Random.Range(0, 1));

        ValueField = textObject.GetComponent<TextMeshPro>();
    }

    public void Draw(Transform parent)
    {
        var start = StartObject.transform.position;
        var end = EndObject.transform.position;

        var middlePosition = (start + end) * 0.5f;
        transform.parent = parent;
        transform.localPosition = middlePosition;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        transform.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(start, end));
        transform.position = middlePosition; //placebond here
        transform.LookAt(end);
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        name = $"line |{start}| to |{end}|";
    }

    public override void OnMouseDown()
    {
        var end = EndObject.gameObject.GetComponent<ItemComponent>();
        StartObject.GetComponent<ItemComponent>().Outputs.Remove(end);
        Destroy(ValueField.gameObject);
        Destroy(gameObject);
    }

    public void UpdateText(float value)
    {
        ValueField.text = $"{value}";
    }
}
