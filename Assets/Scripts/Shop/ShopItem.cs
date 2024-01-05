/// <summary>
/// Shop Item 实体类
/// </summary>
public class ShopItem{
    private string speed;
    private string rotate;
    private string model;
    private string price;
    private string id;

    public ShopItem(string speed, string rotate, string model, string price, string id)
    {
        this.speed = speed;
        this.rotate = rotate;
        this.model = model;
        this.price = price;
        this.id = id;
    }

    public string Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public string Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }

    public string Model
    {
        get { return model; }
        set { model = value; }
    }

    public string Price
    {
        get { return price; }
        set { price = value; }
    }

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    /// <summary>
    /// out put what the class have
    /// </summary>
    /// <returns></returns>
    public string OutputExample()
    {
        return string.Format("Speed: {0}, Rotate: {1}, Model: {2}, Price: {3}, id: {4}", Speed, rotate, Model, price, id);
    }

    /// <summary>
    /// 通过override ToString 来返回也可以
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Format("Speed: {0}, Rotate: {1}, Model: {2}, Price: {3}, id{4}", Speed, rotate, Model, price, id);
    }
}
