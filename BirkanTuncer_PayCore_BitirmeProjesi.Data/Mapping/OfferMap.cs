using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Data
{
    public class OfferMap : ClassMapping<Offer>
    {
        public OfferMap()
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

            Property(b => b.OfferPrice, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Column("offerprice");
                x.NotNullable(true);
            });

            Property(x => x.Offerer, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("offerer");
                x.NotNullable(true);
            });
            Property(x => x.Status, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("status");
            });


            Table("offer");
        }
    }
}
