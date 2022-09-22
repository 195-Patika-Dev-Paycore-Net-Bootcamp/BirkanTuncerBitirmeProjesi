using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Data
{
    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });

            Property(b => b.ProductId, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("productid");
                x.NotNullable(true);
            });
            Property(b => b.ProductName, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("productname");
                x.NotNullable(true);
            });
            Property(b => b.Buyer, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("buyer");
                x.NotNullable(true);
            });

            Table("order");
        }
    }
}
