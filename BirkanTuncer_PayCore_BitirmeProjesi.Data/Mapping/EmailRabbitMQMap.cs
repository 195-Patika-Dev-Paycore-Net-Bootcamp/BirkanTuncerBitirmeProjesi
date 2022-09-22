using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Data.Mapping
{
    public class EmailRabbitMQMap : ClassMapping<EmailRabbitMQ>
    {
        public EmailRabbitMQMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });

            Property(b => b.Email, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("email");
                x.NotNullable(true);
            });

            Property(b => b.Status, x =>
            {
                x.Type(NHibernateUtil.String);
                x.Column("status");
                x.NotNullable(true);
                
            });

            Table("email");
        }
    }
}
