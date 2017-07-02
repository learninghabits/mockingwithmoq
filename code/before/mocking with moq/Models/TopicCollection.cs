using System.Collections.Generic;

public class Topic
{
    public Topic()
    {
        Tutorials = new List<Tutorial>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<Tutorial> Tutorials { get; set; }
}

public class Tutorial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public virtual Topic Topic { get; set; }
}
