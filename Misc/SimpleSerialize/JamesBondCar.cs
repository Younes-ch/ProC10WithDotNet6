namespace SimpleSerialize;

[Serializable, XmlRoot(Namespace = "http://www.MyCompany.com")]
public class JamesBondCar : Car
{
    [XmlAttribute]
    public bool CanFly;
    
    [XmlAttribute]
    public bool CanSubmerge;
    public override string ToString() => $"CanFly:{CanFly}, CanSubmerge:{CanSubmerge} {base.ToString()}";
}
