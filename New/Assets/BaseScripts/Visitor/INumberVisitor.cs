using BaseScripts.Factory.NumberFactory;

namespace BaseScripts.Visitor
{
    public interface INumberVisitor
    {
        void Visit(OneNumber oneNumber);
        void Visit(ZeroNumber zeroNumber);
    }
}