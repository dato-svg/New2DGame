using BaseScripts.Visitor;

namespace BaseScripts.Factory.NumberFactory
{
    public class ZeroNumber :  Number
    {
        public override void Accept(NumberVisitor visitor) => 
            visitor.Visit(this);
    }
}