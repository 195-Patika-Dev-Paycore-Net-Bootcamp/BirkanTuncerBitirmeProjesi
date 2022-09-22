using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Data
{
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });
           
            Property(b => b.ProductOwnerAccountId, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("ProductOwnerAccountId");
                x.NotNullable(true);
            });

            Property(b => b.ProductName, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("productname");
                x.NotNullable(true);
                x.Length(100);
            });

            Property(x => x.ProductDescription, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("productdescription");
                x.NotNullable(true);
                x.Length(500);
            });
            Property(x => x.ProductColor, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("productcolor");
                x.NotNullable(true);
            });
            Property(x => x.ProductBrand, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("productbrand");
                x.NotNullable(true);
            });
            Property(x => x.ProductPrice, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Column("productprice");
                x.NotNullable(true);
            });
            Property(x => x.isOfferable, x =>
            {
                x.Type(NHibernateUtil.Boolean);
                x.Column("isofferable");
                x.NotNullable(true);
            });
            Property(x => x.isSold, x =>
            {
                x.Type(NHibernateUtil.Boolean);
                x.Column("issold");
                x.NotNullable(true);
                               
            });
            Property(x => x.CategoryId, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("categoryid");
                x.NotNullable(true);
            });

            Table("product");
        }
    }
}
