using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Data
{
    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });
            Property(b => b.CategoryName, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("categoryname");
                x.NotNullable(true);
                
            });
            Table("category");
        }
    }
}
