namespace BirkanTuncer_PayCore_BitirmeProjesi.Data
{
    public class Product
    {
        
        public virtual int Id { get; set; }
        public virtual int ProductOwnerAccountId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string ProductDescription { get; set; }
        public virtual string ProductColor { get; set; }
        public virtual string ProductBrand { get; set; }
        public virtual double ProductPrice { get; set; }
        public virtual bool isOfferable { get; set; }
        public virtual bool isSold { get; set; }
        public virtual int CategoryId { get; set; }
        

    }
}
